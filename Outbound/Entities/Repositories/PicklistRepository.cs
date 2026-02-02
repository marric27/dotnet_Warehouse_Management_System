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
            return await _context.Picklists
                .AsNoTracking()
                .Where(p => string.IsNullOrEmpty(query.Code) || p.Code.Contains(query.Code))
                .OrderBy(p => p.Id)
                .ToPagedListAsync(query.PageNumber, query.PageSize);
        }

        public async Task<Picklist?> GetByCodeAsync(string code)
        {
            return await context.Picklists
                .Include(p => p.PicklistItemList)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Code == code);
        }
    }
}
