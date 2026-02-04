using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Data;
using dotnet_Warehouse_Management_System.GoodsIn.Dtos;
using dotnet_Warehouse_Management_System.GoodsIn.Entities.Services;
using dotnet_Warehouse_Management_System.GoodsIn.Services;
using dotnet_Warehouse_Management_System.Products.Entities.Services;

namespace dotnet_Warehouse_Management_System.GoodsIn.CheckGoodsIn.Services
{
    public class CheckGoodsInService(ApplicationDBContext context, IGrnService grnService, IGrnItemService grnItemService, ICheckingInfoService checkingInfoService, IGrnItemStateService stateService, IProductService productService, IStockUnitService stockUnitService)
    {
        public async Task<GrnItemResponseDto> CreateCheckingInfoAndStockUnit(string grnItemCode, StockUnitRequestDto su)
        {
            using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                // 1. Recupero dati iniziale
                var grnItem = await grnItemService.GetByCodeAsync(grnItemCode)
                          ?? throw new Exception($"GrnItem {grnItemCode} not found");

                // 2. Validazioni business
                if (grnItem.State == State.CHECKED || grnItem.State == State.PUTAWAY)
                    throw new Exception("State is already Closed or Putaway");

                var alreadyStockedQty = grnItem.CheckingInfoList.Sum(ci => ci.Quantity);
                if (su.Quantity > (grnItem.ReceivedQty - alreadyStockedQty))
                    throw new Exception("Requested quantity exceeds available quantity");

                // 3. Preparazione StockUnit
                var product = await productService.GetByCodeAsync(grnItem.ProductCode);
                su.ProductCode = grnItem.ProductCode;
                su.Category = product.Category;

                var stockUnit = await stockUnitService.CreateAsync(su);

                // 4. Salvataggio CheckingInfo
                var ci = new CheckingInfoDto
                {
                    StockUnitId = stockUnit.Id,
                    GrnItemId = grnItem.Id,
                    State = State.OPEN,
                    Quantity = su.Quantity,
                    BatchNumber = su.BatchNumber,
                    ExpirationDate = su.ExpirationDate
                };
                await checkingInfoService.CreateAsync(ci);

                // 5. RE-FETCH FINALE (Cruciale per EF)
                // Recuperiamo l'item dal DB *dopo* l'inserimento della CheckingInfo.
                // Assicurati che GetByCodeAsync usi .AsNoTracking() per evitare conflitti di tracking.
                var updatedItem = await grnItemService.GetByCodeAsync(grnItemCode);

                await stateService.EvaluateAndProgressGrnItemStateAsync(updatedItem);
                await transaction.CommitAsync();
                return updatedItem;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<List<StockUnitResponseDto>> ListStockUnit()
        {
            return await stockUnitService.GetAllAsync();
        }
    }
}
