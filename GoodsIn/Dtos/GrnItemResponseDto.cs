using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.GoodsIn.Entities;

namespace dotnet_Warehouse_Management_System.GoodsIn.Dtos
{
    public class GrnItemResponseDto
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string ProductCode { get; set; }
        public int ExpectedQty { get; set; }
        public int ReceivedQty { get; set; }
        public int CompliantQty { get; set; }
        public int NotCompliantQty { get; set; }
        public State State { get; set; }
        public string Notes { get; set; }
        public List<CheckingInfoDto> checkingInfoList { get; set; } = new();
        public long GrnId { get; set; }
    }
}
