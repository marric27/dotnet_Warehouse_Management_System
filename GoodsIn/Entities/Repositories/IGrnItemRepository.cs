using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.GoodsIn.Dtos;
using dotnet_Warehouse_Management_System.Products.Entities;
using dotnet_Warehouse_Management_System.Products.Entities.Dtos;

namespace dotnet_Warehouse_Management_System.GoodsIn.Entities.Repositories
{
    public interface IGrnItemRepository
    {
        Task<List<GrnItem>> GetAllAsync(QueryObject query);
        Task<GrnItem?> GetByCodeAsync(string code);
        Task<GrnItem> CreateAsync(GrnItem item);
        Task<GrnItem> UpdateAsync(string code, GrnItemRequestDto itemDto);
        Task<GrnItem?> DeleteAsync(string code);
    }
}
