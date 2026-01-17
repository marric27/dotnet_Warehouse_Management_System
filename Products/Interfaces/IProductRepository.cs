using dotnet_Warehouse_Management_System.Products.Dtos;
using dotnet_Warehouse_Management_System.Products.Helpers;
using dotnet_Warehouse_Management_System.Products.Model;

namespace dotnet_Warehouse_Management_System.Products.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync(QueryObject query);
        Task<Product?> GetByCodeAsync(string code);
        Task<Product> CreateAsync(Product product);
        Task<Product> UpdateAsync(string code, ProductRequestDto productDto);
        Task<Product?> DeleteAsync(string code);
    }
}
