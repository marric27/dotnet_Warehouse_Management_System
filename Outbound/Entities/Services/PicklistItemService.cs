using dotnet_Warehouse_Management_System.Outbound.Dtos;
using dotnet_Warehouse_Management_System.Outbound.Entities.Repositories;
using dotnet_Warehouse_Management_System.Outbound.Mappers;

namespace dotnet_Warehouse_Management_System.Outbound.Entities.Services
{
    public class PicklistItemService : IPicklistItemService
    {
        private readonly IPicklistItemRepository _picklistItemRepository;
        public PicklistItemService(IPicklistItemRepository picklistItemRepository)
        {
            _picklistItemRepository = picklistItemRepository;
        }

        public async Task<PicklistItemDto?> UpdateAsync(string code, PicklistItemDto dto)
        {
            var updated = await _picklistItemRepository.UpdateAsync(code, dto);
            return updated?.ToResponseDto();
        }
    }
}
