
using dotnet_Warehouse_Management_System.Data;
using Microsoft.EntityFrameworkCore;

namespace dotnet_Warehouse_Management_System.GoodsIn.Entities.Repositories
{
    public class CheckingInfoRepository(ApplicationDBContext context) : ICheckingInfoRepository
    {
        public async Task<CheckingInfo> CreateAsync(CheckingInfo entity)
        {
            await context.CheckingInfos.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(CheckingInfo ci)
        {
            context.CheckingInfos.Remove(ci);
            await context.SaveChangesAsync();
        }

        public async Task<List<CheckingInfo>> GetAllAsync()
        {
            return await context.CheckingInfos.AsNoTracking().ToListAsync();
        }

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

        public async Task UpdateAsync() => await context.SaveChangesAsync();
        public async Task<CheckingInfo> UpdateStateAsync(CheckingInfo checkingInfo)
        {
            var entity = await context.CheckingInfos.FindAsync(checkingInfo.Id)
                         ?? throw new KeyNotFoundException("CheckingInfo not found");

            entity.State = checkingInfo.State;

            await context.SaveChangesAsync();
            return entity;
        }
    }
}
