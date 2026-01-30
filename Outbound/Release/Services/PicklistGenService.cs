using dotnet_Warehouse_Management_System.Outbound.Dtos;
using dotnet_Warehouse_Management_System.Outbound.Entities;
using dotnet_Warehouse_Management_System.Outbound.Entities.Mappers;
using dotnet_Warehouse_Management_System.Outbound.Entities.Services;
using dotnet_Warehouse_Management_System.Products.Entities.Services;

namespace dotnet_Warehouse_Management_System.Outbound.Release.Services
{
    public class PicklistGenService(IOrderService orderService, ISlotService slotService, IPicklistService picklistService)
    {
        public async Task<List<PicklistDto>> GeneratePicklists(List<long> OrderIds)
        {
            List<OrderResponseDto> ordersOpen = await orderService.GetByStateAndIdsAsync(OrderState.OPEN, OrderIds);

            Dictionary<string, PicklistDto> pickListMap = [];
            string _releaseNumber = $"PKL-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";

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

                foreach (var line in order.salesOrderLineList )
                {
                    string productCode = line.productCode;
                    var slot = await slotService.GetSlotContainingProduct(productCode);
                    PicklistItemDto itemDto = new()
                    {
                        Code = $"Item-{Guid.NewGuid().ToString()[..8].ToUpper()}",
                        ProductCode = productCode,
                        State = Common.PicklistItemState.OPEN,
                        Qty = line.quantity,
                        PickingSequence = slot.PickingSequence,
                        SlotCode = slot.Code,
                        SalesOrderCode = order.code,
                        SalesOrderLineNumber = line.salesOrderLineNumber
                    };

                    pickListDto.pickListItemList.Add(itemDto);
                    order.state = OrderState.PICKING;
                    await orderService.UpdateAsync(order);
                }
            }

            List<PicklistDto> result = [];

            foreach (var picklist in pickListMap.Values)
            {
                var picklistEntity = await picklistService.CreateAsync(picklist);
                result.Add(picklistEntity);
            }
            return result;
        }

    }
}
