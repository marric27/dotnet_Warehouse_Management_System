using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.GoodsIn.Dtos;
using dotnet_Warehouse_Management_System.GoodsIn.Entities;
using dotnet_Warehouse_Management_System.GoodsIn.Services;

namespace dotnet_Warehouse_Management_System.GoodsIn
{
    public class GrnItemStateService(IGrnService grnService, IGrnItemService grnItemService) : IGrnItemStateService
    {
        public async Task EvaluateAndProgressGrnItemStateAsync(GrnItemResponseDto item)
        {
            List<CheckingInfoDto> checkingInfos = item.checkingInfoList;
            int received = item.ReceivedQty;
            int assigned = item.checkingInfoList.Sum(ci => ci.Quantity);

            State current = item.State == null ? State.OPEN : item.State;

            if (assigned >= received && current == State.OPEN)
            {
                await grnItemService.UpdateStateAsync(item.Code, State.CHECKED);
                current = State.CHECKED;
            }

            if (current == State.CHECKED
                && checkingInfos != null
                && checkingInfos.Count != 0
                && checkingInfos.All(c => c.State == State.PUTAWAY))
            {
                await grnItemService.UpdateStateAsync(item.Code, State.PUTAWAY);

                EvaluateAndProgressGrnState(item.GrnId);
            }
        }

        public async void EvaluateAndProgressGrnState(long grnId)
        {
            GrnResponseDto grnResponseDto = await grnService.GetByIdAsync(grnId) ?? throw new KeyNotFoundException("Grn not found");
            bool allPutaway = grnResponseDto.Items.All(i => i.State == State.PUTAWAY);

            if (allPutaway)
            {
                _ = grnService.UpdateStateAsync(grnResponseDto.Code, State.CLOSED);
            }
        }

        public void ValidateItemQuantities(GrnItemRequestDto item)
        {
            int expected = item.ExpectedQty;
            int compliant = item.CompliantQty;
            int notCompliant = item.NotCompliantQty;
            int received = item.ReceivedQty;

            //if (received == 0) item.State(State.PUTAWAY);
            //else item.State = State.OPEN; // TODO

            if (expected <= 0)
                throw new Exception("Expected qty must be > 0");

            if (received != compliant + notCompliant)
                throw new Exception("Received != compliant + notCompliant");

            if (received > expected)
                throw new Exception("Over-received: expected=" + expected + " received=" + received);
        }
    }
}
