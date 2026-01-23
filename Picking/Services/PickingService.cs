using dotnet_Warehouse_Management_System.Outbound.Dtos;
using dotnet_Warehouse_Management_System.Outbound.Entities.Services;
using dotnet_Warehouse_Management_System.Picking.Dtos;
using dotnet_Warehouse_Management_System.Picking.Entities.Service;

namespace dotnet_Warehouse_Management_System.Picking.Services
{
    public class PickingService
    {
        private readonly IPicklistService _picklistService;
        private readonly IPickingInfoService _pickingInfoService;
        public PickingService(IPicklistService picklistService, IPickingInfoService pickingInfoService)
        {
            _picklistService = picklistService;
            _pickingInfoService = pickingInfoService;
        }

        public Task<PicklistItemDto> GetNextPickListItem(NextItemRequest nextItemRequest) => _picklistService.GetNextPickListItemAsync(nextItemRequest);





    }
}
