using dotnet_Warehouse_Management_System.BaseRepository;

namespace dotnet_Warehouse_Management_System.GoodsIn.Entities.Repositories
{
    public interface IStockUnitRepository : IBaseRepository<StockUnit>
    {
        Task<StockUnit> GetByIdAsync(long id, bool track);
        Task<StockUnit?> GetByCodeAsync(string code, bool track);
        Task<List<StockUnit>> GetByCodesAsync(List<string> codes);
    }
}