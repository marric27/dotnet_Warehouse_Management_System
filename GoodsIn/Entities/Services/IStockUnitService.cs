using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.GoodsIn.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_Warehouse_Management_System.GoodsIn.Entities.Services
{
    public interface IStockUnitService
    {
        Task<StockUnitResponseDto> CreateAsync(StockUnitRequestDto stockUnitRequestDto);
        Task<StockUnitResponseDto> DeleteAsync(string code);
        Task<StockUnitResponseDto?> GetByCodeAsync(string code);
        Task<StockUnitResponseDto?> GetByIdAsync(long id);
        Task<StockUnitResponseDto?> UpdateAsync(string code, StockUnitRequestDto stockUnitRequestDto);
    }
}