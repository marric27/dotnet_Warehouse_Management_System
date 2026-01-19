using dotnet_Warehouse_Management_System.Common;

namespace dotnet_Warehouse_Management_System.GoodsIn.Entities
{
    public class Grn
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Supplier { get; set; }
        public State State { get; set; }
        public DateTime ReceivingDate { get; set; }
        public List<GrnItem> Items { get; set; }

        public void GenerateCode()
        {
            Code = $"GRN-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";
        }

    }
}
