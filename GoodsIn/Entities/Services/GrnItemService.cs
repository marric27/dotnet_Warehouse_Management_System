using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.GoodsIn.Dtos;
using dotnet_Warehouse_Management_System.GoodsIn.Entities;
using dotnet_Warehouse_Management_System.GoodsIn.Entities.Mappers;
using dotnet_Warehouse_Management_System.GoodsIn.Entities.Repositories;
using dotnet_Warehouse_Management_System.Products.Entities.Repository;
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
            var grnItem = await grnItemRepository.GetByCodeAsync(code, false);
            return grnItem?.ToResponseDto();
        }

        public async Task<List<GrnItemResponseDto>> GetAllAsync(QueryObject query)
        {
            var grnItems = await grnItemRepository.GetAllAsync(query);
            return grnItems.Select(grnItem => grnItem.ToResponseDto()).ToList();
        }

        public async Task<bool> DeleteAsync(string code)
        {
            var item = await grnItemRepository.GetByCodeAsync(code, true);
            if (item == null) return false;

            await grnItemRepository.DeleteAsync(item);
            return true;
        }

        public async Task<GrnItemResponseDto> UpdateAsync(string code, GrnItemResponseDto grnItemResponseDto)
        {
            var existingItem = await grnItemRepository.GetByCodeAsync(code, true);
            existingItem.State = grnItemResponseDto.State;
            await grnItemRepository.UpdateAsync();
            return existingItem.ToResponseDto();
        }

        public async Task<GrnItemResponseDto> CreateGrnItemForExistingGrnByCodeAsync(string grnCode, GrnItemRequestDto grnItemRequestDto)
        {
            var grn = await grnRepository.GetByCodeAsync(grnCode, false);
            var grnItem = grnItemRequestDto.ToGrnItem();
            grnItem.GenerateCode();
            grnItem.State = State.OPEN;
            grnItem.GrnId = grn.Id;
            var createdItem = await grnItemRepository.CreateAsync(grnItem);

            return createdItem.ToResponseDto();
        }

        public async Task<GrnItemResponseDto?> GetByIdAsync(long id)
        {
            var item = await grnItemRepository.GetById(id, false);
            return item.ToResponseDto();
        }

        public async Task AddCheckingInfo(string grnItemCode, string checkingInfoCode)
        {
            GrnItem grnItem = await grnItemRepository.GetByCodeAsync(grnItemCode, true);
            CheckingInfo checkingInfo = await checkingInfoRepository.GetByCodeAsync(checkingInfoCode, false);

            grnItem.CheckingInfoList.Add(checkingInfo);

            await grnItemRepository.UpdateAsync();
        }

        public async Task<GrnItemResponseDto> UpdateStateAsync(string code, State state)
        {
            var updated = await grnItemRepository.UpdateStateAsync(code, state);

            return updated.ToResponseDto();
        }
    }
}
