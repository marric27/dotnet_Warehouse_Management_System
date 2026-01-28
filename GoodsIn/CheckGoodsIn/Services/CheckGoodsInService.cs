using Azure.Core;
using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.GoodsIn.Dtos;
using dotnet_Warehouse_Management_System.GoodsIn.Entities.Repositories;
using dotnet_Warehouse_Management_System.GoodsIn.Entities.Services;
using dotnet_Warehouse_Management_System.GoodsIn.Services;
using dotnet_Warehouse_Management_System.Products.Entities.Services;

namespace dotnet_Warehouse_Management_System.GoodsIn.CheckGoodsIn.Services
{
    public class CheckGoodsInService(IGrnService grnService, IGrnItemService grnItemService, ICheckingInfoService checkingInfoService, IGrnItemStateService stateService, IProductService productService, IStockUnitService stockUnitService)
    {
        public async Task<GrnItemResponseDto> CreateCheckingInfoAndStockUnit(string grnItemCode, StockUnitRequestDto su)
        {
            GrnItemResponseDto grnItem = await grnItemService.GetByCodeAsync(grnItemCode);
            if (grnItem.State == Common.State.CHECKED || grnItem.State == Common.State.PUTAWAY) throw new Exception("Cant assign checking info to GrnItem " + grnItemCode + " in Closed or Putaway state");

            if (su.Quantity > grnItem.ReceivedQty) throw new Exception("Requested quantity " + su.Quantity + " exceeds available quantity " + grnItem.ReceivedQty);

            var alreadyStockedQty = grnItem.checkingInfoList.Sum(ci => ci.Quantity);
            var toStockQty = grnItem.ReceivedQty - alreadyStockedQty;

            if (su.Quantity > toStockQty) throw new Exception("Requested quantity " + su.Quantity + " exceeds available quantity " + toStockQty);

            su.ProductCode = grnItem.ProductCode;

            var product = await productService.GetByCodeAsync(grnItem.ProductCode);
            su.Category = product.Category;

            // Create StockUnit
            var stockUnit = await stockUnitService.CreateAsync(su);

            // Create CheckingInfo
            var ci = new CheckingInfoDto
            {
                StockUnitId = stockUnit.Id,
                GrnItemId = grnItem.Id,
                State = State.OPEN,
                Quantity = su.Quantity,
                BatchNumber = su.BatchNumber,
                ExpirationDate = su.ExpirationDate
            };

            var savedCi = await checkingInfoService.CreateAsync(ci);

            // Assign to item
            //await grnItemService.AddCheckingInfo(grnItemCode, savedCi.Code);

            // Progress state
            var updatedItem = await grnItemService.GetByCodeAsync(grnItemCode);
            await stateService.EvaluateAndProgressGrnItemStateAsync(updatedItem);

            return updatedItem;
        }
    }
}
