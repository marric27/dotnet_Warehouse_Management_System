using dotnet_Warehouse_Management_System.Outbound.Entities;
using System.ComponentModel.DataAnnotations;

namespace dotnet_Warehouse_Management_System.Outbound.Dtos
{
    public class OrderRequestDto
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        [MinLength(3)]
        public string CustomerCode { get; set; }

        [Required]
        public OrderState State { get; set; }

        [Required]
        [MinLength(1)]
        public List<SalesOrderLineRequestDto> SalesOrderLineList { get; set; } = new List<SalesOrderLineRequestDto>();
    }
}
