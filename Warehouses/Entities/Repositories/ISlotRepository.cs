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
        Task UpdateAsync();
        Task DeleteAsync(string code);
        Task<Slot?> GetSlotContainingProductAsync(string productCode);
        Task<List<Slot>> GetAllAsync();
    }
}
