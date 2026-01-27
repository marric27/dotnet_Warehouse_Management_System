using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_Warehouse_Management_System.GoodsIn.Entities.Repositories;
using dotnet_Warehouse_Management_System.Products.Entities.Mappers;
using dotnet_Warehouse_Management_System.Products.Entities.Repository;


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

        public async Task<StockUnitResponseDto> CreateAsync(StockUnitRequestDto stockUnitResponseDto)
        {
            var stockUnit = stockUnitResponseDto.ToStockUnit();
            stockUnit.GenerateCode();

            var created = await _stockUnitRepository.CreateAsync(stockUnit);
            return created.ToResponseDto();
        }

        public async Task<StockUnitResponseDto?> UpdateAsync(string code, StockUnitRequestDto stockUnitResponseDto)
        {
            var updated = await _stockUnitRepository.UpdateAsync(code, stockUnitResponseDto);
            return updated?.ToResponseDto();
        }

        public async Task<StockUnitResponseDto?> DeleteAsync(string code)
        {
            var deleted = await _stockUnitRepository.DeleteAsync(code);
            return deleted?.ToResponseDto();
        }
        
    }
}