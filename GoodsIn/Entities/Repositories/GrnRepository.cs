using dotnet_Warehouse_Management_System.BaseRepository;
using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Data;
using Microsoft.EntityFrameworkCore;

namespace dotnet_Warehouse_Management_System.GoodsIn.Entities.Repositories
{
    public class GrnRepository(ApplicationDBContext context) : BaseRepository<Grn>(context), IGrnRepository
    {
        public async Task<Page<Grn>> GetAllPaginatedAsync(QueryObject query)
        {
            return await context.Grns
                .AsNoTracking()
                .Where(p => string.IsNullOrEmpty(query.Code) || p.Code.Contains(query.Code))
                .OrderBy(p => p.Id)
                .ToPagedListAsync(query.PageNumber, query.PageSize);
        }

        public async Task<Grn?> GetByCodeAsync(string code, bool track)
        {
            var query = context.Grns.AsQueryable();
            if (!track) query = query.AsNoTracking();
            return await query.Include(x => x.Items).FirstOrDefaultAsync(p => p.Code == code);
        }

        public async Task<Grn?> GetByIdAsync(long id, bool track)
        {
            var query = context.Grns.AsQueryable();
            if (!track) query = query.AsNoTracking();
            return await query.Include(x => x.Items).FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}
