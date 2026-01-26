using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_Warehouse_Management_System.GoodsIn.Dtos;

namespace dotnet_Warehouse_Management_System.GoodsIn.Entities.Services
{
    public interface IStockUnitService
    {
        Task<StockUnitResponseDto> CreateAsync(StockUnitResponseDto stockUnitResponseDto);
        Task<bool> DeleteAsync(string code);
        Task<StockUnitResponseDto?> GetByCodeAsync(string code);
        Task<StockUnitResponseDto?> GetByIdAsync(long id);
        Task<StockUnitResponseDto> UpdateAsync(string code, StockUnitResponseDto stockUnitResponseDto);
    }
}