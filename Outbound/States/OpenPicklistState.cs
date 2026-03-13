using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Outbound.Dtos;
using dotnet_Warehouse_Management_System.Outbound.Entities;

namespace dotnet_Warehouse_Management_System.Outbound.States
{
    public class OpenPicklistState : IPicklistStateHandler
    {
        public PicklistState State => PicklistState.OPEN;

        public bool CanTransitionTo(PicklistState targetState, PicklistDto picklist)
        {
            return targetState == PicklistState.CLOSED && picklist.pickListItemList.All(i => i.State == PicklistItemState.PICKED);
        }

        public PicklistState OnPicklistItemUpdated(PicklistDto picklist)
        {
            return CanTransitionTo(PicklistState.CLOSED, picklist) ? PicklistState.CLOSED : PicklistState.OPEN;
        }
    }
}
