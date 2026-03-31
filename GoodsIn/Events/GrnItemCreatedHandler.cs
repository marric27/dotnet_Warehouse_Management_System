using dotnet_Warehouse_Management_System.GoodsIn.Services;

namespace dotnet_Warehouse_Management_System.GoodsIn.Events
{
    public class GrnItemCreatedHandler(IGrnItemService grnItemService, IGrnItemStateService grnItemStateService) : IEventHandler<GrnItemCreatedEvent>
    {
        public async Task HandleAsync(GrnItemCreatedEvent evt, CancellationToken ct = default)
        {
            var item = await grnItemService.GetByIdAsync(evt.GrnItemId) ?? throw new KeyNotFoundException($"GrnItem {evt.GrnItemId} not found");

            await grnItemStateService.EvaluateAndProgressGrnItemStateAsync(item);
        }
    }
}
