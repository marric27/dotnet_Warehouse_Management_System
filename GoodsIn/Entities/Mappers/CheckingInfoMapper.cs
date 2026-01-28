using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.GoodsIn.Dtos;

namespace dotnet_Warehouse_Management_System.GoodsIn.Entities.Mappers
{
    public static class CheckingInfoMapper
    {
        public static CheckingInfoDto ToResponseDto(this CheckingInfo entitiy)
        {
            return new CheckingInfoDto
            {
                Code = entitiy.Code,
                BatchNumber = entitiy.BatchNumber,
                ExpirationDate = entitiy.ExpirationDate,
                Quantity = entitiy.Quantity,
                State = entitiy.State,
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
            };
        }
    }
}
