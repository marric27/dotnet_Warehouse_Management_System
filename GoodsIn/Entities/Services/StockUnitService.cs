using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.GoodsIn.Dtos;
using dotnet_Warehouse_Management_System.GoodsIn.Entities;
using dotnet_Warehouse_Management_System.GoodsIn.Entities.Mappers;
using dotnet_Warehouse_Management_System.GoodsIn.Entities.Repositories;

namespace dotnet_Warehouse_Management_System.GoodsIn.Entities.Services
{
    public class StockUnitService : IStockUnitService
    {
        private readonly IStockUnitRepository _stockUnitRepository;
        public StockUnitService(IStockUnitRepository stockUnitRepository)
        {
            _stockUnitRepository = stockUnitRepository;
        }

        public async Task<StockUnitResponseDto?> GetByCodeAsync(string code)
        {
            var stockUnit = await _stockUnitRepository.GetByCodeAsync(code);
            return stockUnit?.ToResponseDto();
        }

        public async Task<StockUnitResponseDto> CreateAsync(StockUnitRequestDto stockUnitRequestDto)
        {
            var stockUnit = stockUnitRequestDto.ToStockUnit();
            stockUnit.GenerateCode();
            stockUnit.Category = Category.STANDARD;

            var created = await _stockUnitRepository.CreateAsync(stockUnit);
            return created.ToResponseDto();
        }

        public async Task<StockUnitResponseDto> UpdateAsync(string code, StockUnitRequestDto stockUnitRequestDto)
        {
            var updated = await _stockUnitRepository.UpdateAsync(code, stockUnitRequestDto.ToStockUnit());
            return updated?.ToResponseDto();
        }

        public async Task<StockUnitResponseDto?> DeleteAsync(string code)
        {
            var deleted = await _stockUnitRepository.DeleteAsync(code);
            return deleted?.ToResponseDto();
        }

        public async Task<StockUnitResponseDto?> GetByIdAsync(long id)
        {
            var stockUnit = await _stockUnitRepository.GetById(id);
            return stockUnit.ToResponseDto();
        }
        
    }
}