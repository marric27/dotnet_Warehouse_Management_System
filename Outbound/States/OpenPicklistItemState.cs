using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Outbound.Dtos;

namespace dotnet_Warehouse_Management_System.Outbound.States
{
    public class OpenPicklistItemState : IPicklistItemStateHandler
    {
        public PicklistItemState State => PicklistItemState.OPEN;

        public bool CanTransitionTo(PicklistItemState targetState, PicklistItemDto item, int pickedQtyAfterUpdate)
        {
            return targetState == PicklistItemState.PICKED && pickedQtyAfterUpdate == item.Qty;
        }

        public PicklistItemState OnPickingConfirmed(PicklistItemDto item, int pickedQtyAfterUpdate)
        {
            return CanTransitionTo(PicklistItemState.PICKED, item, pickedQtyAfterUpdate)
                ? PicklistItemState.PICKED
                : PicklistItemState.OPEN;
        }
    }
}
