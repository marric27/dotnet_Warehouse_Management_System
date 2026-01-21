using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.GoodsIn.Dtos;

namespace dotnet_Warehouse_Management_System.GoodsIn
{
    public class GrnItemStateService : IGrnItemStateService
    {
        public void EvaluateAndProgressGrnItemState(GrnItemRequestDto item)
        {
            throw new NotImplementedException();
        }

        public void EvaluateAndProgressGrnState(GrnRequestDto grnItem)
        {
            
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
