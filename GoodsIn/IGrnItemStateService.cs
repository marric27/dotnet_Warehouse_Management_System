using dotnet_Warehouse_Management_System.GoodsIn.Dtos;

namespace dotnet_Warehouse_Management_System.GoodsIn
{
    public interface IGrnItemStateService
    {
        public void ValidateItemQuantities(GrnItemRequestDto grnItem);
        public Task EvaluateAndProgressGrnItemStateAsync(GrnItemResponseDto grnItem);
        public Task EvaluateAndProgressGrnState(long grnId);
    }
}
