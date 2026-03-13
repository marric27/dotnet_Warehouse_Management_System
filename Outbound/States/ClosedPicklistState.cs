using dotnet_Warehouse_Management_System.Outbound.Dtos;
using dotnet_Warehouse_Management_System.Outbound.Entities;

namespace dotnet_Warehouse_Management_System.Outbound.States
{
    public class ClosedPicklistState : IPicklistStateHandler
    {
        public PicklistState State => PicklistState.CLOSED;

        public bool CanTransitionTo(PicklistState targetState, PicklistDto picklist)
        {
            return targetState == PicklistState.CLOSED;
        }

        public PicklistState OnPicklistItemUpdated(PicklistDto picklist)
        {
            return PicklistState.CLOSED;
        }
    }
}
