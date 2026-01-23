using dotnet_Warehouse_Management_System.Picking.Dtos;
using dotnet_Warehouse_Management_System.Picking.Entities.Repository;
using dotnet_Warehouse_Management_System.Picking.Mappers;

namespace dotnet_Warehouse_Management_System.Picking.Entities.Service
{
    public class PickingInfoService : IPickingInfoService
    {
        private readonly IPickingInfoRepository _pickingInfoRepository;
        public PickingInfoService(IPickingInfoRepository pickingInfoRepository)
        {
            _pickingInfoRepository = pickingInfoRepository;
        }
        public async Task<PickingInfoDto> CreateAsync(PickingInfoDto dto)
        {
            var pickinginfo = dto.ToEntity();
            var created = await _pickingInfoRepository.CreateAsync(pickinginfo);
            return created.ToResponseDto();
        }
    }
}
