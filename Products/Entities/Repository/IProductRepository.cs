using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Products.Entities.Dtos;

namespace dotnet_Warehouse_Management_System.Products.Entities.Repository
{
    public interface IProductRepository
    {
        Task<Page<Product>> GetAllPaginatedAsync(QueryObject query);
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetByCodeAsync(string code);
        Task<Product> CreateAsync(Product product);
        Task UpdateAsync();
        Task DeleteAsync(string code);
    }
}
