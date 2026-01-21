using System.ComponentModel.DataAnnotations;

namespace dotnet_Warehouse_Management_System.Outbound.Dtos
{
    public class SalesOrderLineRequestDto
    {
        [Required]
        public string productCode { get; set; }
        [Required]
        public int quantity { get; set; }
    }
}
