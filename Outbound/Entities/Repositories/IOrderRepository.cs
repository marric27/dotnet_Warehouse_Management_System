using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Outbound.Dtos;

namespace dotnet_Warehouse_Management_System.Outbound.Entities.Repositories
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllAsync();
        Task<Page<Order>> GetAllPaginatedAsync(QueryObject query);
        Task<Order?> GetByCodeAsync(string code, bool track);
        Task<Order> CreateAsync(Order Order);
        Task UpdateAsync();
        Task DeleteAsync(Order Order);
        Task<List<Order>> GetByStateAndIds(OrderState state, List<long> ids);
    }
}
