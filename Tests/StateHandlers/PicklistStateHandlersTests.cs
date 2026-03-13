using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Outbound.Dtos;
using dotnet_Warehouse_Management_System.Outbound.Entities;
using dotnet_Warehouse_Management_System.Outbound.States;

namespace dotnet_Warehouse_Management_System.Tests.StateHandlers
{
    public class PicklistStateHandlersTests
    {
        [Fact]
        public void OpenPicklistItem_ShouldTransitionToPicked_WhenPickedQtyMatchesRequiredQty()
        {
            var handler = new OpenPicklistItemState();
            var item = new PicklistItemDto { State = PicklistItemState.OPEN, Qty = 10, PickedQty = 4 };

            PicklistItemState next = handler.OnPickingConfirmed(item, pickedQtyAfterUpdate: 10);

            Assert.Equal(PicklistItemState.PICKED, next);
            Assert.True(handler.CanTransitionTo(PicklistItemState.PICKED, item, 10));
        }

        [Fact]
        public void OpenPicklistItem_ShouldRemainOpen_WhenPickedQtyIsPartial()
        {
            var handler = new OpenPicklistItemState();
            var item = new PicklistItemDto { State = PicklistItemState.OPEN, Qty = 10, PickedQty = 4 };

            PicklistItemState next = handler.OnPickingConfirmed(item, pickedQtyAfterUpdate: 8);

            Assert.Equal(PicklistItemState.OPEN, next);
            Assert.False(handler.CanTransitionTo(PicklistItemState.PICKED, item, 8));
        }

        [Fact]
        public void OpenPicklist_ShouldTransitionToClosed_WhenAllItemsArePicked()
        {
            var handler = new OpenPicklistState();
            var picklist = new PicklistDto
            {
                State = PicklistState.OPEN,
                pickListItemList =
                [
                    new PicklistItemDto { State = PicklistItemState.PICKED },
                    new PicklistItemDto { State = PicklistItemState.PICKED }
                ]
            };

            PicklistState next = handler.OnPicklistItemUpdated(picklist);

            Assert.Equal(PicklistState.CLOSED, next);
            Assert.True(handler.CanTransitionTo(PicklistState.CLOSED, picklist));
        }

        [Fact]
        public void OpenPicklist_ShouldRemainOpen_WhenAtLeastOneItemIsOpen()
        {
            var handler = new OpenPicklistState();
            var picklist = new PicklistDto
            {
                State = PicklistState.OPEN,
                pickListItemList =
                [
                    new PicklistItemDto { State = PicklistItemState.PICKED },
                    new PicklistItemDto { State = PicklistItemState.OPEN }
                ]
            };

            PicklistState next = handler.OnPicklistItemUpdated(picklist);

            Assert.Equal(PicklistState.OPEN, next);
            Assert.False(handler.CanTransitionTo(PicklistState.CLOSED, picklist));
        }
    }
}
