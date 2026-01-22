using dotnet_Warehouse_Management_System.Common;

namespace dotnet_Warehouse_Management_System.Outbound.Dtos
{
    public class PicklistItemDto
    {
        public long Id { get; set; }
        public string code { get; set; }
        public string productCode { get; set; }
        public PicklistItemState State { get; set; }
        public int qty { get; set; }
        public int PickedQty { get; set; }
        public int PickingSequence { get; set; }
        public ErrorReason ErrorReason { get; set; }
        public string SlotCode { get; set; }
        public string salesOrderCode { get; set; }
        public int salesOrderLineNumber { get; set; }
    }
}
