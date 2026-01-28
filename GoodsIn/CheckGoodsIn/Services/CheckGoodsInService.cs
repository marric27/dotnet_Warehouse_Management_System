<<<<<<< Updated upstream
﻿namespace dotnet_Warehouse_Management_System.GoodsIn.CheckGoodsIn.Services
{
    public class CheckGoodsInService
    {
=======
﻿using Azure.Core;
using dotnet_Warehouse_Management_System.GoodsIn.Dtos;
using dotnet_Warehouse_Management_System.GoodsIn.Entities.Repositories;
using dotnet_Warehouse_Management_System.GoodsIn.Entities.Services;
using dotnet_Warehouse_Management_System.GoodsIn.Services;

namespace dotnet_Warehouse_Management_System.GoodsIn.CheckGoodsIn.Services
{
    public class CheckGoodsInService(IGrnService grnService, IGrnItemService grnItemService, ICheckingInfoService checkingInfoService, IGrnItemStateService grnItemStateService)
    {
        private readonly IGrnItemService _grnItemService = grnItemService;
        private readonly IGrnService _grnService = grnService;
        private readonly ICheckingInfoService _checkingInfoService = checkingInfoService;
        private readonly IGrnItemStateService _grnItemStateService = grnItemStateService;

        public async GrnItemResponseDto CreateCheckingInfoAndStockUnit(string grnItemCode, StockUnitDto su)
        {
            GrnItemResponseDto grnItem = await _grnItemService.GetByCodeAsync(grnItemCode);
            if (grnItem.State == Common.State.CHECKED || grnItem.State == Common.State.PUTAWAY) throw new Exception("Cant assign checking info to GrnItem " + grnItemCode + " in Closed or Putaway state");

            if (su.Quantity > grnItem.ReceivedQty) throw new Exception("Requested quantity " + su.Quantity + " exceeds available quantity " + grnItem.ReceivedQty);

            // setta su

            // crea su

            CheckingInfoDto ci = new()
            {

                Quantity = su.Quantity,
                State = Common.State.OPEN,
                BatchNumber = su.BatchNumber,
                ExpirationDate = su.ExpirationDate,
            };
            var createdCi = _checkingInfoService.CreateAsync(ci);

            // assegna a item

            // progress state item
            //_grnItemStateService.EvaluateAndProgressGrnItemState(); // vedi quale item se quello gia in memoria o fare nuovo pull da db
            //return item;
        }
>>>>>>> Stashed changes
    }
}
