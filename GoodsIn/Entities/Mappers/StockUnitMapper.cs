using dotnet_Warehouse_Management_System.GoodsIn.Dtos;
using dotnet_Warehouse_Management_System.Products.Entities.Mappers;
using dotnet_Warehouse_Management_System.Warehouses.Entities.Mappers;

namespace dotnet_Warehouse_Management_System.GoodsIn.Entities.Mappers
{
    public static class StockUnitMapper
    {
        public static StockUnitResponseDto ToResponseDto (this StockUnit stockUnit)
        {
            return new StockUnitResponseDto
            {
                Id = stockUnit.Id,
                BatchNumber = stockUnit.BatchNumber,
                ExpirationDate = stockUnit.ExpirationDate,
                ProductCode = stockUnit.ProductCode,
                Code = stockUnit.Code,
                Quantity = stockUnit.Quantity,
                Category = stockUnit.Category,
                SlotId = stockUnit.SlotId
            };
        }

        public static StockUnit ToStockUnit (this StockUnitRequestDto dto)
        {
            return new StockUnit
            {
                BatchNumber = dto.BatchNumber,
                ExpirationDate = dto.ExpirationDate,
                Quantity = dto.Quantity,
                Category = dto.Category,
                ProductCode = dto.ProductCode,

            };

        }
    }
}