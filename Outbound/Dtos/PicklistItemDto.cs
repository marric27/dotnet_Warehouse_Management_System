using dotnet_Warehouse_Management_System.Common;

namespace dotnet_Warehouse_Management_System.Outbound.Dtos
{
    public class PicklistItemDto
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string ProductCode { get; set; }
        public PicklistItemState State { get; set; }
        public int Qty { get; set; }
        public int PickedQty { get; set; }
        public int PickingSequence { get; set; }
        public ErrorReason ErrorReason { get; set; }
        public string SlotCode { get; set; }
        public string SalesOrderCode { get; set; }
        public int SalesOrderLineNumber { get; set; }
    }
}
