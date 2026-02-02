using dotnet_Warehouse_Management_System.Common;

namespace dotnet_Warehouse_Management_System.Outbound.Entities
{
    public class Picklist
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string ReleaseNumber { get; set; }
        public string CustomerCode { get; set; }
        public PicklistState State { get; set; }
        public List<PicklistItem> PicklistItemList { get; set; } = [];

    }
}
