using dotnet_Warehouse_Management_System.BaseRepository;
using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;

namespace dotnet_Warehouse_Management_System.Products.Entities.Repository
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<Page<Product>> GetAllPaginatedAsync(QueryObject query);
        Task<Product?> GetByCodeAsync(string code, bool track = false);
    }
}
