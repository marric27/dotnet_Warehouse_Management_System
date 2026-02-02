using dotnet_Warehouse_Management_System.BaseRepository;
using dotnet_Warehouse_Management_System.Common.Helpers;

namespace dotnet_Warehouse_Management_System.GoodsIn.Entities.Repositories
{
    public interface IGrnItemRepository : IBaseRepository<GrnItem>
    {
        Task<GrnItem?> GetById(long id, bool track);
        Task<GrnItem?> GetByCodeAsync(string code, bool track);
    }
}
