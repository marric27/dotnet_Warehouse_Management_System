using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;

namespace dotnet_Warehouse_Management_System.Outbound.Entities.Repositories
{
    public interface IPicklistRepository
    {
        Task<Page<Picklist>> GetAllPaginatedAsync(QueryObject query);
        Task<Picklist?> GetByCodeAsync(string code);
        Task<Picklist> CreateAsync(Picklist picklist);
        Task<List<Picklist>> GetAllAsync();
    }
}
