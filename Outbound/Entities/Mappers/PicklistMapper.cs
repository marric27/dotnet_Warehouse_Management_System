using dotnet_Warehouse_Management_System.Outbound.Dtos;
using dotnet_Warehouse_Management_System.Outbound.Mappers;

namespace dotnet_Warehouse_Management_System.Outbound.Entities.Mappers
{
    public static class PicklistMapper
    {
        public static PicklistDto ToResponseDto(this Picklist entity)
        {
            if (entity == null)
                return null;

            return new PicklistDto
            {
                Code = entity.Code,
                ReleaseNumber = entity.ReleaseNumber,
                CustomerCode = entity.CustomerCode,
                PicklistItemList = entity.PicklistItemList?
                    .Select(p => p.ToResponseDto())
                    .ToList()
            };
        }

        public static Picklist ToEntity(this PicklistDto dto)
        {
            if (dto == null)
                return null;

            var picklist = new Picklist
            {
                Code = dto.Code,
                ReleaseNumber = dto.ReleaseNumber,
                CustomerCode = dto.CustomerCode,
                PicklistItemList = dto.PicklistItemList?
                .Select(PicklistItemMapper.ToEntity)
                .ToList()
            };

            foreach (var item in picklist.PicklistItemList)
            {
                item.Picklist = picklist;
            }

            return picklist;
        }
    }
}