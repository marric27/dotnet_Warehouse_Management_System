using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Data;
using dotnet_Warehouse_Management_System.GoodsIn.Dtos;
using dotnet_Warehouse_Management_System.GoodsIn.Entities.Mappers;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace dotnet_Warehouse_Management_System.GoodsIn.Entities.Repositories
{
    public class GrnItemRepository : IGrnItemRepository
    {
        private readonly ApplicationDBContext _context;
        public GrnItemRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<GrnItem> CreateAsync(GrnItem item)
        {
            await _context.GrnItems.AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<bool> DeleteAsync(string code)
        {
            var item = await GetByCodeAsync(code);
            if (item != null)
            {
                _context.GrnItems.Remove(item);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<GrnItem>> GetAllAsync(QueryObject query)
        {
            return await _context.GrnItems.AsNoTracking().AsQueryable()
                     .ToListAsync();
        }

        public async Task<GrnItem?> GetByCodeAsync(string code)
        {
            return await _context.GrnItems.AsNoTracking().Where(i => i.Code == code).FirstOrDefaultAsync();
        }

        public async Task<GrnItem> UpdateAsync(string code, GrnItem item)
        {
            _context.GrnItems.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }

    }
}
