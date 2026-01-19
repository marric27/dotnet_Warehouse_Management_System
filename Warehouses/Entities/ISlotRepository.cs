using dotnet_Warehouse_Management_System.Slots.Helpers;
using dotnet_Warehouse_Management_System.Warehouses.Entities.Dtos;

namespace dotnet_Warehouse_Management_System.Warehouses.Entities
{
    public interface ISlotRepository
    {
        Task<List<Slot>> GetAllAsync(QueryObject query);
        Task<Slot?> GetByCodeAsync(string code);
        Task<Slot> CreateAsync(Slot slot);
        Task<Slot> UpdateAsync(string code, SlotRequestDto slotDto);
        Task<Slot?> DeleteAsync(string code);
    }
}
