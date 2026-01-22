using dotnet_Warehouse_Management_System.Common;
using System;

namespace dotnet_Warehouse_Management_System.Outbound.Entities
{
    public class PicklistItem
    {
        public long Id { get; set; }
        public string code { get; set; }
        public string ProductCode { get; set; }
        public PicklistItemState State { get; set; }
        public int qty { get; set; }
        public int PickedQty { get; set; }
        public int PickingSequence { get; set; }
        public ErrorReason ErrorReason { get; set; }
        public string SlotCode { get; set; }
        public string SalesOrderCode { get; set; }
        public int SalesOrderLineNumber { get; set; }
        public long PicklistId { get; set; }
        public Picklist Picklist { get; set; }

    }
}
