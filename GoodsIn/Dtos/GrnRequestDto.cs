using dotnet_Warehouse_Management_System.Common;
using System.ComponentModel.DataAnnotations;

namespace dotnet_Warehouse_Management_System.GoodsIn.Dtos
{
    public class GrnRequestDto
    {
        [Required]
        public required string Supplier { get; set; }
        [Required]
        public DateTime ReceivingDate { get; set; }
    }
}
