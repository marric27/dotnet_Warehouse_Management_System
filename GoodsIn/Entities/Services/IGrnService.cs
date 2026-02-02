using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.GoodsIn.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_Warehouse_Management_System.GoodsIn.Services
{
    public interface IGrnService
    {
        Task<GrnResponseDto> CreateAsync(GrnRequestDto grnRequestDto);
        Task<bool> DeleteAsync(string code);
        Task<GrnResponseDto?> GetByCodeAsync(string code);
        Task<GrnResponseDto?> GetByIdAsync(long id);
        Task<Page<GrnResponseDto>> GetAllAsync(QueryObject query);
        Task<GrnResponseDto> UpdateAsync(GrnResponseDto grnDto);
    }
}
