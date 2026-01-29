using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;

namespace dotnet_Warehouse_Management_System.GoodsIn.Entities.Repositories
{
    public interface IGrnItemRepository
    {
        Task<List<GrnItem>> GetAllAsync(QueryObject query);
        Task<GrnItem?> GetById(long id, bool track);
        Task<GrnItem?> GetByCodeAsync(string code, bool track);
        Task<GrnItem> CreateAsync(GrnItem item);
        Task UpdateAsync();
        Task DeleteAsync(GrnItem item);
        Task<GrnItem?> UpdateStateAsync(string code, State newState);
    }
}
