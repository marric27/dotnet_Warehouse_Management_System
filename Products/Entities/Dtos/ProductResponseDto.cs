using dotnet_Warehouse_Management_System.Common;

namespace dotnet_Warehouse_Management_System.Products.Entities.Dtos
{
    public class ProductResponseDto
    {
        public required string Code { get; set; }
        public required string Name { get; set; }
        public Category Category { get; set; }
    }
}
