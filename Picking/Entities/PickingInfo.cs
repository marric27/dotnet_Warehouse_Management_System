using dotnet_Warehouse_Management_System.Outbound.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet_Warehouse_Management_System.Picking.Entities
{
    public class PickingInfo
    {
        public long Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string User {  get; set; }
        public string StockUnitCode { get; set; }
        public string BatchNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int Quantity { get; set; }
        public long PickListItemId { get; set; }
        public PicklistItem PickListItem { get; set; }

    }
}
