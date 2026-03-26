using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Data;
using dotnet_Warehouse_Management_System.GoodsIn.Entities.Services;
using dotnet_Warehouse_Management_System.GoodsIn.Events;
using dotnet_Warehouse_Management_System.Warehouses.Entities.Dtos;

namespace dotnet_Warehouse_Management_System.GoodsIn.Putaway.Services
{
    public class PutawayService(
        ApplicationDBContext context,
        ISlotService slotService,
        IStockUnitService stockUnitService,
        ICheckingInfoService checkingInfoService,
        IEventPublisher eventPublisher)
    {
        public async Task<SlotResponseDto> AssignStockUnitToSlotAsync(string stockUnitCode, string slotCode)
        {
            using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                var slot = await slotService.GetByCodeAsync(slotCode) ?? throw new KeyNotFoundException("Slot not found");
                var su = await stockUnitService.GetByCodeAsync(stockUnitCode) ?? throw new KeyNotFoundException("StockUnit not found");

                if (slot.Category != su.Category) throw new ArgumentException("Category mismatch");

                //Assign to slot
                su.SlotId = slot.Id;
                await stockUnitService.UpdateAsync(su);

                // Update checkingInfo state
                var ci = await checkingInfoService.GetByStockUnitIdAsync(su.Id) ?? throw new KeyNotFoundException("CheckingInfo not found");
                ci.State = State.PUTAWAY;
                await checkingInfoService.UpdateAsync(ci);

                await eventPublisher.PublishAsync(new StockUnitPutawayAssignedEvent(ci.GrnItemId, su.Id, slot.Id));
                await transaction.CommitAsync();
                // Ritorna il dato fresco
                return await slotService.GetByCodeAsync(slotCode);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

    }
}
