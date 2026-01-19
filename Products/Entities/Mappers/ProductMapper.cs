using dotnet_Warehouse_Management_System.Products.Entities.Dtos;

namespace dotnet_Warehouse_Management_System.Products.Entities.Mappers
{
    public static class ProductMapper
    {
        public static ProductResponseDto ToResponseDto(this Product product)
        {
            return new ProductResponseDto
            {
                Code = product.Code,
                Name = product.Name,
                Category = product.Category,
            };
        }

        public static Product ToProduct(this ProductRequestDto dto)
        {
            return new Product
            {
                Name = dto.Name,
                Category = dto.Category,
            };
        }
    }
}
