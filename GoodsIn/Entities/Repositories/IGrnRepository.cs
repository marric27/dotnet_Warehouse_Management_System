using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.GoodsIn.Dtos;

namespace dotnet_Warehouse_Management_System.GoodsIn.Entities.Repositories
{
    public interface IGrnRepository
    {
        Task<Page<Grn>> GetAllAsync(QueryObject query);
        Task<Grn?> GetById(long id);
        Task<Grn?> GetByCodeAsync(string code);
        Task<Grn> CreateAsync(Grn grn);
        Task<Grn> UpdateAsync(string code, Grn grn);
        Task<Grn?> DeleteAsync(string code);
    }
}
