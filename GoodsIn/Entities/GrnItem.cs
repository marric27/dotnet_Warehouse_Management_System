using dotnet_Warehouse_Management_System.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet_Warehouse_Management_System.GoodsIn.Entities
{
    public class GrnItem
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string ProductCode { get; set; }
        public int ExpectedQty { get; set; }
        public int ReceivedQty { get; set; }
        public int CompliantQty { get; set; }
        public int NotCompliantQty { get; set; }
        public State State { get; set; }
        public string? Notes { get; set; }
        public long GrnId { get; set; }
        public Grn Grn { get; set; }


        //public List<CheckingInfo> checkingInfoList;

        public void GenerateCode()
        {
            Code = $"Item-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";
        }
    }
}
