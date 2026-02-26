using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Outbound.Dtos;
using dotnet_Warehouse_Management_System.Outbound.Entities.Mappers;
using dotnet_Warehouse_Management_System.Outbound.Entities.Repositories;

namespace dotnet_Warehouse_Management_System.Outbound.Entities.Services
{
    public class OrderService(IOrderRepository orderRepository) : IOrderService
    {
        public async Task<OrderResponseDto> CreateAsync(OrderRequestDto OrderDto)
        {
            ValidateCreateOrderRequest(OrderDto);

            var order = OrderDto.ToEntity();
            order.GenerateCode();
            var created = await orderRepository.CreateAsync(order);
            return created.ToResponseDto();
        }

        public async Task<bool> DeleteAsync(string code)
        {
            throw new NotImplementedException();
        }

        public async Task<List<OrderResponseDto>> GetAllAsync()
        {
            var orders = await orderRepository.GetAllAsync();
            return orders.Select(o => o.ToResponseDto()).ToList();
        }

        public async Task<Page<OrderResponseDto>> GetAllPaginatedAsync(QueryObject query)
        {
            var orders = await orderRepository.GetAllPaginatedAsync(query);
            return orders.Map(o => o.ToResponseDto());
        }

        public async Task<OrderResponseDto?> GetByCodeAsync(string code)
        {
            var order = await orderRepository.GetByCodeAsync(code, false) ?? throw new KeyNotFoundException("Order not found.");
            return order?.ToResponseDto();
        }

        public async Task<List<OrderResponseDto>> GetByStateAndIdsAsync(OrderState state, List<long> ids)
        {
            var order = await orderRepository.GetByStateAndIds(state, ids);
            return [.. order.Select(order => order.ToResponseDto())];
        }

        public async Task<OrderResponseDto?> UpdateAsync(OrderResponseDto OrderDto)
        {
            var existingOrder = await orderRepository.GetByCodeAsync(OrderDto.code, true);
            existingOrder.State = OrderDto.state;
            await orderRepository.UpdateAsync();
            return existingOrder.ToResponseDto();
        }

        private static void ValidateCreateOrderRequest(OrderRequestDto orderDto)
        {
            if (orderDto.SalesOrderLineList is null || orderDto.SalesOrderLineList.Count == 0)
            {
                throw new ArgumentException("At least one sales order line is required.");
            }

            int totalRequestedQty = orderDto.SalesOrderLineList.Sum(line => line.quantity);
            if (totalRequestedQty <= 0)
            {
                throw new ArgumentException("The sum of all sales order line quantities must be greater than zero.");
            }

            if (orderDto.State != OrderState.OPEN)
            {
                throw new ArgumentException($"Invalid order state for creation: {orderDto.State}. Only OPEN is allowed.");
            }
        }
    }
}
