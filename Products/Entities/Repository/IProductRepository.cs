using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Products.Entities.Dtos;

namespace dotnet_Warehouse_Management_System.Products.Entities.Repository
{
    public interface IProductRepository
    {
        Task<Page<Product>> GetAllAsync(QueryObject query);
        Task<Product?> GetByCodeAsync(string code);
        Task<Product> CreateAsync(Product product);
        Task<Product> UpdateAsync(string code, ProductRequestDto productDto);
        Task<Product?> DeleteAsync(string code);
    }
}
