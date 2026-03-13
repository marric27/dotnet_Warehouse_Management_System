using dotnet_Warehouse_Management_System.Outbound.Dtos;
using dotnet_Warehouse_Management_System.Outbound.Entities;

namespace dotnet_Warehouse_Management_System.Outbound.States
{
    public interface IPicklistStateHandler
    {
        PicklistState State { get; }
        bool CanTransitionTo(PicklistState targetState, PicklistDto picklist);
        PicklistState OnPicklistItemUpdated(PicklistDto picklist);
    }
}
