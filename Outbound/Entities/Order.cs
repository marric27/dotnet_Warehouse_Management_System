namespace dotnet_Warehouse_Management_System.Outbound.Entities
{
    public class Order
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public DateTime date { get; set; }
        public string CustomerCode { get; set; }
        public OrderState State { get; set; }
        public List<SalesOrderLine> SalesOrderLines { get; set; } = new List<SalesOrderLine>();
        public void GenerateCode()
        {
            Code = $"ORD-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";
        }

    }
}
