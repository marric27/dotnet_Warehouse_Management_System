using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.GoodsIn.Dtos;

namespace dotnet_Warehouse_Management_System.GoodsIn.Entities.Repositories
{
    public interface IGrnRepository
    {
        Task<Page<Grn>> GetAllAsync(QueryObject query);
        Task<Grn?> GetById(long id, bool track);
        Task<Grn?> GetByCodeAsync(string code, bool track);
        Task<Grn> CreateAsync(Grn grn);
        Task UpdateAsync();
        Task<Grn> UpdateStateAsync(string code, State newState);
        Task DeleteAsync(Grn grn);
    }
}
