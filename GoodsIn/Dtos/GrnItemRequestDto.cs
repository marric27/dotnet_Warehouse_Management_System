using System.ComponentModel.DataAnnotations;

namespace dotnet_Warehouse_Management_System.GoodsIn.Dtos
{
    public class GrnItemRequestDto
    {
        [Required]
        public required string ProductCode { get; set; }
        [Required]
        public int ExpectedQty { get; set; }
        [Required]
        public int ReceivedQty { get; set; }
        [Required]
        public int CompliantQty { get; set; }
        [Required]
        public int NotCompliantQty { get; set; }
        public string? Notes { get; set; }
    }
}
