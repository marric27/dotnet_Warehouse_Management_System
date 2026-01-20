using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.GoodsIn.Dtos;
using dotnet_Warehouse_Management_System.GoodsIn.Entities;
using dotnet_Warehouse_Management_System.GoodsIn.Entities.Mappers;
using dotnet_Warehouse_Management_System.GoodsIn.Entities.Repositories;

namespace dotnet_Warehouse_Management_System.GoodsIn.Services
{
    public class GrnItemService : IGrnItemService
    {
        private readonly IGrnItemRepository _grnItemRepository;
        private readonly IGrnRepository _grnRepository;

        public GrnItemService(IGrnItemRepository grnItemRepository, IGrnRepository grnRepository)
        {
            _grnItemRepository = grnItemRepository;
            _grnRepository = grnRepository;
        }

        public async Task<GrnItemResponseDto> CreateAsync(GrnItemRequestDto grnItemRequestDto)
        {
            var grnItem = grnItemRequestDto.ToGrnItem();
            grnItem.GenerateCode();
            grnItem.State = State.OPEN;
            var createdItem = await _grnItemRepository.CreateAsync(grnItem);

            return createdItem.ToResponseDto();
        }

        public async Task<GrnItemResponseDto?> GetByCodeAsync(string code)
        {
            var grnItem = await _grnItemRepository.GetByCodeAsync(code);
            if (grnItem == null)
            {
                return null;
            }
            return grnItem.ToResponseDto();
        }

        public async Task<List<GrnItemResponseDto>> GetAllAsync(QueryObject query)
        {
            var grnItems = await _grnItemRepository.GetAllAsync(query);
            return grnItems.Select(grnItem => grnItem.ToResponseDto()).ToList();
        }

        public async Task<GrnItemResponseDto?> DeleteAsync(string code)
        {
            var deleted = await _grnItemRepository.DeleteAsync(code);
            return deleted?.ToResponseDto();
        }

        public async Task<GrnItemResponseDto> UpdateAsync(string code, GrnItemRequestDto grnItemRequestDto)
        {
            var updated = await _grnItemRepository.UpdateAsync(code, grnItemRequestDto.ToGrnItem());
            if (updated == null)
                throw new KeyNotFoundException($"GRN {code} non trovata");

            return updated.ToResponseDto();
        }

        public async Task<GrnItemResponseDto> CreateGrnItemForExistingGrnByCodeAsync(string grnCode, GrnItemRequestDto grnItemRequestDto)
        {
            var grn = await _grnRepository.GetByCodeAsync(grnCode);
            grnItemRequestDto.GrnId = grn.Id;
            var grnItem = grnItemRequestDto.ToGrnItem();
            grnItem.GenerateCode();
            grnItem.State = State.OPEN;
            var createdItem = await _grnItemRepository.CreateAsync(grnItem);

            return createdItem.ToResponseDto();
        }

        public async Task<GrnItemResponseDto?> GetByIdAsync(long id)
        {
            var item = await _grnItemRepository.GetById(id);
            return item.ToResponseDto();
        }
    }
}
