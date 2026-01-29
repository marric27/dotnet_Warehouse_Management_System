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
    public class GrnService(IGrnRepository grnRepository, IGrnItemService grnItemService) : IGrnService
    {
        public async Task<GrnResponseDto> CreateAsync(GrnRequestDto grnRequestDto)
        {
            var grn = grnRequestDto.ToGrn();
            grn.GenerateCode();
            grn.State = State.OPEN;

            var createdGrn = await grnRepository.CreateAsync(grn);

            return createdGrn.ToResponseDto();
        }

        public async Task<bool> DeleteAsync(string code)
        {
            var grn = await grnRepository.GetByCodeAsync(code, true);

            if (grn == null)
            {
                return false;
            }
            await grnRepository.DeleteAsync(grn);
            return true;
        }

        public async Task<GrnResponseDto?> GetByCodeAsync(string code)
        {
            var grn = await grnRepository.GetByCodeAsync(code, false);

            if (grn == null)
            {
                return null;
            }

            return grn.ToResponseDto();
        }

        public async Task<Page<GrnResponseDto>> GetAllAsync(QueryObject query)
        {
            var pagedGrns = await grnRepository.GetAllAsync(query);

            var pagedDto = new Page<GrnResponseDto>
            {
                PageNumber = pagedGrns.PageNumber,
                PageSize = pagedGrns.PageSize,
                TotalElements = pagedGrns.TotalElements,
                Content = pagedGrns.Content.Select(g => g.ToResponseDto()).ToList()
            };

            return pagedDto;
        }
        public async Task<GrnResponseDto> UpdateAsync(GrnResponseDto grnDto)
        {
            var existingGrn = await grnRepository.GetByCodeAsync(grnDto.Code, true) ?? throw new KeyNotFoundException();
            existingGrn.State = grnDto.State;
            await grnRepository.UpdateAsync();
            return existingGrn.ToResponseDto();
        }

        public async Task<GrnResponseDto?> GetByIdAsync(long id)
        {
            var grn = await grnRepository.GetById(id, false);
            return grn.ToResponseDto();
        }
    }
}
