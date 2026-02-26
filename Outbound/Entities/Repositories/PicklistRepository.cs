using dotnet_Warehouse_Management_System.BaseRepository;
using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Data;
using Microsoft.EntityFrameworkCore;

namespace dotnet_Warehouse_Management_System.Outbound.Entities.Repositories
{
    public class PicklistRepository(ApplicationDBContext context) : BaseRepository<Picklist>(context), IPicklistRepository
    {
        public override async Task<List<Picklist>> GetAllAsync()
        {
            return await context.Picklists.AsNoTracking().Include(p => p.PicklistItemList).ToListAsync();
        }

        public async Task<Page<Picklist>> GetAllPaginatedAsync(QueryObject query)
        {
            return await context.Picklists
                .AsNoTracking()
                .Where(p => string.IsNullOrEmpty(query.Code) || p.Code.Contains(query.Code))
                .OrderBy(p => p.Id)
                .ToPagedListAsync(query.PageNumber, query.PageSize);
        }

        public async Task<List<Picklist>> CreateRangeAsync(List<Picklist> picklists)
        {
            return await base.CreateRangeAsync(picklists);
        }

        public async Task<Picklist?> GetByCodeAsync(string code, bool track)
        {
            var query = context.Picklists.AsQueryable();
            if (!track) query = query.AsNoTracking();
            return await query
                .Include(p => p.PicklistItemList)
                .FirstOrDefaultAsync(p => p.Code == code);
        }
    }
}
