using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.GoodsIn.Dtos;
using dotnet_Warehouse_Management_System.GoodsIn.Services;

namespace dotnet_Warehouse_Management_System.GoodsIn
{
    public class GrnItemStateService(IGrnService grnService, IGrnItemService grnItemService) : IGrnItemStateService
    {
        public void EvaluateAndProgressGrnItemState(GrnItemResponseDto item)
        {
            List<CheckingInfoDto> checkingInfos = item.CheckingInfos;
            int received = item.ReceivedQty;
            //int assigned = // somma delle quatita delle ci

            State current = item.State == null ? State.OPEN : item.State;

            if (assigned >= received && current == State.OPEN)
            { 
                item.State = State.CHECKED;
                grnItemService.UpdateAsync(item.Code, item);
                current = State.CHECKED;
            }

            if (current == State.CHECKED
                && checkingInfos != null
                && checkingInfos.Count != 0
                && checkingInfos.All(c => c.State == State.PUTAWAY))
                {
                    item.State = State.PUTAWAY;
                    grnItemService.UpdateAsync(item.Code, item);

                EvaluateAndProgressGrnState(item.Grn);
                }



            throw new NotImplementedException();
        }

        public async void EvaluateAndProgressGrnState(GrnResponseDto grn)
        {
            //GrnResponseDto grnResponseDto = await _grnService.GetByCodeAsync(grn.Code);
            //bool allPutaway = false;

            //if (allPutaway)
            //{
            //    grn.State = State.CLOSED;
            //    _grnService.UpdateAsync(grn.Code, grn);
            //}
        }

        public void ValidateItemQuantities(GrnItemRequestDto item)
        {
            int expected = item.ExpectedQty;
            int compliant = item.CompliantQty;
            int notCompliant = item.NotCompliantQty;
            int received = item.ReceivedQty;

            //if (received == 0) item.State(State.PUTAWAY);
            //else item.State = State.OPEN;

            if (expected <= 0)
                throw new Exception("Expected qty must be > 0");

            if (received != compliant + notCompliant)
                throw new Exception("Received != compliant + notCompliant");

            if (received > expected)
                throw new Exception("Over-received: expected=" + expected + " received=" + received);
        }
    }
}
