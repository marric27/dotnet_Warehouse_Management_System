using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.GoodsIn.Entities;

namespace dotnet_Warehouse_Management_System.GoodsIn.Dtos
{
    public class GrnResponseDto
    {
        public string Code { get; set; }
        public string Supplier { get; set; }
        public State State { get; set; }
        public DateTime ReceivingDate { get; set; }
        public List<GrnItem> Items { get; set; }
    }
}
