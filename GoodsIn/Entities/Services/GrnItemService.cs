using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.GoodsIn.Dtos;
using dotnet_Warehouse_Management_System.GoodsIn.Entities;
using dotnet_Warehouse_Management_System.GoodsIn.Entities.Mappers;
using dotnet_Warehouse_Management_System.GoodsIn.Entities.Repositories;
using Microsoft.EntityFrameworkCore;

namespace dotnet_Warehouse_Management_System.GoodsIn.Services
{
    public class GrnItemService(IGrnItemRepository grnItemRepository, IGrnRepository grnRepository, ICheckingInfoRepository checkingInfoRepository) : IGrnItemService
    {

        public async Task<GrnItemResponseDto> CreateAsync(GrnItemRequestDto grnItemRequestDto)
        {
            var grnItem = grnItemRequestDto.ToGrnItem();
            grnItem.GenerateCode();
            grnItem.State = State.OPEN;
            var createdItem = await grnItemRepository.CreateAsync(grnItem);

            return createdItem.ToResponseDto();
        }

        public async Task<GrnItemResponseDto?> GetByCodeAsync(string code)
        {
            var grnItem = await grnItemRepository.GetByCodeAsync(code);
            if (grnItem == null)
            {
                return null;
            }
            return grnItem.ToResponseDto();
        }

        public async Task<List<GrnItemResponseDto>> GetAllAsync(QueryObject query)
        {
            var grnItems = await grnItemRepository.GetAllAsync(query);
            return grnItems.Select(grnItem => grnItem.ToResponseDto()).ToList();
        }

        public async Task<GrnItemResponseDto?> DeleteAsync(string code)
        {
            var deleted = await grnItemRepository.DeleteAsync(code);
            return deleted?.ToResponseDto();
        }

        public async Task<GrnItemResponseDto> UpdateAsync(string code, GrnItemRequestDto grnItemRequestDto)
        {
            var updated = await grnItemRepository.UpdateAsync(grnItemRequestDto.ToGrnItem());
            if (updated == null)
                throw new KeyNotFoundException($"GRN {code} non trovata");

            return updated.ToResponseDto();
        }

        public async Task<GrnItemResponseDto> CreateGrnItemForExistingGrnByCodeAsync(string grnCode, GrnItemRequestDto grnItemRequestDto)
        {
            var grn = await grnRepository.GetByCodeAsync(grnCode);
            var grnItem = grnItemRequestDto.ToGrnItem();
            grnItem.GenerateCode();
            grnItem.State = State.OPEN;
            grnItem.GrnId = grn.Id;
            var createdItem = await grnItemRepository.CreateAsync(grnItem);

            return createdItem.ToResponseDto();
        }

        public async Task<GrnItemResponseDto?> GetByIdAsync(long id)
        {
            var item = await grnItemRepository.GetById(id);
            return item.ToResponseDto();
        }

        public async Task AddCheckingInfo(string grnItemCode, string checkingInfoCode)
        {
            GrnItem grnItem = await grnItemRepository.GetByCodeAsync(grnItemCode);
            CheckingInfo checkingInfo = await checkingInfoRepository.GetByCode(checkingInfoCode);

            grnItem.CheckingInfoList.Add(checkingInfo);

            await grnItemRepository.UpdateAsync(grnItem);
        }

        public async Task<GrnItemResponseDto> UpdateStateAsync(string code, State state)
        {
            var updated = await grnItemRepository.GetByCodeAsync(code) ?? throw new KeyNotFoundException($"GRN item {code} non trovata");
            updated.State = state;
            await grnItemRepository.UpdateAsync(updated);

            return updated.ToResponseDto();
        }
    }
}
