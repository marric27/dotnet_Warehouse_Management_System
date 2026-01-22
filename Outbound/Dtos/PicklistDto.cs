namespace dotnet_Warehouse_Management_System.Outbound.Dtos
{
    public class PicklistDto
    {
        public string Code { get; set; }
        public string ReleaseNumber { get; set; }
        public string CustomerCode { get; set; }
        public List<PicklistItemDto> PicklistItemList { get; set; } = [];
    }
}
