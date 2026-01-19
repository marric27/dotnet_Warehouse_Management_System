using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.GoodsIn.Dtos;
using dotnet_Warehouse_Management_System.GoodsIn.Entities.Mappers;
using dotnet_Warehouse_Management_System.GoodsIn.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace dotnet_Warehouse_Management_System.GoodsIn.Receiving
{
    public class ReceivingService
    {
        private readonly IGrnService _grnService;
        private readonly IGrnItemService _grnItemService;

        public ReceivingService(IGrnService grnService, IGrnItemService grnItemService)
        {
            _grnService = grnService;
            _grnItemService = grnItemService;
        }

        public Task<GrnResponseDto> CreateGrn(GrnRequestDto grnRequestDto)
        {
            return _grnService.CreateAsync(grnRequestDto);
        }

        public async Task<GrnItemResponseDto> CreateGrnItemForExistingGrnByCodeAsync(string grncode, GrnItemRequestDto grnItemRequestDto)
        {
            return await _grnItemService.CreateGrnItemForExistingGrnByCodeAsync(grncode, grnItemRequestDto);
        }

        public Task<List<GrnResponseDto>> GetAllGrnsAsync(QueryObject query)
        {
            return _grnService.GetAllAsync(query);
        }

        public Task<List<GrnItemResponseDto>> GetAllGrnItemsAsync(QueryObject query)
        {
            return _grnItemService.GetAllAsync(query);
        }

        public Task<GrnResponseDto> GetGrnByCodeAsync(string code)
        {
            return _grnService.GetByCodeAsync(code);
        }

        public Task<GrnItemResponseDto> GetGrnItemByCodeAsync(string code)
        {
            return _grnItemService.GetByCodeAsync(code);
        }
    }
}
