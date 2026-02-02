using dotnet_Warehouse_Management_System.BaseRepository;

namespace dotnet_Warehouse_Management_System.GoodsIn.Entities.Repositories
{
    public interface ICheckingInfoRepository : IBaseRepository<CheckingInfo>
    {
        Task<CheckingInfo?> GetByIdAsync(long id, bool track);
        Task<CheckingInfo?> GetByCodeAsync(string code, bool track);
        Task<CheckingInfo?> GetByStockUnitIdAsync(long stockUnitId);
    }
}
