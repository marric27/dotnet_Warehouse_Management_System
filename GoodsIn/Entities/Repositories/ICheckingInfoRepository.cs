namespace dotnet_Warehouse_Management_System.GoodsIn.Entities.Repositories
{
    public interface ICheckingInfoRepository
    {
        Task<List<CheckingInfo>> GetAllAsync();
        Task<CheckingInfo?> GetByIdAsync(long id, bool track);
        Task<CheckingInfo?> GetByCodeAsync(string code, bool track);
        Task<CheckingInfo> CreateAsync(CheckingInfo entity);
        Task UpdateAsync();
        Task DeleteAsync(CheckingInfo entity);
        Task<CheckingInfo?> GetByStockUnitIdAsync(long stockUnitId);
        Task<CheckingInfo> UpdateStateAsync(CheckingInfo checkingInfo);
    }
}
