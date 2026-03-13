using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Outbound.Dtos;

namespace dotnet_Warehouse_Management_System.Outbound.States
{
    public class PickedPicklistItemState : IPicklistItemStateHandler
    {
        public PicklistItemState State => PicklistItemState.PICKED;

        public bool CanTransitionTo(PicklistItemState targetState, PicklistItemDto item, int pickedQtyAfterUpdate)
        {
            return targetState == PicklistItemState.PICKED;
        }

        public PicklistItemState OnPickingConfirmed(PicklistItemDto item, int pickedQtyAfterUpdate)
        {
            return PicklistItemState.PICKED;
        }
    }
}
