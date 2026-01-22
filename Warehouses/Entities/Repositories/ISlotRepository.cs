using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Warehouses.Entities.Dtos;

namespace dotnet_Warehouse_Management_System.Warehouses.Entities.Repositories
{
    public interface ISlotRepository
    {
        Task<Page<Slot>> GetAllAsync(QueryObject query);
        Task<Slot?> GetByCodeAsync(string code);
        Task<Slot> CreateAsync(Slot slot);
        Task<Slot> UpdateAsync(string code, SlotRequestDto slotDto);
        Task<Slot?> DeleteAsync(string code);
        Task<Slot?> GetSlotContainingProduct(string productCode);
    }
}
