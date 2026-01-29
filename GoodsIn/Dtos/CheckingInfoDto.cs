using dotnet_Warehouse_Management_System.Common;

namespace dotnet_Warehouse_Management_System.GoodsIn.Dtos
{
    public class CheckingInfoDto
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string BatchNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int Quantity { get; set; }
        public State State { get; set; }
        public long StockUnitId { get; set; }
        public long GrnItemId { get; set; }

    }
}
