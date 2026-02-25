using System.ComponentModel.DataAnnotations;

namespace dotnet_Warehouse_Management_System.Outbound.Dtos
{
    public class SalesOrderLineRequestDto
    {
        [Required]
        [MinLength(3)]
        public string productCode { get; set; }

        [Range(1, int.MaxValue)]
        public int quantity { get; set; }
    }
}
