using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Products.Entities.Repository;
using dotnet_Warehouse_Management_System.Warehouses.Entities.Dtos;
using dotnet_Warehouse_Management_System.Warehouses.Entities.Mappers;
using dotnet_Warehouse_Management_System.Warehouses.Entities.Repositories;

namespace dotnet_Warehouse_Management_System.Products.Entities.Services
{
    public class SlotService(ISlotRepository slotRepository) : ISlotService
    {
        public async Task<Page<SlotResponseDto>> GetAllAsync(QueryObject query)
        {
            var slots = await slotRepository.GetAllAsync(query);

            var pagedDto = new Page<SlotResponseDto>
            {
                PageNumber = slots.PageNumber,
                PageSize = slots.PageSize,
                TotalElements = slots.TotalElements,
                Content = slots.Content.Select(p => p.ToResponseDto()).ToList()
            };
            return pagedDto;
        }
        public async Task<List<SlotResponseDto>> GetAllAsync()
        {
            var slots = await slotRepository.GetAllAsync();
            return slots.Select(p => p.ToResponseDto()).ToList();
        }

        public async Task<SlotResponseDto?> GetByCodeAsync(string code)
        {
            var slot = await slotRepository.GetByCodeAsync(code);
            return slot?.ToResponseDto();
        }

        public async Task<SlotResponseDto> CreateAsync(SlotRequestDto slotDto)
        {
            var slot = slotDto.ToSlot();
            slot.GenerateCode();

            var created = await slotRepository.CreateAsync(slot);
            return created.ToResponseDto();
        }

        public async Task<SlotResponseDto?> UpdateAsync(string code, SlotRequestDto slotDto)
        {
            var existingSlot = await slotRepository.GetByCodeAsync(code);
            await slotRepository.UpdateAsync();
            return existingSlot.ToResponseDto();
        }

        public async Task<bool> DeleteAsync(string code)
        {
            var deleted = await slotRepository.GetByCodeAsync(code);
            if (deleted == null) return false;
            await slotRepository.DeleteAsync(code);
            return true;
        }

        public async Task<SlotResponseDto?> GetSlotContainingProduct(string productCode)
        {
            var slot = await slotRepository.GetSlotContainingProduct(productCode);
            return slot?.ToResponseDto();
        }
    }
}
