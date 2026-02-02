using dotnet_Warehouse_Management_System.BaseRepository;
using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.GoodsIn.Dtos;

namespace dotnet_Warehouse_Management_System.GoodsIn.Entities.Repositories
{
    public interface IGrnRepository : IBaseRepository<Grn>
    {
        Task<Page<Grn>> GetAllPaginatedAsync(QueryObject query);
        Task<Grn?> GetByIdAsync(long id, bool track);
        Task<Grn?> GetByCodeAsync(string code, bool track);
    }
}
