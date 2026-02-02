using dotnet_Warehouse_Management_System.BaseRepository;
using dotnet_Warehouse_Management_System.Data;
using Microsoft.EntityFrameworkCore;

namespace dotnet_Warehouse_Management_System.GoodsIn.Entities.Repositories
{
    public class StockUnitRepository(ApplicationDBContext context) : BaseRepository<StockUnit>(context), IStockUnitRepository
    {
        public async Task<StockUnit> GetByIdAsync(long id, bool track = false)
        {
            var query = context.StockUnits.AsQueryable();
            if (!track) query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<StockUnit?> GetByCodeAsync(string code, bool track = false)
        {
            var query = context.StockUnits.AsQueryable();
            if (!track) query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(s => s.Code == code);
        }

        public async Task<List<StockUnit>> GetByCodesAsync(List<string> codes)
        {
            return await context.StockUnits
                .Where(su => codes.Contains(su.Code))
                .ToListAsync();
        }
    }
}