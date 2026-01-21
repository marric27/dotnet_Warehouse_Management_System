using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Outbound.Dtos;

namespace dotnet_Warehouse_Management_System.Outbound.Entities.Services
{
    public interface IOrderService
    {
        Task<Page<OrderResponseDto>> GetAllAsync(QueryObject query);
        Task<OrderResponseDto?> GetByCodeAsync(string code);
        Task<OrderResponseDto> CreateAsync(OrderRequestDto OrderDto);
        Task<OrderResponseDto?> UpdateAsync(string code, OrderRequestDto OrderDto);
        Task<OrderResponseDto?> DeleteAsync(string code);
    }
}
