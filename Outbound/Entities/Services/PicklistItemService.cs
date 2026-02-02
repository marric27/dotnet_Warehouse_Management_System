using dotnet_Warehouse_Management_System.Outbound.Dtos;
using dotnet_Warehouse_Management_System.Outbound.Entities.Repositories;
using dotnet_Warehouse_Management_System.Outbound.Mappers;

namespace dotnet_Warehouse_Management_System.Outbound.Entities.Services
{
    public class PicklistItemService(IPicklistItemRepository picklistItemRepository) : IPicklistItemService
    {
        public async Task<PicklistItemDto?> UpdateAsync(string code, PicklistItemDto dto)
        {
            var existing = await picklistItemRepository.GetByCodeAsync(code, true) ?? throw new KeyNotFoundException();
            existing.State = dto.State;
            existing.ErrorReason = dto.ErrorReason;
            existing.qty = dto.Qty;
            existing.PickedQty = dto.PickedQty;
            await picklistItemRepository.UpdateAsync();
            return existing?.ToResponseDto();
        }
    }
}
