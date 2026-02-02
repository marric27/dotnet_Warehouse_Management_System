using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Products.Entities.Dtos;

namespace dotnet_Warehouse_Management_System.Products.Entities.Services
{
    public interface IProductService
    {
        Task<ProductResponseDto> CreateAsync(ProductRequestDto productDto);
        Task<List<ProductResponseDto>> GetAllAsync();
        Task<Page<ProductResponseDto>> GetAllPaginatedAsync(QueryObject query);
        Task<ProductResponseDto?> GetByCodeAsync(string code);
        Task<ProductResponseDto?> UpdateAsync(string code, ProductRequestDto productDto);
        Task<bool> DeleteAsync(string code);
    }
}
