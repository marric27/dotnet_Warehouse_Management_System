using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.GoodsIn.Dtos;

namespace dotnet_Warehouse_Management_System.GoodsIn.States
{
    public class OpenGrnItemState : IGrnItemStateHandler
    {
        public State State => State.OPEN;

        public bool CanTransitionTo(State targetState, GrnItemResponseDto item)
        {
            int assigned = item.CheckingInfoList?.Sum(ci => ci.Quantity) ?? 0;
            return targetState == Common.State.CHECKED && assigned >= item.ReceivedQty && item.ReceivedQty > 0;
        }

        public State OnCheckingInfoAdded(GrnItemResponseDto item)
        {
            return CanTransitionTo(Common.State.CHECKED, item) ? Common.State.CHECKED : Common.State.OPEN;
        }

        public State OnPutawayAssigned(GrnItemResponseDto item)
        {
            return OnCheckingInfoAdded(item);
        }
    }
}
