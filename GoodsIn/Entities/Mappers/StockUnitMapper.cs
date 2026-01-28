using dotnet_Warehouse_Management_System.GoodsIn.Dtos;

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
                Product = stockUnit.Product.ToResponseDto(),
                Slot = stockUnit.Slot.ToResponseDto()
            };
        }

        public static StockUnit ToStockUnit (StockUnitRequestDto dto)
        {
            return new StockUnit
            {
                BatchNumber = dto.BatchNumber,
                ExpirationDate = dto.ExpirationDate,
                Quantity = dto.Quantity
            };

        }
    }
}