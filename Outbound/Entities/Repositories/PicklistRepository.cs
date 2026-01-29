using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Data;
using Microsoft.EntityFrameworkCore;

namespace dotnet_Warehouse_Management_System.Outbound.Entities.Repositories
{
    public class PicklistRepository : IPicklistRepository
    {
        private readonly ApplicationDBContext _context;
        public PicklistRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Picklist> CreateAsync(Picklist picklist)
        {
            await _context.Picklists.AddAsync(picklist);
            await _context.SaveChangesAsync();
            return picklist;
        }

        public async Task<List<Picklist>> GetAllAsync()
        {
            return await _context.Picklists.AsNoTracking().Include(p => p.PicklistItemList).ToListAsync();
        }

        public async Task<Page<Picklist>> GetAllPaginatedAsync(QueryObject query)
        {
            int pageNumber = Math.Max(0, query.PageNumber);
            int pageSize = Math.Clamp(query.PageSize, 1, 100);

            var picklists = _context.Picklists.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Code))
            {
                picklists = picklists.Where(o => o.Code.Contains(query.Code));
            }
            picklists = query.SortBy?.ToLower() switch
            {
                "code" => query.IsDescending
                    ? picklists.OrderByDescending(g => g.Code)
                    : picklists.OrderBy(g => g.Code),

                _ => query.IsDescending
                    ? picklists.OrderByDescending(g => g.Id)
                    : picklists.OrderBy(g => g.Id)
            };

            var totalItems = await picklists.CountAsync();

            var items = await picklists
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new Page<Picklist>
            {
                Content = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalElements = totalItems
            };
        }

        public async Task<Picklist?> GetByCodeAsync(string code)
        {
            return await _context.Picklists
                .Include(p => p.PicklistItemList)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Code == code);
        }
    }
}
