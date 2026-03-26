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
        IPicklistService picklistService,
        ILogger<PicklistGenService> logger)
    {
        public async Task<List<PicklistDto>> GeneratePicklists(List<long> orderIds)
        {
            // 1️ Lettura fuori transazione
            List<OrderResponseDto> ordersOpen = await orderService.GetByStateAndIdsAsync(OrderState.OPEN, orderIds);

            if (ordersOpen.Count == 0)
                return [];

            Dictionary<string, PicklistBuilder> pickListMap = [];
            string releaseNumber = $"PKL-{Guid.NewGuid():N}".Substring(0, 12).ToUpper();

            var productCodes = ordersOpen
                .SelectMany(o => o.salesOrderLineList)
                .Select(l => l.productCode)
                .Where(code => !string.IsNullOrWhiteSpace(code))
                .Distinct()
                .ToList();

            var slotsByProductCode = await slotService.GetBestSlotsForProducts(productCodes);

            // 2️ Costruzione picklist (CPU-bound, no DB)
            foreach (var order in ordersOpen)
            {
                if (!pickListMap.TryGetValue(order.customerCode, out var builder))
                {
                    builder = new PicklistBuilder()
                        .ForCustomer(order.customerCode)
                        .WithReleaseNumber(releaseNumber);
                    pickListMap[order.customerCode] = builder;
                }

                foreach (var line in order.salesOrderLineList)
                {
                    if (!slotsByProductCode.TryGetValue(line.productCode, out var slot))
                        throw new KeyNotFoundException($"No slot for product {line.productCode}");

                    var item = new PicklistItemBuilder()
                                    .FromOrderLine(line, order.code)
                                    .WithSlotInfo(slot)
                                    .Build();

                    builder.AddItem(item);
                    logger.LogInformation(
                        "Added item {ProductCode} to picklist for customer {Customer}",
                        line.productCode,
                        order.customerCode);
                }
            }

            // 3 build dei PicklistDto
            var picklists = pickListMap
                .Values
                .Select(b => b.Build())
                .ToList();

            var idsToUpdate = ordersOpen.Select(o => o.id).ToList();

            using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                // 4 BULK UPDATE
                await context.Orders
                    .Where(o => idsToUpdate.Contains(o.Id))
                    .ExecuteUpdateAsync(s => s.SetProperty(o => o.State, OrderState.PICKING));

                // 5️ Insert batch
                var result = await picklistService.CreateBulkAsync(picklists);

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