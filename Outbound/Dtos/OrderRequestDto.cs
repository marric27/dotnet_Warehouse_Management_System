using System.ComponentModel.DataAnnotations;

namespace dotnet_Warehouse_Management_System.Outbound.Dtos
{
    public class OrderRequestDto
    {
        [Required]
        public string CustomerCode { get; set; }
        [Required]
        public List<SalesOrderLineRequestDto> SalesOrderLineList;
    }
}
