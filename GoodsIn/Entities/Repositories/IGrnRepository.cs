using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.GoodsIn.Dtos;

namespace dotnet_Warehouse_Management_System.GoodsIn.Entities.Repositories
{
    public interface IGrnRepository
    {
        Task<List<Grn>> GetAllAsync(QueryObject query);
        Task<Grn?> GetByCodeAsync(string code);
        Task<Grn> CreateAsync(Grn grn);
        Task<Grn> UpdateAsync(string code, GrnRequestDto grnDto);
        Task<Grn?> DeleteAsync(string code);
    }
}
