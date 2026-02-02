using dotnet_Warehouse_Management_System.BaseRepository;
using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Data;
using dotnet_Warehouse_Management_System.Outbound.Entities;
using Microsoft.EntityFrameworkCore;

namespace dotnet_Warehouse_Management_System.Warehouses.Entities.Repositories
{
    public class SlotRepository(ApplicationDBContext context) : BaseRepository<Slot>(context), ISlotRepository
    {
        public async Task<Page<Slot>> GetAllPaginatedAsync(QueryObject query)
        {
            return await context.Slots
                .AsNoTracking()
                .Where(p => string.IsNullOrEmpty(query.Code) || p.Code.Contains(query.Code))
                .OrderBy(p => p.Id)
                .ToPagedListAsync(query.PageNumber, query.PageSize);
        }

        public async Task<Slot?> GetByCodeAsync(string code, bool track)
        {
            var query = context.Slots.AsQueryable();
            if (!track) query = query.AsNoTracking().Include(s => s.StockUnits);
            return await query.FirstOrDefaultAsync(p => p.Code == code);
        }

        public async Task<Slot?> GetSlotContainingProductAsync(string productCode)
        {
            return await context.Slots
                .AsNoTracking()
                .Where(s => s.StockUnits.Any(su => su.ProductCode == productCode))
                .OrderBy(s => s.PickingSequence)
                .FirstOrDefaultAsync();
        }

        public override async Task<List<Slot>> GetAllAsync()
        {
            return await context.Slots.AsNoTracking().Include(s => s.StockUnits).ToListAsync();
        }
    }
}
