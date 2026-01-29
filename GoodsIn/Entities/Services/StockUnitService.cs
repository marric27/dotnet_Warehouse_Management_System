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
        private readonly IStockUnitRepository _stockUnitRepository = stockUnitRepository;

        public async Task<StockUnitResponseDto?> GetByCodeAsync(string code)
        {
            var stockUnit = await _stockUnitRepository.GetByCodeAsync(code);
            return stockUnit?.ToResponseDto();
        }

        public async Task<StockUnitResponseDto> CreateAsync(StockUnitRequestDto stockUnitRequestDto)
        {
            var stockUnit = stockUnitRequestDto.ToStockUnit();
            stockUnit.GenerateCode();
            var product = await productRepository.GetByCodeAsync(stockUnitRequestDto.ProductCode);

            var created = await _stockUnitRepository.CreateAsync(stockUnit);
            return created.ToResponseDto();
        }

        public async Task<StockUnitResponseDto> UpdateAsync(StockUnitResponseDto stockUnitResponseDto)
        {
            var updated = await _stockUnitRepository.UpdateAsync(stockUnitResponseDto.ToStockUnit());
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

        public async Task<StockUnitResponseDto?> AssingToSlotAsync(string suCode, string slotCode)
        {
            // 1. Recupero delle entità tramite i codici (usando i tuoi metodi del repo)
            var stockUnit = await _stockUnitRepository.GetByCodeAsync(suCode)
                            ?? throw new KeyNotFoundException($"StockUnit {suCode} not found");

            var slot = await slotRepository.GetByCodeAsync(slotCode)
                       ?? throw new KeyNotFoundException($"Slot {slotCode} not found");

            // 2. Eseguiamo l'assegnazione fisica dell'ID dello slot sulla StockUnit
            stockUnit.SlotId = slot.Id;

            // 3. Chiamata al repo per il salvataggio
            // Passiamo l'entità modificata al repository
            var su = await _stockUnitRepository.UpdateAsync(stockUnit);

            // 4. Mappatura verso il DTO di risposta
            return su.ToResponseDto();
        }

        public async Task<List<StockUnitResponseDto>> GetAllAsync()
        {
            var stockunits = await _stockUnitRepository.GetAllAsync();
            return stockunits.Select(su => su.ToResponseDto()).ToList();
        }

        public async Task<List<StockUnitResponseDto>> GetByCodesAsync(List<string> codes)
        {
            var stockUnits = await _stockUnitRepository.GetByCodesAsync(codes);

            return stockUnits
                .Select(su => su.ToResponseDto())
                .ToList();
        }

    }
}