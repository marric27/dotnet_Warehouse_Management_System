using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.GoodsIn.Dtos;

namespace dotnet_Warehouse_Management_System.GoodsIn.States
{
    public interface IGrnItemStateHandler
    {
        State State { get; }
        bool CanTransitionTo(State targetState, GrnItemResponseDto item);
        State OnCheckingInfoAdded(GrnItemResponseDto item);
        State OnPutawayAssigned(GrnItemResponseDto item);
    }
}
