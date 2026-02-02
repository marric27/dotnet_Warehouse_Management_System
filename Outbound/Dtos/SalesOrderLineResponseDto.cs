using dotnet_Warehouse_Management_System.Outbound.Entities;

namespace dotnet_Warehouse_Management_System.Outbound.Dtos
{
    public class SalesOrderLineResponseDto
    {
        public long id { get; set; }
        public int salesOrderLineNumber { get; set; }
        public string productCode { get; set; }
        public int quantity { get; set; }
        public OrderState status { get; set; }
    }
}
