using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Outbound.Dtos;
using dotnet_Warehouse_Management_System.Outbound.Entities.Repositories;
using dotnet_Warehouse_Management_System.Outbound.Entities.Mappers;

namespace dotnet_Warehouse_Management_System.Outbound.Entities.Services
{
    public class PicklistService : IPicklistService
    {
        private readonly IPicklistRepository _picklistRepository;
        public PicklistService(IPicklistRepository picklistRepository)
        {
            _picklistRepository = picklistRepository;
        }
        public async Task<PicklistDto> CreateAsync(PicklistDto picklistDto)
        {
            Console.WriteLine(
                $"DTO items count: {picklistDto.pickListItemList?.Count ?? -1}"
            );

            var picklist = picklistDto.ToEntity();
            Console.WriteLine(
                $"ENTITY items count: {picklist.PicklistItemList.Count}"
            );

            var created = await _picklistRepository.CreateAsync(picklist);
            return created.ToResponseDto();
        }

        public async Task<List<PicklistDto>> GetAllAsync()
        {
            var pls = await _picklistRepository.GetAllAsync();
            return pls.Select(p => p.ToResponseDto()).ToList();
        }

        public async Task<Page<PicklistDto>> GetAllPaginatedAsync(QueryObject query)
        {
            var pls = await _picklistRepository.GetAllPaginatedAsync(query);
            return pls.Map(o => o.ToResponseDto());
        }

        public async Task<PicklistDto?> GetByCodeAsync(string code)
        {
            var pl = await _picklistRepository.GetByCodeAsync(code);
            return pl.ToResponseDto();
        }
    }
}
