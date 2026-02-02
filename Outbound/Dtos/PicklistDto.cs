using dotnet_Warehouse_Management_System.Outbound.Entities;

namespace dotnet_Warehouse_Management_System.Outbound.Dtos
{
    public class PicklistDto
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string ReleaseNumber { get; set; }
        public string CustomerCode { get; set; }
        public PicklistState State { get; set; }
        public List<PicklistItemDto> pickListItemList { get; set; } = [];
    }
}
