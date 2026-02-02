using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.GoodsIn.Dtos;
using dotnet_Warehouse_Management_System.GoodsIn.Entities.Services;
using dotnet_Warehouse_Management_System.Outbound.Dtos;
using dotnet_Warehouse_Management_System.Outbound.Entities.Services;
using dotnet_Warehouse_Management_System.Picking.Dtos;
using dotnet_Warehouse_Management_System.Picking.Entities.Service;

namespace dotnet_Warehouse_Management_System.Picking.Services
{
    public class PickingService(IPicklistService picklistService, IPicklistItemService picklistItemService, IPickingInfoService pickingInfoService, IStockUnitService stockUnitService)
    {
        public async Task<PicklistItemDto> GetNextPickListItem(NextItemRequest nextItemRequest) => await picklistService.GetNextPickListItemAsync(nextItemRequest);

        public async Task ConfirmPickingAsync(ConfirmPickingRequest request)
        {
            PicklistItemDto picklistItem = await LoadPickListItem(request.PickListCode, request.PickListItemCode);
            Dictionary<string, int> stockUnitQuantities = request.stockUnitQuantities
                .ToDictionary(
                    x => x.SuId,
                    x => x.Quantity
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

            await ExecutePicking(stockUnitQuantities, StockUnitsByCode, picklistItem);
            await UpdatePicklistItem(picklistItem, toPick, errorReason);
        }

        private async Task<PicklistItemDto> LoadPickListItem(string pickListCode, string pickListItemCode)
        {
            var picklistDto = await picklistService.GetByCodeAsync(pickListCode);
            var item = picklistDto.pickListItemList.FirstOrDefault(i => i.Code == pickListItemCode);

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

        private async Task UpdatePicklistItem(PicklistItemDto picklistItem, int totalPickedQty, ErrorReason errorReason)
        {
            int pickedQty = picklistItem.PickedQty + totalPickedQty;
            picklistItem.PickedQty = pickedQty;
            if (pickedQty == picklistItem.Qty) picklistItem.State = PicklistItemState.PICKED;
            picklistItem.ErrorReason = errorReason;
            await picklistItemService.UpdateAsync(picklistItem.Code, picklistItem);
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
