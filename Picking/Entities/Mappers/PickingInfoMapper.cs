using dotnet_Warehouse_Management_System.Picking.Dtos;
using dotnet_Warehouse_Management_System.Picking.Entities;

namespace dotnet_Warehouse_Management_System.Picking.Mappers
{
    public static class PickingInfoMapper
    {
        public static PickingInfoDto ToResponseDto(this PickingInfo entity)
        {
            if (entity == null)
                return null;

            return new PickingInfoDto
            {
                Id = entity.Id,
                Timestamp = entity.Timestamp,
                User = entity.User,
                StockUnitCode = entity.StockUnitCode,
                BatchNumber = entity.BatchNumber,
                ExpirationDate = entity.ExpirationDate,
                Quantity = entity.Quantity
            };
        }

        public static PickingInfo ToEntity(this PickingInfoDto dto)
        {
            if (dto == null)
                return null;

            return new PickingInfo
            {
                Id = dto.Id,
                Timestamp = dto.Timestamp,
                User = dto.User,
                StockUnitCode = dto.StockUnitCode,
                BatchNumber = dto.BatchNumber,
                ExpirationDate = dto.ExpirationDate,
                Quantity = dto.Quantity,
            };
        }
    }
}
