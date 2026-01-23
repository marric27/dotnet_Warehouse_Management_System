using dotnet_Warehouse_Management_System.Outbound.Entities;

namespace dotnet_Warehouse_Management_System.Picking.Dtos
{
    public class PickingInfoDto
    {
        public long Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string User { get; set; }
        public string StockUnitCode { get; set; }
        public string BatchNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int Quantity { get; set; }
        public long PicklistItemId { get; set; }
        public string PicklistItemCode { get; set; }
    }
}
