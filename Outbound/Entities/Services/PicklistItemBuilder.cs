using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Outbound.Dtos;
using dotnet_Warehouse_Management_System.Warehouses.Entities.Dtos;

namespace dotnet_Warehouse_Management_System.Outbound.Entities.Services
{
    public class PicklistItemBuilder
    {
        private readonly PicklistItemDto _item = new()
        {
            Code = $"IT-{Guid.NewGuid():N}".Substring(0, 12).ToUpper(),
            State = PicklistItemState.OPEN,
            PickedQty = 0
        };

        public PicklistItemBuilder FromOrderLine(SalesOrderLineResponseDto line, string orderCode)
        {
            _item.ProductCode = line.productCode;
            _item.Qty = line.quantity;
            _item.SalesOrderCode = orderCode;
            _item.SalesOrderLineNumber = line.salesOrderLineNumber;
            return this;
        }

        public PicklistItemBuilder WithSlotInfo(SlotResponseDto slot)
        {
            _item.SlotCode = slot.Code;
            _item.PickingSequence = slot.PickingSequence;
            return this;
        }

        public PicklistItemDto Build() => _item;
    }
}
