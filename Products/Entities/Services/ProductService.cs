using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Products.Entities.Dtos;
using dotnet_Warehouse_Management_System.Products.Entities.Mappers;
using dotnet_Warehouse_Management_System.Products.Entities.Repository;

namespace dotnet_Warehouse_Management_System.Products.Entities.Services
{
    public class ProductService(IProductRepository productRepository, ILogger<ProductService> logger) : IProductService
    {
        public async Task<List<ProductResponseDto>> GetAllAsync()
        {
            logger.LogInformation("Getting all products");
            var products = await productRepository.GetAllAsync();
            return products.Select(p => p.ToResponseDto()).ToList();
        }

        public async Task<Page<ProductResponseDto>> GetAllPaginatedAsync(QueryObject query)
        {
            var products = await productRepository.GetAllPaginatedAsync(query);
            return products.Map(p => p.ToResponseDto());
        }

        public async Task<ProductResponseDto?> GetByCodeAsync(string code)
        {
            var product = await productRepository.GetByCodeAsync(code, false);
            return product?.ToResponseDto();
        }

        public async Task<ProductResponseDto> CreateAsync(ProductRequestDto productDto)
        {
            var product = productDto.ToProduct();
            product.GenerateCode();

            var created = await productRepository.CreateAsync(product);
            return created.ToResponseDto();
        }

        public async Task<ProductResponseDto?> UpdateAsync(string code, ProductRequestDto productDto)
        {
            var existingProduct = await productRepository.GetByCodeAsync(code, true) ?? throw new KeyNotFoundException();
            existingProduct.Name = productDto.Name;
            existingProduct.Category = productDto.Category;
            await productRepository.UpdateAsync();
            return existingProduct.ToResponseDto();
        }

        public async Task<bool> DeleteAsync(string code)
        {
            var product = await productRepository.GetByCodeAsync(code, true);
            if (product == null) return false;

            await productRepository.DeleteAsync(product);
            return true;
        }
    }
}
