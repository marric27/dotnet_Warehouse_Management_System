//using dotnet_Warehouse_Management_System.GoodsIn.Dtos;
//using dotnet_Warehouse_Management_System.GoodsIn.Entities.Services;
//using dotnet_Warehouse_Management_System.GoodsIn.Services;
//using dotnet_Warehouse_Management_System.Products.Entities.Services;
//using dotnet_Warehouse_Management_System.Warehouses.Entities.Dtos;

//namespace dotnet_Warehouse_Management_System.GoodsIn.Putaway.Services
//{
//    public class PutawayService(ISlotService slotService, IStockUnitService stockUnitService, CheckingInfoService checkingInfoService, GrnItemService grnItemService, IGrnItemStateService grnItemStateService)
//    {



//        public async SlotResponseDto AssignStockUnitToSlot(long suId, long slotId)
//        {
//            var slot = await slotService.GetByCodeAsync("code");
//            var su = await stockUnitService.GetByCodeAsync("code");

//            if (!slot.Category.Equals(su.Category)) throw new Exception("Category mismatch");

//            slot.StockUnits.Add(su);
//            su.; // set slot id;
//            var savedSlot = slotService.UpdateAsync(slotId, slot);

//            CheckingInfoDto ci = checkingInfoService get by su id;
//            ci.State = Common.State.PUTAWAY;
//            checkingInfoService.UpdateAsync(ci);

//            GrnItemResponseDto item = grnItemService.GetByCodeAsync("code from ci");
//            grnItemStateService.EvaluateAndProgressGrnItemState(item);



//            return savedSlot;
//        }
//    }
//}
