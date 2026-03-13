using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.GoodsIn.Dtos;

namespace dotnet_Warehouse_Management_System.GoodsIn.States
{
    public class CheckedGrnItemState : IGrnItemStateHandler
    {
        public State State => State.CHECKED;

        public bool CanTransitionTo(State targetState, GrnItemResponseDto item)
        {
            return targetState == Common.State.PUTAWAY
                && item.CheckingInfoList is not null
                && item.CheckingInfoList.Count != 0
                && item.CheckingInfoList.All(c => c.State == Common.State.PUTAWAY);
        }

        public State OnCheckingInfoAdded(GrnItemResponseDto item)
        {
            return CanTransitionTo(Common.State.PUTAWAY, item) ? Common.State.PUTAWAY : Common.State.CHECKED;
        }

        public State OnPutawayAssigned(GrnItemResponseDto item)
        {
            return OnCheckingInfoAdded(item);
        }
    }
}
