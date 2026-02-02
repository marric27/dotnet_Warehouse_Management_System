using dotnet_Warehouse_Management_System.Picking.Dtos;
using dotnet_Warehouse_Management_System.Picking.Entities.Repository;
using dotnet_Warehouse_Management_System.Picking.Mappers;

namespace dotnet_Warehouse_Management_System.Picking.Entities.Service
{
    public class PickingInfoService(IPickingInfoRepository pickingInfoRepository) : IPickingInfoService
    {
        public async Task<PickingInfoDto> CreateAsync(PickingInfoDto dto)
        {
            var pickinginfo = dto.ToEntity();
            var created = await pickingInfoRepository.CreateAsync(pickinginfo);
            return created.ToResponseDto();
        }
    }
}
