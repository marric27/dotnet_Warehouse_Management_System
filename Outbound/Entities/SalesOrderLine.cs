namespace dotnet_Warehouse_Management_System.Outbound.Entities
{
    public class SalesOrderLine
    {
        public long Id { get; set; }
        public int SalesOrderLineNumber { get; set; }
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
        public OrderState Status { get; set; }
        public long OrderId { get; set; }
        public Order Order { get; set; }
    }
}
