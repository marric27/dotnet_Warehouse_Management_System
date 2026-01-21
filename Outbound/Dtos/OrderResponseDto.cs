using dotnet_Warehouse_Management_System.Outbound.Entities;
using System.ComponentModel.DataAnnotations;

namespace dotnet_Warehouse_Management_System.Outbound.Dtos
{
    public class OrderResponseDto
    {
        public string Code { get; set; }
        public DateTime Date { get; set; }
        public string CustomerCode { get; set; }
        public OrderState State { get; set; }
        public List<SalesOrderLineResponseDto> SalesOrderLines { get; set; } = new List<SalesOrderLineResponseDto>();


    }
}
