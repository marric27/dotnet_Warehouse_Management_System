using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Customers.Entities.Dtos;
using dotnet_Warehouse_Management_System.Customers.Entities.Services;
using dotnet_Warehouse_Management_System.Outbound.Dtos;
using dotnet_Warehouse_Management_System.Outbound.Entities.Services;

namespace dotnet_Warehouse_Management_System.Outbound.SalesOrders.Services
{
    public class SalesOrderService(ICustomerService customerService, IOrderService orderService)
    {
        public async Task<OrderResponseDto> CreateOrderAndAssign(string customerCode, OrderRequestDto orderDto)
        {
            CustomerResponseDto customerResponseDto = await customerService.GetByCodeAsync(customerCode);

            // verifica che i prodotti ordinati esistano TODO
            OrderRequestDto order = new()
            {
                CustomerCode = customerCode,
                Date = DateTime.UtcNow,
                State = Entities.OrderState.OPEN,
                SalesOrderLineList = orderDto.SalesOrderLineList
            };

            var created = await orderService.CreateAsync(order);
            return created;
        }

        public async Task<OrderResponseDto> GetByCodeAsync(string code)
        {
            var ord = await orderService.GetByCodeAsync(code);
            return ord;
        }

        public async Task<Page<OrderResponseDto>> GetAllPaginated(QueryObject query)
        {
            return await orderService.GetAllPaginatedAsync(query);
        }

        public async Task<List<OrderResponseDto>> GetAll()
        {
            return await orderService.GetAllAsync();
        }
    }
}
