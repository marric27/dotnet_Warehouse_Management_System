using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.GoodsIn.Dtos;
using dotnet_Warehouse_Management_System.GoodsIn.Entities.Services;
using dotnet_Warehouse_Management_System.Outbound.Dtos;
using dotnet_Warehouse_Management_System.Outbound.Entities.Services;
using dotnet_Warehouse_Management_System.Picking.Dtos;
using dotnet_Warehouse_Management_System.Picking.Entities.Service;

namespace dotnet_Warehouse_Management_System.Picking.Services
{
    public class PickingService(IPicklistService _picklistService, IPicklistItemService _picklistItemService, IPickingInfoService _pickingInfoService, IStockUnitService _stockUnitService)
    {
        public Task<PicklistItemDto> GetNextPickListItem(NextItemRequest nextItemRequest) => _picklistService.GetNextPickListItemAsync(nextItemRequest);

        //        public async Task ConfirmPickingAsync(ConfirmPickingRequest request)
        //        {
        //            PicklistItemDto picklistItem = await LoadPickListItem(request.PickListCode, request.PickListItemCode);
        //            Dictionary<string, int> stockUnitQuantities = request.stockUnitQuantities
        //                .ToDictionary(
        //                    x => x.SuId,
        //                    x => x.Quantity
        //                );
        //            if (stockUnitQuantities == null || stockUnitQuantities.Count == 0)
        //            {
        //                throw new Exception("No stock units provided for picking");
        //            }
        //            int toPick = stockUnitQuantities.Values.Sum();
        //            if (toPick > picklistItem.Quantity - picklistItem.PickedQty) throw new Exception("Errore: Stai richiedendo quantità maggiore di quanto specificata nel pick list item");

        //            ErrorReason? errorReason;
        //            if (toPick < picklistItem.PickedQty && request.ErrorReason != null)
        //            {
        //                errorReason = request.ErrorReason.Value;
        //            }
        //            else if (toPick == picklistItem.Quantity)
        //            {
        //                errorReason = null;
        //            }
        //            else
        //            {
        //                throw new Exception("Error reason cant be omitted when qty to pick is lower than ");
        //            }

        //            Dictionary<string, StockUnitResponseDto> StockUnitsByCode = [];
        //            foreach (string code in stockUnitQuantities.Keys)
        //            {
        //                StockUnitResponseDto stockUnit = await _stockUnitService.GetByCodeAsync(code);
        //                StockUnitsByCode.Add(stockUnit.Code, stockUnit);
        //            }

        //            CanPickFromSU(stockUnitQuantities, StockUnitsByCode, picklistItem);

        //            ExecutePicking(stockUnitQuantities, StockUnitsByCode, picklistItem);
        //            UpdatePicklistItem(picklistItem, totalPickedQty, errorReason);
        //        }

        //        private void UpdatePicklistItem(PicklistItemDto picklistItem, int totalPickedQty, ErrorReason? errorReason)
        //        {
        //            int pickedQty = picklistItem.PickedQty + totalPickedQty;
        //            picklistItem.PickedQty = pickedQty;
        //            if (pickedQty == picklistItem.Quantity) picklistItem.State = PicklistItemState.PICKED;
        //            picklistItem.ErrorReason = errorReason;

        //            throw new NotImplementedException();
        //        }

        //        private void ExecutePicking(Dictionary<string, int> requested, Dictionary<string, StockUnitResponseDto> stockUnitsByCode, PicklistItemDto picklistItem)
        //        {
        //            foreach (var entry in requested)
        //            {
        //                string code = entry.Key;
        //                int quantityToPick = entry.Value;

        //                StockUnitResponseDto su = stockUnitsByCode[code];
        //                int oldQty = su.Quantity;
        //                su.Quantity = oldQty - quantityToPick;
        //                _stockUnitService.UpdateAsync(su.Id, su);

        //                CreatePickingInfo(su, quantityToPick, picklistItem);

        //            }
        //        }



        //        private void CreatePickingInfo(StockUnitResponseDto su, int pickedQty, PicklistItemDto picklistItem)
        //        {
        //            PickingInfoDto pickingInfo = new PickingInfoDto()
        //            {
        //                User = "USR-01QWERTY",
        //                Timestamp = DateTime.Now,
        //                Quantity = pickedQty,
        //                StockUnitCode = su.Code,
        //                BatchNumber = su.BatchNumber,
        //                ExpirationDate = su.ExpirationDate,

        //            };
        //            var created = _pickingInfoService.CreateAsync(pickingInfo);
        //        }

        //        private void CanPickFromSU(Dictionary<string, int> requested, Dictionary<string, StockUnitResponseDto> stockUnitsByCode, PicklistItemDto picklistItem)
        //        {
        //            foreach (var entry in requested)
        //            {
        //                string code = entry.Key;
        //                int quantity = entry.Value;

        //                if (!stockUnitsByCode.TryGetValue(code, out var su))
        //                {
        //                    throw new KeyNotFoundException($"StockUnit {code} not found in stockUnits dictionary.");
        //                }

        //                if (!su.ProductCode.Equals(picklistItem.productCode, StringComparison.OrdinalIgnoreCase))
        //                {
        //                    throw new Exception(
        //                        $"StockUnit {code} contains product {su.ProductCode} " +
        //                        $"but PickListItem requires product {picklistItem.productCode}"
        //                    );
        //                }

        //                if (quantity > su.Quantity)
        //                {
        //                    throw new Exception(
        //                        $"Requested quantity {quantity} > available quantity {su.Quantity} for stock unit: {code}"
        //                    );
        //                }
        //            }
        //        }

        //        private async Task<PicklistItemDto> LoadPickListItem(string pickListCode, string pickListItemCode)
        //        {
        //            var picklistDto = await _picklistService.GetByCodeAsync(pickListCode);
        //            var item = picklistDto.pickListItemList.FirstOrDefault(i => i.code == pickListItemCode);

        //            if (item.State != PicklistItemState.OPEN)
        //            {
        //                throw new Exception("PickListItem is not OPEN: " + item.State);
        //            }

        //            return item;
        //        }

    }
}
