using dotnet_Warehouse_Management_System.Common;

namespace dotnet_Warehouse_Management_System.GoodsIn.Entities
{
    public class CheckingInfo
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string BatchNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int Quantity { get; set; }
        public State State { get; set; }

        // relazioni con grnitem e stockunit

        public void GenerateCode()
        {
            Code = $"CI-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";
        }
    }
}
