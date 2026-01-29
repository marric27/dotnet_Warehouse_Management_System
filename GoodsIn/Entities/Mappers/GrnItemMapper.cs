using dotnet_Warehouse_Management_System.GoodsIn.Dtos;

namespace dotnet_Warehouse_Management_System.GoodsIn.Entities.Mappers
{
    public static class GrnItemMapper
    {
        public static GrnItemResponseDto ToResponseDto(this GrnItem item)
        {
            return new GrnItemResponseDto
            {
                Id = item.Id,
                Code = item.Code,
                ProductCode = item.ProductCode,
                ExpectedQty = item.ExpectedQty,
                ReceivedQty = item.ReceivedQty,
                CompliantQty = item.CompliantQty,
                NotCompliantQty = item.NotCompliantQty,
                State = item.State,
                Notes = item.Notes,
                GrnId = item.GrnId,
                CheckingInfoList = item.CheckingInfoList?.Select(ci => ci.ToResponseDto()).ToList()
            };
        }

        public static GrnItem ToGrnItem(this GrnItemRequestDto dto)
        {
            return new GrnItem
            {
                ProductCode = dto.ProductCode,
                ExpectedQty = dto.ExpectedQty,
                ReceivedQty = dto.ReceivedQty,
                CompliantQty = dto.CompliantQty,
                NotCompliantQty = dto.NotCompliantQty,
                Notes = dto.Notes,
                GrnId = dto.GrnId
            };
        }
    }
}
