using dotnet_Warehouse_Management_System.Common;
using System.ComponentModel.DataAnnotations;

namespace dotnet_Warehouse_Management_System.Products.Entities.Dtos
{
    public class ProductRequestDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Name must be at least 5 characters long")]
        [MaxLength(20, ErrorMessage = "Name cannot be over 20 characters")]
        public required string Name { get; set; }
        [Required]
        public Category Category { get; set; }
    }
}
