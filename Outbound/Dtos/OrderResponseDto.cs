using dotnet_Warehouse_Management_System.Outbound.Entities;
using System.ComponentModel.DataAnnotations;

namespace dotnet_Warehouse_Management_System.Outbound.Dtos
{
    public class OrderResponseDto
    {
        public long id { get; set; }
        public string code { get; set; }
        public DateTime date { get; set; }
        public string customerCode { get; set; }
        public OrderState state { get; set; }
        public List<SalesOrderLineResponseDto> salesOrderLineList { get; set; } = [];


    }
}
