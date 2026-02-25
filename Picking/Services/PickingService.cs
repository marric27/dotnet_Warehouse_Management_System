using dotnet_Warehouse_Management_System.Common;
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
                if (stockUnitQuantities == null || stockUnitQuantities.Count == 0)
                {
                    throw new Exception("No stock units provided for picking");
                }
                int toPick = stockUnitQuantities.Values.Sum();
                if (toPick > picklistItem.Qty - picklistItem.PickedQty) throw new Exception("Errore: Stai richiedendo quantità maggiore di quanto specificata nel pick list item");


                int totalAfterPicking = picklistItem.PickedQty + toPick;
                ErrorReason errorReason;

                // Caso A: Abbiamo prelevato tutto quello che serviva
                if (totalAfterPicking == picklistItem.Qty)
                {
                    errorReason = ErrorReason.NO_ERROR;
                }
                // Caso B: Abbiamo prelevato meno del totale richiesto
                else if (totalAfterPicking < picklistItem.Qty)
                {
                    if (request.ErrorReason != null)
                    {
                        errorReason = request.ErrorReason.Value;
                    }
                    else
                    {
                        throw new Exception($"Error reason is required when total picked qty ({totalAfterPicking}) is lower than requested qty ({picklistItem.Qty})");
                    }
                }
                // Caso C: Più del richiesto (già gestito sopra, ma per sicurezza)
                else
                {
                    throw new Exception("Cannot pick more than requested quantity");
                }

                Dictionary<string, StockUnitResponseDto> StockUnitsByCode = [];
                foreach (string code in stockUnitQuantities.Keys)
                {
                    StockUnitResponseDto stockUnit = await stockUnitService.GetByCodeAsync(code);
                    StockUnitsByCode.Add(stockUnit.Code, stockUnit);
                }

                CanPickFromSU(stockUnitQuantities, StockUnitsByCode, picklistItem);

                await ExecutePicking(stockUnitQuantities, StockUnitsByCode, picklistItem, request.User);
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
                throw new Exception("PickListItem is not OPEN: " + item.State);
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
                    throw new Exception(
                        $"StockUnit {code} contains product {su.ProductCode} " +
                        $"but PickListItem requires product {picklistItem.ProductCode}"
                    );
                }

                if (quantity > su.Quantity)
                {
                    throw new Exception(
                        $"Requested quantity {quantity} > available quantity {su.Quantity} for stock unit: {code}"
                    );
                }
            }
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

        private async Task ExecutePicking(Dictionary<string, int> requested, Dictionary<string, StockUnitResponseDto> stockUnitsByCode, PicklistItemDto picklistItem, string requestUser)
        {
            foreach (var entry in requested)
            {
                string code = entry.Key;
                int quantityToPick = entry.Value;

                StockUnitResponseDto su = stockUnitsByCode[code];
                int oldQty = su.Quantity;
                su.Quantity = oldQty - quantityToPick;
                await stockUnitService.UpdateAsync(su);

                await CreatePickingInfo(su, quantityToPick, picklistItem, requestUser);
            }
        }

        private async Task CreatePickingInfo(StockUnitResponseDto su, int pickedQty, PicklistItemDto picklistItem, string requestUser)
        {
            PickingInfoDto pickingInfo = new()
            {
                User = requestUser,
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
