using dotnet_Warehouse_Management_System.Common;

namespace dotnet_Warehouse_Management_System.GoodsIn.Dtos
{
    public class GrnItemResponseDto
    {
        public string Code { get; set; }
        public string ProductCode { get; set; }
        public int ExpectedQty { get; set; }
        public int ReceivedQty { get; set; }
        public int CompliantQty { get; set; }
        public int NotCompliantQty { get; set; }
        public State State { get; set; }
        public string Notes { get; set; }
    }
}
