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
            var order = OrderDto.ToEntity();
            order.GenerateCode();
            var created = await _orderRepository.CreateAsync(order);
            return created.ToResponseDto();
        }

        public async Task<OrderResponseDto?> DeleteAsync(string code)
        {
            throw new NotImplementedException();
        }

        public async Task<List<OrderResponseDto>> GetAllAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return orders.Select(o => o.ToResponseDto()).ToList();  
        }

        public async Task<Page<OrderResponseDto>> GetAllPaginatedAsync(QueryObject query)
        {
            var orders = await _orderRepository.GetAllPaginatedAsync(query);
            return orders.Map(o => o.ToResponseDto());
        }

        public async Task<OrderResponseDto?> GetByCodeAsync(string code)
        {
            var order = await _orderRepository.GetByCodeAsync(code);
            return order?.ToResponseDto();
        }

        public async Task<List<OrderResponseDto>> GetByStateAndIdsAsync(OrderState state, List<long> ids)
        {
            var order = await _orderRepository.GetByStateAndIds(state, ids);
            return [.. order.Select(order => order.ToResponseDto())];
        }

        public async Task<OrderResponseDto?> UpdateAsync(string code, OrderRequestDto OrderDto)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderResponseDto?> UpdateStateAsync(string code, OrderState state)
        {
            var order = await _orderRepository.UpdateStateAsync(code, state);
            return order.ToResponseDto();
        }
    }
}
