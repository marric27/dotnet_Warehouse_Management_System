using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.GoodsIn.Entities.Services;
using dotnet_Warehouse_Management_System.GoodsIn.Services;
using dotnet_Warehouse_Management_System.Products.Entities.Services;
using dotnet_Warehouse_Management_System.Warehouses.Entities.Dtos;

namespace dotnet_Warehouse_Management_System.GoodsIn.Putaway.Services
{
    public class PutawayService(
        ISlotService slotService,
        IStockUnitService stockUnitService,
        ICheckingInfoService checkingInfoService,
        IGrnItemService grnItemService,
        IGrnItemStateService stateService)
    {
        public async Task<SlotResponseDto> AssignStockUnitToSlotAsync(string stockUnitCode, string slotCode)
        {
            var slot = await slotService.GetByCodeAsync(slotCode) ?? throw new KeyNotFoundException("Slot not found");
            var su = await stockUnitService.GetByCodeAsync(stockUnitCode) ?? throw new KeyNotFoundException("StockUnit not found");

            if (slot.Category != su.Category) throw new ArgumentException("Category mismatch");

            // Assegnazione
            await stockUnitService.AssingToSlotAsync(su.Code, slot.Code);

            // Update checkingInfo state
            var ci = await checkingInfoService.GetByStockUnitIdAsync(su.Id) ?? throw new KeyNotFoundException("CheckingInfo not found");
            ci.State = State.PUTAWAY;
            await checkingInfoService.UpdateAsync(ci);

            // Recupero Item e valutazione stato
            var item = await grnItemService.GetByIdAsync(ci.GrnItemId);
            await stateService.EvaluateAndProgressGrnItemStateAsync(item);


            // Ritorna il dato fresco
            return await slotService.GetByCodeAsync(slotCode);
        }

    }
}
