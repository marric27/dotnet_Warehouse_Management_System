using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Exceptions;
using dotnet_Warehouse_Management_System.Data;
using dotnet_Warehouse_Management_System.GoodsIn.Dtos;
using dotnet_Warehouse_Management_System.GoodsIn.Entities.Services;
using dotnet_Warehouse_Management_System.Outbound.Dtos;
using dotnet_Warehouse_Management_System.Outbound.Entities;
using dotnet_Warehouse_Management_System.Outbound.Entities.Services;
using dotnet_Warehouse_Management_System.Picking.Dtos;
using dotnet_Warehouse_Management_System.Picking.Entities.Service;

namespace dotnet_Warehouse_Management_System.Picking.Services
{
    public class PickingService(ApplicationDBContext context, IPicklistService picklistService, IPicklistItemService picklistItemService, IPickingInfoService pickingInfoService, IStockUnitService stockUnitService)
    {
        public async Task<PicklistItemDto?> GetNextPickListItem(NextItemRequest nextItemRequest) => await picklistService.GetNextPickListItemAsync(nextItemRequest);

        public async Task ConfirmPickingAsync(ConfirmPickingRequest request)
        {
            using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                var picklistDto = await picklistService.GetByCodeAsync(request.PickListCode)
                    ?? throw new KeyNotFoundException($"Picklist {request.PickListCode} not found");
                PicklistItemDto picklistItem = await LoadPickListItem(picklistDto, request.PickListItemCode);
                Dictionary<string, int> stockUnitQuantities = request.stockUnitQuantities
                    .GroupBy(x => x.SuId)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Sum(x => x.Quantity)
                    );

                (int toPick, ErrorReason errorReason) = ValidateConfirmPickingRules(request, picklistItem, stockUnitQuantities);

                Dictionary<string, StockUnitResponseDto> StockUnitsByCode = [];
                foreach (string code in stockUnitQuantities.Keys)
                {
                    StockUnitResponseDto stockUnit = await stockUnitService.GetByCodeAsync(code);
                    StockUnitsByCode.Add(stockUnit.Code, stockUnit);
                }

                CanPickFromSU(stockUnitQuantities, StockUnitsByCode, picklistItem);

                await ExecutePicking(stockUnitQuantities, StockUnitsByCode, picklistItem);
                await UpdatePicklistItem(picklistDto.Code, picklistItem, toPick, errorReason);
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        private async Task<PicklistItemDto> LoadPickListItem(PicklistDto picklistDto, string pickListItemCode)
        {
            var item = picklistDto.pickListItemList.FirstOrDefault(i => i.Code == pickListItemCode)
                ?? throw new KeyNotFoundException($"Picklist item {pickListItemCode} not found");

            if (item.State != PicklistItemState.OPEN)
            {
                throw new DomainConflictException("PickListItem is not OPEN: " + item.State);
            }

            return item;
        }

        private void CanPickFromSU(Dictionary<string, int> requested, Dictionary<string, StockUnitResponseDto> stockUnitsByCode, PicklistItemDto picklistItem)
        {
            foreach (var entry in requested)
            {
                string code = entry.Key;
                int quantity = entry.Value;

                if (!stockUnitsByCode.TryGetValue(code, out var su))
                {
                    throw new KeyNotFoundException($"StockUnit {code} not found in stockUnits dictionary.");
                }

                if (!su.ProductCode.Equals(picklistItem.ProductCode, StringComparison.OrdinalIgnoreCase))
                {
                    throw new DomainConflictException(
                        $"StockUnit {code} contains product {su.ProductCode} " +
                        $"but PickListItem requires product {picklistItem.ProductCode}"
                    );
                }

                if (quantity > su.Quantity)
                {
                    throw new DomainConflictException(
                        $"Requested quantity {quantity} > available quantity {su.Quantity} for stock unit: {code}"
                    );
                }
            }
        }
        

        private static (int ToPick, ErrorReason ErrorReason) ValidateConfirmPickingRules(ConfirmPickingRequest request, PicklistItemDto picklistItem, Dictionary<string, int> stockUnitQuantities)
        {
            if (stockUnitQuantities.Count == 0)
            {
                throw new ArgumentException("No stock units were provided for picking.");
            }

            if (stockUnitQuantities.Any(x => string.IsNullOrWhiteSpace(x.Key)))
            {
                throw new ArgumentException("Every stock unit id must be provided.");
            }

            if (stockUnitQuantities.Any(x => x.Value <= 0))
            {
                throw new ArgumentException("Every stock unit quantity must be greater than zero.");
            }

            int toPick = stockUnitQuantities.Values.Sum();
            if (toPick <= 0)
            {
                throw new ArgumentException("The sum of stock unit quantities must be greater than zero.");
            }

            int remainingQty = picklistItem.Qty - picklistItem.PickedQty;
            if (toPick > remainingQty)
            {
                throw new ArgumentException($"Cannot pick {toPick} items: remaining quantity for the picklist item is {remainingQty}.");
            }

            int totalAfterPicking = picklistItem.PickedQty + toPick;
            if (totalAfterPicking < picklistItem.Qty && request.ErrorReason is null)
            {
                throw new ArgumentException($"ErrorReason is required when picked quantity ({totalAfterPicking}) is lower than requested quantity ({picklistItem.Qty}).");
            }

            ErrorReason errorReason = totalAfterPicking == picklistItem.Qty
                ? ErrorReason.NO_ERROR
                : request.ErrorReason!.Value;

            return (toPick, errorReason);
        }

        private async Task UpdatePicklistItem(string picklistCode, PicklistItemDto picklistItem, int totalPickedQty, ErrorReason errorReason)
        {
            int pickedQty = picklistItem.PickedQty + totalPickedQty;
            picklistItem.PickedQty = pickedQty;
            if (pickedQty == picklistItem.Qty)
            {
                picklistItem.State = PicklistItemState.PICKED;
                await UpdatePicklist(picklistCode);
            }
            picklistItem.ErrorReason = errorReason;
            await picklistItemService.UpdateAsync(picklistItem.Code, picklistItem);
        }

        private async Task UpdatePicklist(string picklistCode)
        {
            var picklistDto = await picklistService.GetByCodeAsync(picklistCode)
                ?? throw new KeyNotFoundException($"Picklist {picklistCode} not found");

            foreach (var item in picklistDto.pickListItemList)
            {
                if (item.State == PicklistItemState.OPEN)
                {
                    return;
                }
            }
            picklistDto.State = PicklistState.CLOSED;
            await picklistService.UpdateAsync(picklistDto);
        }

        private async Task ExecutePicking(Dictionary<string, int> requested, Dictionary<string, StockUnitResponseDto> stockUnitsByCode, PicklistItemDto picklistItem)
        {
            foreach (var entry in requested)
            {
                string code = entry.Key;
                int quantityToPick = entry.Value;

                StockUnitResponseDto su = stockUnitsByCode[code];
                int oldQty = su.Quantity;
                su.Quantity = oldQty - quantityToPick;
                await stockUnitService.UpdateAsync(su);

                await CreatePickingInfo(su, quantityToPick, picklistItem);
            }
        }

        private async Task CreatePickingInfo(StockUnitResponseDto su, int pickedQty, PicklistItemDto picklistItem)
        {
            PickingInfoDto pickingInfo = new()
            {
                User = "USR-01QWERTY",
                Timestamp = DateTime.Now,
                Quantity = pickedQty,
                StockUnitCode = su.Code,
                BatchNumber = su.BatchNumber,
                ExpirationDate = su.ExpirationDate,
                PicklistItemCode = picklistItem.Code,
                PicklistItemId = picklistItem.Id
            };
            await pickingInfoService.CreateAsync(pickingInfo);
        }
    }
}
