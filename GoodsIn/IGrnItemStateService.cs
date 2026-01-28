using dotnet_Warehouse_Management_System.GoodsIn.Dtos;

namespace dotnet_Warehouse_Management_System.GoodsIn
{
    public interface IGrnItemStateService
    {
        public void ValidateItemQuantities(GrnItemRequestDto grnItem);
        public void EvaluateAndProgressGrnItemState(GrnItemResponseDto grnItem);
        public void EvaluateAndProgressGrnState(GrnResponseDto grn);
    }
}
