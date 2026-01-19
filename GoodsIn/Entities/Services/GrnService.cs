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

        public async Task<List<GrnResponseDto>> GetAllAsync(QueryObject query)
        {
            var grns = await _grnRepository.GetAllAsync();

            //if (!string.IsNullOrWhiteSpace(query.Code))
            //{
            //    grns = grns.Where(p => p.Code.Contains(query.Code));
            //}
            //if (!string.IsNullOrWhiteSpace(query.SortBy))
            //{
            //    if (query.SortBy.Equals("Code", StringComparison.OrdinalIgnoreCase))
            //    {
            //        grns = query.IsDescending ? grns.OrderByDescending(s => s.Code) : grns.OrderBy(s => s.Code);
            //    }
            //}

            //var skipNumber = (query.PageNumber - 1) * query.PageSize;
            //return await grns.Skip(skipNumber).Take(query.PageSize).ToListAsync();






            return grns.Select(grn => grn.ToResponseDto()).ToList();
        }

        public async Task<GrnResponseDto> UpdateAsync(string code, GrnRequestDto grnRequestDto)
        {
            var updated = await _grnRepository.UpdateAsync(code, grnRequestDto.ToGrn());
            if (updated == null)
                throw new KeyNotFoundException($"GRN {code} non trovata");

            return updated.ToResponseDto();
        }
    }
}
