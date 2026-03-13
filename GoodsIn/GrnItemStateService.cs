using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Exceptions;
using dotnet_Warehouse_Management_System.GoodsIn.Dtos;
using dotnet_Warehouse_Management_System.GoodsIn.Services;
using dotnet_Warehouse_Management_System.GoodsIn.States;

namespace dotnet_Warehouse_Management_System.GoodsIn
{
    public class GrnItemStateService(IGrnService grnService, IGrnItemService grnItemService, IGrnItemStateHandlerResolver stateHandlerResolver) : IGrnItemStateService
    {
        public async Task EvaluateAndProgressGrnItemStateAsync(GrnItemResponseDto item)
        {
            State currentState = item.State;
            IGrnItemStateHandler currentHandler = stateHandlerResolver.Resolve(currentState);

            State nextState = currentHandler.OnCheckingInfoAdded(item);
            if (nextState != currentState && currentHandler.CanTransitionTo(nextState, item))
            {
                item.State = nextState;
                await grnItemService.UpdateAsync(item);
                currentState = nextState;
            }

            currentHandler = stateHandlerResolver.Resolve(currentState);
            nextState = currentHandler.OnPutawayAssigned(item);
            if (nextState != currentState && currentHandler.CanTransitionTo(nextState, item))
            {
                item.State = nextState;
                await grnItemService.UpdateAsync(item);
            }

            if (item.State == State.PUTAWAY)
            {
                await EvaluateAndProgressGrnStateAsync(item.GrnId);
            }
        }

        public async Task EvaluateAndProgressGrnStateAsync(long grnId)
        {
            var grn = await grnService.GetByIdAsync(grnId) ?? throw new KeyNotFoundException("GRN not found");

            bool allPutaway = grn.Items.All(i => i.State == State.PUTAWAY);

            if (allPutaway)
            {
                grn.State = State.CLOSED;
                await grnService.UpdateAsync(grn);
            }
        }

        public void ValidateItemQuantities(GrnItemRequestDto item)
        {
            int expected = item.ExpectedQty;
            int compliant = item.CompliantQty;
            int notCompliant = item.NotCompliantQty;
            int received = item.ReceivedQty;

            if (received == 0) item.State = State.PUTAWAY;
            else item.State = State.OPEN;

            if (expected <= 0)
                throw new ArgumentException("Expected qty must be > 0");

            if (received != compliant + notCompliant)
                throw new ArgumentException("Received != compliant + notCompliant");

            if (received > expected)
                throw new DomainConflictException("Over-received: expected=" + expected + " received=" + received);
        }
    }
}
