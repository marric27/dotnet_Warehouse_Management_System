using dotnet_Warehouse_Management_System.Outbound.Dtos;

namespace dotnet_Warehouse_Management_System.Outbound.Entities.Mappers
{
    public static class OrderMapper
    {
        public static OrderResponseDto ToResponseDto(this Order order)
        {
            return new OrderResponseDto
            {
                Code = order.Code,
                Date = order.date,
                CustomerCode = order.CustomerCode,
                State = order.State,
                SalesOrderLines = order.SalesOrderLines
                    .Select(x => x.ToResponseDto())
                    .ToList()
            };
        }

        public static Order ToEntity(this OrderRequestDto dto)
        {
            var order = new Order
            {
                CustomerCode = dto.CustomerCode,
                date = DateTime.UtcNow,
                SalesOrderLines = dto.SalesOrderLineList
                    .Select(x => x.ToEntity())
                    .ToList()
            };

            order.GenerateCode();
            return order;
        }
    }
}