using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.GoodsIn.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_Warehouse_Management_System.GoodsIn.Services
{
    public interface IGrnItemService
    {
        Task<GrnItemResponseDto> CreateAsync(GrnItemRequestDto grnItemRequestDto);
        Task<GrnItemResponseDto?> GetByIdAsync(long Id);
        Task<GrnItemResponseDto?> GetByCodeAsync(string code);

        Task<List<GrnItemResponseDto>> GetAllAsync(QueryObject query);

        Task<GrnItemResponseDto> DeleteAsync(string code);

        Task<GrnItemResponseDto> UpdateAsync(string code, GrnItemRequestDto grnItemRequestDto);

        Task<GrnItemResponseDto> CreateGrnItemForExistingGrnByCodeAsync(string grnCode, GrnItemRequestDto grnItemDto);
    }
}
