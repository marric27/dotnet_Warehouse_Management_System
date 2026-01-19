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

        public async Task<List<Grn>> GetAllAsync()
        {
            return await _context.Grns.Include(g => g.Items).AsNoTracking().AsQueryable()
                     .ToListAsync();
}

        public async Task<Grn?> GetByCodeAsync(string code)
        {
            return await _context.Grns.Include(g => g.Items).AsNoTracking().Where(g => g.Code == code).FirstOrDefaultAsync();
        }

        public async Task<Grn> UpdateAsync(string code, Grn grn)
        {
            _context.Grns.Update(grn);
            await _context.SaveChangesAsync();
            return grn;
        }

    }
}
