using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Products.Entities.Dtos;
using dotnet_Warehouse_Management_System.Products.Entities.Mappers;
using dotnet_Warehouse_Management_System.Products.Entities.Repository;

namespace dotnet_Warehouse_Management_System.Products.Entities.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Page<ProductResponseDto>> GetAllAsync(QueryObject query)
        {
            var products = await _productRepository.GetAllAsync(query);
            return products.Map(p => p.ToResponseDto());
        }

        public async Task<ProductResponseDto?> GetByCodeAsync(string code)
        {
            var product = await _productRepository.GetByCodeAsync(code);
            return product?.ToResponseDto();
        }

        public async Task<ProductResponseDto> CreateAsync(ProductRequestDto productDto)
        {
            var product = productDto.ToProduct();
            product.GenerateCode();

            var created = await _productRepository.CreateAsync(product);
            return created.ToResponseDto();
        }

        public async Task<ProductResponseDto?> UpdateAsync(string code, ProductRequestDto productDto)
        {
            var updated = await _productRepository.UpdateAsync(code, productDto);
            return updated?.ToResponseDto();
        }

        public async Task<ProductResponseDto?> DeleteAsync(string code)
        {
            var deleted = await _productRepository.DeleteAsync(code);
            return deleted?.ToResponseDto();
        }
    }
}
