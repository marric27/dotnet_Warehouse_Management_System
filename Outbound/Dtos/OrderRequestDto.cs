using dotnet_Warehouse_Management_System.Outbound.Entities;

namespace dotnet_Warehouse_Management_System.Outbound.Dtos
{
    public class OrderRequestDto
    {
        public DateTime Date { get; set; }
        public string CustomerCode { get; set; }
        public OrderState State { get; set; }
        public List<SalesOrderLineRequestDto> SalesOrderLineList { get; set; } = new List<SalesOrderLineRequestDto>();
    }
}
