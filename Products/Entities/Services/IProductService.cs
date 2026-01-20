using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Products.Entities.Dtos;

namespace dotnet_Warehouse_Management_System.Products.Entities.Services
{
    public interface IProductService
    {
        Task<Page<ProductResponseDto>> GetAllAsync(QueryObject query);
        Task<ProductResponseDto?> GetByCodeAsync(string code);
        Task<ProductResponseDto> CreateAsync(ProductRequestDto productDto);
        Task<ProductResponseDto?> UpdateAsync(string code, ProductRequestDto productDto);
        Task<ProductResponseDto?> DeleteAsync(string code);
    }
}
