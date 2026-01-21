using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Outbound.Dtos;
using dotnet_Warehouse_Management_System.Outbound.Entities.Mappers;
using dotnet_Warehouse_Management_System.Outbound.Entities.Repositories;

namespace dotnet_Warehouse_Management_System.Outbound.Entities.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<OrderResponseDto> CreateAsync(OrderRequestDto OrderDto)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderResponseDto?> DeleteAsync(string code)
        {
            throw new NotImplementedException();
        }

        public async Task<Page<OrderResponseDto>> GetAllAsync(QueryObject query)
        {
            var orders = await _orderRepository.GetAllAsync(query);
            return orders.Map(o => o.ToResponseDto());
        }

        public async Task<OrderResponseDto?> GetByCodeAsync(string code)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderResponseDto?> UpdateAsync(string code, OrderRequestDto OrderDto)
        {
            throw new NotImplementedException();
        }
    }
}
