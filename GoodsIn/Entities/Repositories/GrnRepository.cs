using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Data;
using Microsoft.EntityFrameworkCore;

namespace dotnet_Warehouse_Management_System.GoodsIn.Entities.Repositories
{
    public class GrnRepository : IGrnRepository
    {
        private readonly ApplicationDBContext _context;
        public GrnRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Grn> CreateAsync(Grn grn)
        {
            await _context.Grns.AddAsync(grn);
            await _context.SaveChangesAsync();
            return grn;
        }

        public async Task<bool> DeleteAsync(string code)
        {
            var grn = await GetByCodeAsync(code);
            if (grn != null)
            {
                _context.Grns.Remove(grn);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Page<Grn>> GetAllAsync(QueryObject query)
        {
            int pageNumber = Math.Max(0, query.PageNumber);
            int pageSize = Math.Clamp(query.PageSize, 1, 100);

            var grnsQuery = _context.Grns.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Code))
            {
                grnsQuery = grnsQuery.Where(g => g.Code.Contains(query.Code));
            }

            grnsQuery = query.SortBy?.ToLower() switch
            {
                "code" => query.IsDescending
                    ? grnsQuery.OrderByDescending(g => g.Code)
                    : grnsQuery.OrderBy(g => g.Code),

                _ => query.IsDescending
                    ? grnsQuery.OrderByDescending(g => g.Id)
                    : grnsQuery.OrderBy(g => g.Id)
            };

            var totalItems = await grnsQuery.CountAsync();

            var items = await grnsQuery
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new Page<Grn>
            {
                Content = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalElements = totalItems
            };
        }

        public async Task<Grn?> GetByCodeAsync(string code)
        {
            return await _context.Grns.Include(g => g.Items).AsNoTracking().Where(g => g.Code == code).FirstOrDefaultAsync();
        }

        public async Task<Grn?> GetById(long id)
        {
            return await _context.Grns
                .Include(g => g.Items)
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Grn> UpdateAsync(string code, Grn grn)
        {
            _context.Grns.Update(grn);
            await _context.SaveChangesAsync();
            return grn;
        }

    }
}
