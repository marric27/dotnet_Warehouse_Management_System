using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.GoodsIn.Dtos;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace dotnet_Warehouse_Management_System.GoodsIn.Services
{
    public interface IGrnItemService
    {
        Task<GrnItemResponseDto> CreateAsync(GrnItemRequestDto grnItemRequestDto);
        Task<GrnItemResponseDto?> GetByIdAsync(long Id);
        Task<GrnItemResponseDto?> GetByCodeAsync(string code);

        Task<List<GrnItemResponseDto>> GetAllAsync(QueryObject query);

        Task<bool> DeleteAsync(string code);

        Task<GrnItemResponseDto> UpdateAsync(string code, GrnItemResponseDto grnItemResponseDto);
        Task<GrnItemResponseDto> UpdateStateAsync(string code, State state);

        Task<GrnItemResponseDto> CreateGrnItemForExistingGrnByCodeAsync(string grnCode, GrnItemRequestDto grnItemDto);
        Task AddCheckingInfo(string grnItemCode, string checkingInfoCode);
    }
}
