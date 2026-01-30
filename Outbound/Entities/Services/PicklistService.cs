using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Outbound.Dtos;
using dotnet_Warehouse_Management_System.Outbound.Entities.Repositories;
using dotnet_Warehouse_Management_System.Outbound.Entities.Mappers;
using dotnet_Warehouse_Management_System.Picking.Dtos;
using Microsoft.EntityFrameworkCore;
using dotnet_Warehouse_Management_System.Outbound.Mappers;

namespace dotnet_Warehouse_Management_System.Outbound.Entities.Services
{
    public class PicklistService(IPicklistRepository picklistRepository, IPicklistItemRepository picklistItemRepository) : IPicklistService
    {
        public async Task<PicklistDto> CreateAsync(PicklistDto picklistDto)
        {
            var picklist = picklistDto.ToEntity();
            var created = await picklistRepository.CreateAsync(picklist);
            return created.ToResponseDto();
        }

        public async Task<List<PicklistDto>> GetAllAsync()
        {
            var pls = await picklistRepository.GetAllAsync();
            return pls.Select(p => p.ToResponseDto()).ToList();
        }

        public async Task<Page<PicklistDto>> GetAllPaginatedAsync(QueryObject query)
        {
            var pls = await picklistRepository.GetAllPaginatedAsync(query);
            return pls.Map(o => o.ToResponseDto());
        }

        public async Task<PicklistDto?> GetByCodeAsync(string code)
        {
            var pl = await picklistRepository.GetByCodeAsync(code);
            return pl.ToResponseDto();
        }

        public async Task<PicklistItemDto?> GetNextPickListItemAsync(NextItemRequest request)
        {
            var plIds = request.PickListIds;
            var item = await picklistItemRepository.FindItemsByStateOrdered(plIds, PicklistItemState.OPEN);
            return item?.ToResponseDto();
        }

    }
}
