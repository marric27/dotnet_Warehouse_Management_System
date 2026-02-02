using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.GoodsIn.Dtos;
using dotnet_Warehouse_Management_System.GoodsIn.Entities;
using dotnet_Warehouse_Management_System.GoodsIn.Entities.Mappers;
using dotnet_Warehouse_Management_System.GoodsIn.Entities.Repositories;
using dotnet_Warehouse_Management_System.Products.Entities.Repository;
using dotnet_Warehouse_Management_System.Warehouses.Entities.Repositories;

namespace dotnet_Warehouse_Management_System.GoodsIn.Entities.Services
{
    public class StockUnitService(IStockUnitRepository stockUnitRepository, IProductRepository productRepository, ISlotRepository slotRepository) : IStockUnitService
    {
        public async Task<StockUnitResponseDto?> GetByCodeAsync(string code)
        {
            var stockUnit = await stockUnitRepository.GetByCodeAsync(code, false);
            return stockUnit?.ToResponseDto();
        }

        public async Task<StockUnitResponseDto> CreateAsync(StockUnitRequestDto stockUnitRequestDto)
        {
            var stockUnit = stockUnitRequestDto.ToStockUnit();
            stockUnit.GenerateCode();

            var created = await stockUnitRepository.CreateAsync(stockUnit);
            return created.ToResponseDto();
        }

        public async Task<StockUnitResponseDto> UpdateAsync(StockUnitResponseDto stockUnitResponseDto)
        {
            var existingStockUnit = await stockUnitRepository.GetByCodeAsync(stockUnitResponseDto.Code, true) ?? throw new KeyNotFoundException($"StockUnit {stockUnitResponseDto.Code} not found");
            existingStockUnit.Quantity = stockUnitResponseDto.Quantity;
            existingStockUnit.SlotId = stockUnitResponseDto.SlotId;
            existingStockUnit.BatchNumber = stockUnitResponseDto.BatchNumber;

            await stockUnitRepository.UpdateAsync();
            return existingStockUnit.ToResponseDto();
        }

        public async Task<bool> DeleteAsync(string code)
        {
            var su = await stockUnitRepository.GetByCodeAsync(code, true);
            if(su == null) return false;
            await stockUnitRepository.DeleteAsync(su);
            return true;
        }

        public async Task<StockUnitResponseDto?> GetByIdAsync(long id)
        {
            var stockUnit = await stockUnitRepository.GetByIdAsync(id, false);
            return stockUnit.ToResponseDto();
        }

        public async Task<List<StockUnitResponseDto>> GetAllAsync()
        {
            var stockunits = await stockUnitRepository.GetAllAsync();
            return stockunits.Select(su => su.ToResponseDto()).ToList();
        }

        public async Task<List<StockUnitResponseDto>> GetByCodesAsync(List<string> codes)
        {
            var stockUnits = await stockUnitRepository.GetByCodesAsync(codes);

            return stockUnits
                .Select(su => su.ToResponseDto())
                .ToList();
        }

    }
}