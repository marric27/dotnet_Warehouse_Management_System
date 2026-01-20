using dotnet_Warehouse_Management_System.Common.Helpers;

namespace dotnet_Warehouse_Management_System.GoodsIn.Entities.Repositories
{
    public interface IGrnItemRepository
    {
        Task<List<GrnItem>> GetAllAsync(QueryObject query);
        Task<GrnItem?> GetById(long id);
        Task<GrnItem?> GetByCodeAsync(string code);
        Task<GrnItem> CreateAsync(GrnItem item);
        Task<GrnItem> UpdateAsync(string code, GrnItem item);
        Task<GrnItem?> DeleteAsync(string code);
    }
}
