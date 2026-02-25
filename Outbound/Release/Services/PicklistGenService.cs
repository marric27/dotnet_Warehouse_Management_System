using dotnet_Warehouse_Management_System.Data;
using dotnet_Warehouse_Management_System.Outbound.Dtos;
using dotnet_Warehouse_Management_System.Outbound.Entities;
using dotnet_Warehouse_Management_System.Outbound.Entities.Services;
using dotnet_Warehouse_Management_System.Products.Entities.Services;

namespace dotnet_Warehouse_Management_System.Outbound.Release.Services
{
    public class PicklistGenService(ApplicationDBContext context, IOrderService orderService, ISlotService slotService, IPicklistService picklistService)
    {
        public async Task<List<PicklistDto>> GeneratePicklists(List<long> OrderIds)
        {
            using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                List<OrderResponseDto> ordersOpen = await orderService.GetByStateAndIdsAsync(OrderState.OPEN, OrderIds);

                Dictionary<string, PicklistDto> pickListMap = [];
                string _releaseNumber = $"PKL-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";

                var productCodes = ordersOpen
                    .SelectMany(order => order.salesOrderLineList)
                    .Select(line => line.productCode)
                    .Where(code => !string.IsNullOrWhiteSpace(code))
                    .Distinct()
                    .ToList();

                var slotsByProductCode = await slotService.GetBestSlotsForProducts(productCodes);

                foreach (var order in ordersOpen)
                {
                    if (!pickListMap.TryGetValue(order.customerCode, out PicklistDto pickListDto))
                    {
                        pickListDto = new PicklistDto
                        {
                            Code = $"PL-{Guid.NewGuid().ToString()[..8].ToUpper()}",
                            CustomerCode = order.customerCode,
                            ReleaseNumber = _releaseNumber,
                            pickListItemList = []
                        };

                        pickListMap[order.customerCode] = pickListDto;
                    }

                    foreach (var line in order.salesOrderLineList)
                    {
                        string productCode = line.productCode;
                        var slot = slotsByProductCode.TryGetValue(productCode, out var slotDto)
                            ? slotDto
                            : throw new KeyNotFoundException($"No slot found containing product {productCode}");
                        PicklistItemDto itemDto = new()
                        {
                            Code = $"Item-{Guid.NewGuid().ToString()[..8].ToUpper()}",
                            ProductCode = productCode,
                            State = Common.PicklistItemState.OPEN,
                            Qty = line.quantity,
                            PickedQty = 0,
                            PickingSequence = slot.PickingSequence,
                            SlotCode = slot.Code,
                            SalesOrderCode = order.code,
                            SalesOrderLineNumber = line.salesOrderLineNumber
                        };

                        pickListDto.pickListItemList.Add(itemDto);
                    }
                    order.state = OrderState.PICKING;
                    await orderService.UpdateAsync(order);
                }

                List<PicklistDto> result = [];

                foreach (var picklist in pickListMap.Values)
                {
                    var picklistEntity = await picklistService.CreateAsync(picklist);
                    result.Add(picklistEntity);
                }
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
