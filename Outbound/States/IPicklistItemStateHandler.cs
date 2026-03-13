using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Outbound.Dtos;

namespace dotnet_Warehouse_Management_System.Outbound.States
{
    public interface IPicklistItemStateHandler
    {
        PicklistItemState State { get; }
        bool CanTransitionTo(PicklistItemState targetState, PicklistItemDto item, int pickedQtyAfterUpdate);
        PicklistItemState OnPickingConfirmed(PicklistItemDto item, int pickedQtyAfterUpdate);
    }
}
