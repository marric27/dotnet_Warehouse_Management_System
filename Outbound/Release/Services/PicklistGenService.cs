using dotnet_Warehouse_Management_System.Data;
using dotnet_Warehouse_Management_System.Outbound.Dtos;
using dotnet_Warehouse_Management_System.Outbound.Entities;
using dotnet_Warehouse_Management_System.Outbound.Entities.Services;
using dotnet_Warehouse_Management_System.Products.Entities.Services;
using Microsoft.EntityFrameworkCore;

namespace dotnet_Warehouse_Management_System.Outbound.Release.Services
{
    public class PicklistGenService(
        ApplicationDBContext context,
        IOrderService orderService,
        ISlotService slotService,
        IPicklistService picklistService)
    {
        public async Task<List<PicklistDto>> GeneratePicklists(List<long> orderIds)
        {
            // 1️⃣ Lettura fuori transazione
            List<OrderResponseDto> ordersOpen =
                await orderService.GetByStateAndIdsAsync(OrderState.OPEN, orderIds);

            if (!ordersOpen.Any())
                return [];

            Dictionary<string, PicklistDto> pickListMap = [];
            string releaseNumber = $"PKL-{Guid.NewGuid():N}".Substring(0, 12).ToUpper();

            var productCodes = ordersOpen
                .SelectMany(o => o.salesOrderLineList)
                .Select(l => l.productCode)
                .Where(code => !string.IsNullOrWhiteSpace(code))
                .Distinct()
                .ToList();

            var slotsByProductCode =
                await slotService.GetBestSlotsForProducts(productCodes);

            // 2️⃣ Costruzione picklist (CPU-bound, no DB)
            foreach (var order in ordersOpen)
            {
                if (!pickListMap.TryGetValue(order.customerCode, out var pickListDto))
                {
                    pickListDto = new PicklistDto
                    {
                        Code = $"PL-{Guid.NewGuid():N}".Substring(0, 12).ToUpper(),
                        CustomerCode = order.customerCode,
                        ReleaseNumber = releaseNumber,
                        pickListItemList = []
                    };
                    pickListMap[order.customerCode] = pickListDto;
                }

                foreach (var line in order.salesOrderLineList)
                {
                    var slot = slotsByProductCode.TryGetValue(line.productCode, out var s)
                        ? s
                        : throw new KeyNotFoundException($"No slot for product {line.productCode}");

                    pickListDto.pickListItemList.Add(new PicklistItemDto
                    {
                        Code = $"IT-{Guid.NewGuid():N}".Substring(0, 12).ToUpper(),
                        ProductCode = line.productCode,
                        State = Common.PicklistItemState.OPEN,
                        Qty = line.quantity,
                        PickedQty = 0,
                        PickingSequence = slot.PickingSequence,
                        SlotCode = slot.Code,
                        SalesOrderCode = order.code,
                        SalesOrderLineNumber = line.salesOrderLineNumber
                    });
                }
            }

            var idsToUpdate = ordersOpen.Select(o => o.id).ToList();

            using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                // 3️⃣ BULK UPDATE
                await context.Orders
                    .Where(o => idsToUpdate.Contains(o.Id))
                    .ExecuteUpdateAsync(s => s.SetProperty(o => o.State, OrderState.PICKING));

                // 4️⃣ Insert picklists in batch
                var result = await picklistService.CreateBulkAsync(pickListMap.Values.ToList());

                await transaction.CommitAsync();
                return result;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}