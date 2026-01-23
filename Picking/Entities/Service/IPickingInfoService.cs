using dotnet_Warehouse_Management_System.Picking.Dtos;

namespace dotnet_Warehouse_Management_System.Picking.Entities.Service
{
    public interface IPickingInfoService
    {
        Task<PickingInfoDto> CreateAsync(PickingInfoDto dto);
    }
}
