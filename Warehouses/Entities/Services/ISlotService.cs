using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Products.Entities.Dtos;
using dotnet_Warehouse_Management_System.Warehouses.Entities.Dtos;

namespace dotnet_Warehouse_Management_System.Products.Entities.Services
{
    public interface ISlotService
    {
        Task<Page<SlotResponseDto>> GetAllAsync(QueryObject query);
        Task<List<SlotResponseDto>> GetAllAsync();
        Task<SlotResponseDto?> GetByCodeAsync(string code);
        Task<SlotResponseDto> CreateAsync(SlotRequestDto slotDto);
        Task<SlotResponseDto?> UpdateAsync(string code, SlotRequestDto slotDto);
        Task<bool> DeleteAsync(string code);
        Task<SlotResponseDto?> GetSlotContainingProduct(string productCode);
        Task<Dictionary<string, SlotResponseDto>> GetBestSlotsForProducts(IEnumerable<string> productCodes);
    }
}
