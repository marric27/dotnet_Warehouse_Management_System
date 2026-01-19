using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Data;
using dotnet_Warehouse_Management_System.GoodsIn.Dtos;
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
            item.GenerateCode();
            await _context.GrnItems.AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<GrnItem?> DeleteAsync(string code)
        {
            var item = await _context.GrnItems.FirstOrDefaultAsync(x => x.Code == code);
            if (item == null)
            {
                return null;
            }
            _context.GrnItems.Remove(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<List<GrnItem>> GetAllAsync(QueryObject query)
        {
            var items = _context.GrnItems.AsNoTracking().AsQueryable();

            return await items.Skip(1).Take(1).ToListAsync();
        }

        public async Task<GrnItem?> GetByCodeAsync(string code)
        {
            return await _context.GrnItems.AsNoTracking().Where(i => i.Code == code).FirstOrDefaultAsync();
        }

        public Task<GrnItem> UpdateAsync(string code, GrnItemRequestDto itemDto)
        {
            throw new NotImplementedException();
        }
    }
}
