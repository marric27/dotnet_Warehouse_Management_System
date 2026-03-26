using dotnet_Warehouse_Management_System.GoodsIn.Services;

namespace dotnet_Warehouse_Management_System.GoodsIn.Events
{
    public class GoodsInEventPublisher(IGrnItemService grnItemService, IGrnItemStateService grnItemStateService) : IEventPublisher
    {
        public async Task PublishAsync<TEvent>(TEvent evt, CancellationToken ct = default)
            where TEvent : IGoodsInDomainEvent
        {
            switch (evt)
            {
                case GrnItemCreatedEvent grnItemCreatedEvent:
                    await EvaluateByGrnItemIdAsync(grnItemCreatedEvent.GrnItemId);
                    break;
                case CheckingInfoCreatedEvent checkingInfoCreatedEvent:
                    await EvaluateByGrnItemIdAsync(checkingInfoCreatedEvent.GrnItemId);
                    break;
                case StockUnitPutawayAssignedEvent stockUnitPutawayAssignedEvent:
                    await EvaluateByGrnItemIdAsync(stockUnitPutawayAssignedEvent.GrnItemId);
                    break;
            }
        }

        private async Task EvaluateByGrnItemIdAsync(long grnItemId)
        {
            var item = await grnItemService.GetByIdAsync(grnItemId) ?? throw new KeyNotFoundException($"GrnItem {grnItemId} not found");
            await grnItemStateService.EvaluateAndProgressGrnItemStateAsync(item);
        }
    }
}
