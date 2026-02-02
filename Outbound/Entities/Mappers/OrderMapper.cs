using dotnet_Warehouse_Management_System.Outbound.Dtos;

namespace dotnet_Warehouse_Management_System.Outbound.Entities.Mappers
{
    public static class OrderMapper
    {
        public static OrderResponseDto ToResponseDto(this Order order)
        {
            return new OrderResponseDto
            {
                id = order.Id,
                code = order.Code,
                date = order.date,
                customerCode = order.CustomerCode,
                state = order.State,
                salesOrderLineList = order.SalesOrderLineList
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
                State = dto.State
            };

            order.SalesOrderLineList = dto.SalesOrderLineList
                .Select((x, index) =>
                {
                    var line = x.ToEntity();
                    line.Order = order;
                    line.SalesOrderLineNumber = index + 1;
                    return line;
                })
                .ToList();
            return order;
        }

    }
}