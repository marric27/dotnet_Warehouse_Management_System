using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Outbound.Dtos;
using dotnet_Warehouse_Management_System.Outbound.Entities;

namespace dotnet_Warehouse_Management_System.Outbound.States
{
    public interface IPicklistStateWorkflowService
    {
        PicklistItemState EvaluatePicklistItemState(PicklistItemDto item, int pickedQtyAfterUpdate);
        PicklistState EvaluatePicklistState(PicklistDto picklist);
    }

    public class PicklistStateWorkflowService(
        IPicklistItemStateHandlerResolver picklistItemStateHandlerResolver,
        IPicklistStateHandlerResolver picklistStateHandlerResolver) : IPicklistStateWorkflowService
    {
        public PicklistItemState EvaluatePicklistItemState(PicklistItemDto item, int pickedQtyAfterUpdate)
        {
            var handler = picklistItemStateHandlerResolver.Resolve(item.State);
            PicklistItemState nextState = handler.OnPickingConfirmed(item, pickedQtyAfterUpdate);
            return handler.CanTransitionTo(nextState, item, pickedQtyAfterUpdate) ? nextState : item.State;
        }

        public PicklistState EvaluatePicklistState(PicklistDto picklist)
        {
            var handler = picklistStateHandlerResolver.Resolve(picklist.State);
            PicklistState nextState = handler.OnPicklistItemUpdated(picklist);
            return handler.CanTransitionTo(nextState, picklist) ? nextState : picklist.State;
        }
    }
}
