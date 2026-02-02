using dotnet_Warehouse_Management_System.Outbound.Dtos;

namespace dotnet_Warehouse_Management_System.Outbound.Entities.Mappers
{
    public static class SalesOrderLineMapper
    {
        public static SalesOrderLineResponseDto ToResponseDto(this SalesOrderLine entity)
        {
            return new SalesOrderLineResponseDto
            {
                id = entity.Id,
                salesOrderLineNumber = entity.SalesOrderLineNumber,
                productCode = entity.ProductCode,
                quantity = entity.Quantity,
                status = entity.Status
            };
        }

        public static SalesOrderLine ToEntity(this SalesOrderLineRequestDto dto)
        {
            return new SalesOrderLine
            {
                ProductCode = dto.productCode,
                Quantity = dto.quantity,
            };
        }
    }
}