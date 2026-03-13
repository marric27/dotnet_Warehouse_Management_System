using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.GoodsIn.Dtos;
using dotnet_Warehouse_Management_System.GoodsIn.States;

namespace dotnet_Warehouse_Management_System.Tests.StateHandlers
{
    public class GrnItemStateHandlersTests
    {
        [Fact]
        public void OpenState_ShouldTransitionToChecked_WhenAllReceivedQtyIsAssigned()
        {
            var handler = new OpenGrnItemState();
            var item = new GrnItemResponseDto
            {
                State = State.OPEN,
                ReceivedQty = 10,
                CheckingInfoList = [new CheckingInfoDto { Quantity = 7 }, new CheckingInfoDto { Quantity = 3 }]
            };

            State next = handler.OnCheckingInfoAdded(item);

            Assert.Equal(State.CHECKED, next);
            Assert.True(handler.CanTransitionTo(State.CHECKED, item));
        }

        [Fact]
        public void OpenState_ShouldStayOpen_WhenAssignedQtyIsLowerThanReceived()
        {
            var handler = new OpenGrnItemState();
            var item = new GrnItemResponseDto
            {
                State = State.OPEN,
                ReceivedQty = 10,
                CheckingInfoList = [new CheckingInfoDto { Quantity = 5 }]
            };

            State next = handler.OnCheckingInfoAdded(item);

            Assert.Equal(State.OPEN, next);
            Assert.False(handler.CanTransitionTo(State.CHECKED, item));
        }

        [Fact]
        public void CheckedState_ShouldTransitionToPutaway_WhenAllCheckingInfoArePutaway()
        {
            var handler = new CheckedGrnItemState();
            var item = new GrnItemResponseDto
            {
                State = State.CHECKED,
                CheckingInfoList =
                [
                    new CheckingInfoDto { State = State.PUTAWAY, Quantity = 4 },
                    new CheckingInfoDto { State = State.PUTAWAY, Quantity = 6 }
                ]
            };

            State next = handler.OnPutawayAssigned(item);

            Assert.Equal(State.PUTAWAY, next);
            Assert.True(handler.CanTransitionTo(State.PUTAWAY, item));
        }

        [Fact]
        public void CheckedState_ShouldStayChecked_WhenAnyCheckingInfoIsNotPutaway()
        {
            var handler = new CheckedGrnItemState();
            var item = new GrnItemResponseDto
            {
                State = State.CHECKED,
                CheckingInfoList =
                [
                    new CheckingInfoDto { State = State.CHECKED, Quantity = 4 },
                    new CheckingInfoDto { State = State.PUTAWAY, Quantity = 6 }
                ]
            };

            State next = handler.OnPutawayAssigned(item);

            Assert.Equal(State.CHECKED, next);
            Assert.False(handler.CanTransitionTo(State.PUTAWAY, item));
        }

        [Fact]
        public void PutawayState_ShouldRemainPutaway_ForEveryEvent()
        {
            var handler = new PutawayGrnItemState();
            var item = new GrnItemResponseDto { State = State.PUTAWAY, ReceivedQty = 1 };

            Assert.Equal(State.PUTAWAY, handler.OnCheckingInfoAdded(item));
            Assert.Equal(State.PUTAWAY, handler.OnPutawayAssigned(item));
            Assert.False(handler.CanTransitionTo(State.CHECKED, item));
        }
    }
}
