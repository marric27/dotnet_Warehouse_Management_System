using dotnet_Warehouse_Management_System.GoodsIn.Services;

namespace dotnet_Warehouse_Management_System.GoodsIn.Events
{
    public class CheckingInfoCreatedHandler(IGrnItemService grnItemService, IGrnItemStateService grnItemStateService) : IEventHandler<CheckingInfoCreatedEvent>
    {
        public async Task HandleAsync(CheckingInfoCreatedEvent evt, CancellationToken ct = default)
        {
            var item = await grnItemService.GetByIdAsync(evt.GrnItemId) ?? throw new KeyNotFoundException($"GrnItem {evt.GrnItemId} not found");

            await grnItemStateService.EvaluateAndProgressGrnItemStateAsync(item);
        }
    }
}
