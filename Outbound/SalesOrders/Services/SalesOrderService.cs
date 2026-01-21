using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Customers.Entities.Dtos;
using dotnet_Warehouse_Management_System.Customers.Entities.Services;
using dotnet_Warehouse_Management_System.Outbound.Dtos;
using dotnet_Warehouse_Management_System.Outbound.Entities.Mappers;
using dotnet_Warehouse_Management_System.Outbound.Entities.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Runtime.InteropServices;

namespace dotnet_Warehouse_Management_System.Outbound.SalesOrders.Services
{
    public class SalesOrderService
    {
        private readonly ICustomerService _customerService;
        private readonly IOrderService _orderService;
        public SalesOrderService(ICustomerService customerService, IOrderService orderService)
        {
            _customerService = customerService;
            _orderService = orderService;
        }

        public async Task<OrderResponseDto> CreateOrderAndAssign(string customerCode, OrderRequestDto orderDto)
        {
            CustomerResponseDto customerResponseDto = await _customerService.GetByCodeAsync(customerCode);

            // verifica che i prodotti ordinati esistano

            OrderRequestDto order = new OrderRequestDto()
            {
                CustomerCode = customerCode,
                Date = DateTime.UtcNow,
                State = Entities.OrderState.OPEN,
                SalesOrderLineList = orderDto.SalesOrderLineList
            };

            var created = await _orderService.CreateAsync(order);
            return created;
        }

        public async Task<OrderResponseDto> GetByCodeAsync(string code)
        {
            var ord = await _orderService.GetByCodeAsync(code);
            return ord;
        }

        public async Task<Page<OrderResponseDto>> GetAllPaginated(QueryObject query)
        {
            return await _orderService.GetAllPaginatedAsync(query);
        }
    }
}
