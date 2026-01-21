using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Outbound.Dtos;

namespace dotnet_Warehouse_Management_System.Outbound.Entities.Repositories
{
    public interface IOrderRepository
    {
        Task<Page<Order>> GetAllAsync(QueryObject query);
        Task<Order?> GetByCodeAsync(string code);
        Task<Order> CreateAsync(Order Order);
        Task<Order> UpdateAsync(string code, OrderRequestDto orderDto);
        Task<Order?> DeleteAsync(string code);
    }
}
