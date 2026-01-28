using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;

namespace dotnet_Warehouse_Management_System.GoodsIn.Entities.Repositories
{
    public interface IGrnItemRepository
    {
        Task<List<GrnItem>> GetAllAsync(QueryObject query);
        Task<GrnItem?> GetById(long id);
        Task<GrnItem?> GetByCodeAsync(string code);
        Task<GrnItem> CreateAsync(GrnItem item);
        Task<GrnItem> UpdateAsync(GrnItem item);
        Task<GrnItem?> DeleteAsync(string code);
        Task<GrnItem?> UpdateStateAsync(string code, State newState);
    }
}
