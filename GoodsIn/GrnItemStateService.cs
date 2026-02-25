using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Exceptions;
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
            int assigned = item.CheckingInfoList?.Sum(ci => ci.Quantity) ?? 0;
            State current = item.State == null ? State.OPEN : item.State;

            // Passaggio a CHECKED
            if (current == State.OPEN && assigned >= received && received > 0)
            {
                item.State = State.CHECKED;
                await grnItemService.UpdateAsync(item);
                current = State.CHECKED;
            }

            // Passaggio a PUTAWAY (se tutti i figli sono in stato PUTAWAY)
            if (current == State.CHECKED &&
                item.CheckingInfoList != null &&
                item.CheckingInfoList.Any() &&
                item.CheckingInfoList.All(c => c.State == State.PUTAWAY))
            {
                item.State = State.PUTAWAY;
                await grnItemService.UpdateAsync(item);
                await EvaluateAndProgressGrnStateAsync(item.GrnId);
            }
        }

        public async Task EvaluateAndProgressGrnStateAsync(long grnId)
        {
            var grn = await grnService.GetByIdAsync(grnId) ?? throw new KeyNotFoundException("GRN not found");

            bool allPutaway = grn.Items.All(i => i.State == State.PUTAWAY);

            if (allPutaway)
            {
                grn.State = State.CLOSED;
                await grnService.UpdateAsync(grn);
            }
        }

        public void ValidateItemQuantities(GrnItemRequestDto item)
        {
            int expected = item.ExpectedQty;
            int compliant = item.CompliantQty;
            int notCompliant = item.NotCompliantQty;
            int received = item.ReceivedQty;

            if (received == 0) item.State = State.PUTAWAY;
            else item.State = State.OPEN;

            if (expected <= 0)
                throw new ArgumentException("Expected qty must be > 0");

            if (received != compliant + notCompliant)
                throw new ArgumentException("Received != compliant + notCompliant");

            if (received > expected)
                throw new DomainConflictException("Over-received: expected=" + expected + " received=" + received);
        }
    }
}
