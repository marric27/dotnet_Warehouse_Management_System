using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.GoodsIn.Dtos;

namespace dotnet_Warehouse_Management_System.GoodsIn.Entities.Mappers
{
    public static class CheckingInfoMapper
    {
        public static CheckingInfoDto ToResponseDto(this CheckingInfo entity)
        {
            if (entity == null) return null;
            return new CheckingInfoDto
            {
                Code = entity.Code,
                BatchNumber = entity.BatchNumber,
                ExpirationDate = entity.ExpirationDate,
                Quantity = entity.Quantity,
                State = entity.State,
                StockUnitId = entity.StockUnitId,
            };
        }

        public static CheckingInfo ToEntity(this CheckingInfoDto dto)
        {
            return new CheckingInfo
            {
                Code = dto.Code,
                BatchNumber = dto.BatchNumber,
                ExpirationDate = dto.ExpirationDate,
                Quantity = dto.Quantity,
                State = dto.State,
                StockUnitId = dto.StockUnitId,
                GrnItemId = dto.GrnItemId
            };
        }
    }
}
