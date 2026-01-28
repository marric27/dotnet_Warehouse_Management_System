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
            int received = item.ReceivedQty;
            int assigned = item.checkingInfoList?.Sum(ci => ci.Quantity) ?? 0;
            State current = item.State == null ? State.OPEN : item.State;


            // Passaggio a CHECKED
            if (current == State.OPEN && assigned >= received && received > 0)
            {
                await grnItemService.UpdateStateAsync(item.Code, State.CHECKED);
                current = State.CHECKED;
            }

            // Passaggio a PUTAWAY (se tutti i figli sono in stato PUTAWAY)
            if (current == State.CHECKED &&
                item.checkingInfoList != null &&
                item.checkingInfoList.Any() &&
                item.checkingInfoList.All(c => c.State == State.PUTAWAY))
            {
                await grnItemService.UpdateStateAsync(item.Code, State.PUTAWAY);
                await EvaluateAndProgressGrnState(item.GrnId);
            }
        }

        public async Task EvaluateAndProgressGrnState(long grnId)
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
