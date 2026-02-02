using dotnet_Warehouse_Management_System.BaseRepository;
using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Warehouses.Entities.Dtos;

namespace dotnet_Warehouse_Management_System.Warehouses.Entities.Repositories
{
    public interface ISlotRepository : IBaseRepository<Slot>
    {
        Task<Page<Slot>> GetAllPaginatedAsync(QueryObject query);
        Task<Slot?> GetByCodeAsync(string code, bool track);
        Task<Slot?> GetSlotContainingProductAsync(string productCode);
    }
}
