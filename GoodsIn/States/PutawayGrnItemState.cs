using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.GoodsIn.Dtos;

namespace dotnet_Warehouse_Management_System.GoodsIn.States
{
    public class PutawayGrnItemState : IGrnItemStateHandler
    {
        public State State => State.PUTAWAY;

        public bool CanTransitionTo(State targetState, GrnItemResponseDto item)
        {
            return targetState == Common.State.PUTAWAY;
        }

        public State OnCheckingInfoAdded(GrnItemResponseDto item)
        {
            return Common.State.PUTAWAY;
        }

        public State OnPutawayAssigned(GrnItemResponseDto item)
        {
            return Common.State.PUTAWAY;
        }
    }
}
