using dotnet_Warehouse_Management_System.BaseRepository;
using dotnet_Warehouse_Management_System.Data;
using Microsoft.EntityFrameworkCore;

namespace dotnet_Warehouse_Management_System.GoodsIn.Entities.Repositories
{
    public class CheckingInfoRepository(ApplicationDBContext context) : BaseRepository<CheckingInfo>(context), ICheckingInfoRepository
    {
        public async Task<CheckingInfo?> GetByCodeAsync(string code, bool track = false)
        {
            var query = context.CheckingInfos.AsQueryable();
            if (!track) query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(p => p.Code == code);
        }

        public async Task<CheckingInfo> GetByIdAsync(long id, bool track = false)
        {
            var query = context.CheckingInfos.AsQueryable();
            if (!track) query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<CheckingInfo?> GetByStockUnitIdAsync(long stockUnitId)
        {
            return await context.CheckingInfos.FirstOrDefaultAsync(ci => ci.StockUnitId == stockUnitId);
        }
    }
}
