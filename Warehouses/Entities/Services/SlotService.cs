using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Warehouses.Entities.Dtos;
using dotnet_Warehouse_Management_System.Warehouses.Entities.Mappers;
using dotnet_Warehouse_Management_System.Warehouses.Entities.Repositories;

namespace dotnet_Warehouse_Management_System.Products.Entities.Services
{
    public class SlotService : ISlotService
    {
        private readonly ISlotRepository _slotRepository;

        public SlotService(ISlotRepository slotRepository)
        {
            _slotRepository = slotRepository;
        }
        public async Task<Page<SlotResponseDto>> GetAllAsync(QueryObject query)
        {
            var slots = await _slotRepository.GetAllAsync(query);

            var pagedDto = new Page<SlotResponseDto>
            {
                PageNumber = slots.PageNumber,
                PageSize = slots.PageSize,
                TotalElements = slots.TotalElements,
                Content = slots.Content.Select(p => p.ToResponseDto()).ToList()
            };
            return pagedDto;
        }

        public async Task<SlotResponseDto?> GetByCodeAsync(string code)
        {
            var slot = await _slotRepository.GetByCodeAsync(code);
            return slot?.ToResponseDto();
        }

        public async Task<SlotResponseDto> CreateAsync(SlotRequestDto slotDto)
        {
            var slot = slotDto.ToSlot();
            slot.GenerateCode();

            var created = await _slotRepository.CreateAsync(slot);
            return created.ToResponseDto();
        }

        public async Task<SlotResponseDto?> UpdateAsync(string code, SlotRequestDto slotDto)
        {
            var updated = await _slotRepository.UpdateAsync(code, slotDto);
            return updated?.ToResponseDto();
        }

        public async Task<SlotResponseDto?> DeleteAsync(string code)
        {
            var deleted = await _slotRepository.DeleteAsync(code);
            return deleted?.ToResponseDto();
        }
    }
}
