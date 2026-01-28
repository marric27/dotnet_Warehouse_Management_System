using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.GoodsIn.Dtos;
using dotnet_Warehouse_Management_System.GoodsIn.Entities;
using dotnet_Warehouse_Management_System.GoodsIn.Entities.Mappers;
using dotnet_Warehouse_Management_System.GoodsIn.Entities.Repositories;
using Microsoft.EntityFrameworkCore;


namespace dotnet_Warehouse_Management_System.GoodsIn.Services
{
    public class GrnService : IGrnService
    {
        private readonly IGrnRepository _grnRepository;
        private readonly IGrnItemService _grnItemService;

        public GrnService(IGrnRepository grnRepository, IGrnItemService grnItemService)
        {
            _grnRepository = grnRepository;
            _grnItemService = grnItemService;
        }

        public async Task<GrnResponseDto> CreateAsync(GrnRequestDto grnRequestDto)
        {
            var grn = grnRequestDto.ToGrn();
            grn.GenerateCode();
            grn.State = State.OPEN;

            var createdGrn = await _grnRepository.CreateAsync(grn);

            return createdGrn.ToResponseDto();
        }

        public async Task<bool> DeleteAsync(string code)
        {
            var grn = await _grnRepository.DeleteAsync(code);

            if (grn == null)
            {
                return false;
            }

            return true;
        }

        public async Task<GrnResponseDto?> GetByCodeAsync(string code)
        {
            var grn = await _grnRepository.GetByCodeAsync(code);

            if (grn == null)
            {
                return null;
            }

            return grn.ToResponseDto();
        }

        public async Task<Page<GrnResponseDto>> GetAllAsync(QueryObject query)
        {
            var pagedGrns = await _grnRepository.GetAllAsync(query);

            var pagedDto = new Page<GrnResponseDto>
            {
                PageNumber = pagedGrns.PageNumber,
                PageSize = pagedGrns.PageSize,
                TotalElements = pagedGrns.TotalElements,
                Content = pagedGrns.Content.Select(g => g.ToResponseDto()).ToList()
            };

            return pagedDto;
        }

        public async Task<GrnResponseDto> UpdateAsync(string code, GrnRequestDto grnRequestDto)
        {
            var updated = await _grnRepository.UpdateAsync(code, grnRequestDto.ToGrn());
            if (updated == null)
                throw new KeyNotFoundException($"GRN {code} non trovata");

            return updated.ToResponseDto();
        }

        public async Task<GrnResponseDto?> GetByIdAsync(long id)
        {
            var grn = await _grnRepository.GetById(id);
            return grn.ToResponseDto();
        }

        public async Task<GrnResponseDto> UpdateStateAsync(string code, State state)
        {
            var updated = await _grnRepository.GetByCodeAsync(code) ?? throw new KeyNotFoundException($"GRN {code} non trovata");
            updated.State = state;
            await _grnRepository.UpdateAsync(code, updated);

            return updated.ToResponseDto();
        }
    }
}
