using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Data;
using Microsoft.EntityFrameworkCore;

namespace dotnet_Warehouse_Management_System.GoodsIn.Entities.Repositories
{
    public class GrnRepository(ApplicationDBContext context) : IGrnRepository
    {
        public async Task<Grn> CreateAsync(Grn grn)
        {
            await context.Grns.AddAsync(grn);
            await context.SaveChangesAsync();
            return grn;
        }

        public async Task DeleteAsync(Grn grn)
        {
            context.Grns.Remove(grn);
            await context.SaveChangesAsync();
        }

        public async Task<Page<Grn>> GetAllAsync(QueryObject query)
        {
            int pageNumber = Math.Max(0, query.PageNumber);
            int pageSize = Math.Clamp(query.PageSize, 1, 100);

            var grnsQuery = context.Grns.AsNoTracking().AsQueryable();

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

        public async Task<Grn?> GetByCodeAsync(string code, bool track)
        {
            var query = context.Grns.AsQueryable();
            if (!track) query = query.AsNoTracking();
            return await query.Include(x => x.Items).FirstOrDefaultAsync(p => p.Code == code);
        }

        public async Task<Grn?> GetById(long id, bool track)
        {
            var query = context.Grns.AsQueryable();
            if (!track) query = query.AsNoTracking();
            return await query.Include(x => x.Items).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task UpdateAsync() => await context.SaveChangesAsync();

        public async Task<Grn> UpdateStateAsync(string code, State newState)
        {
            var grn = await context.Grns.FirstOrDefaultAsync(g => g.Code == code);
            grn.State = newState;
            await context.SaveChangesAsync();
            return grn;
        }

    }
}
