using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Data;
using dotnet_Warehouse_Management_System.GoodsIn.Dtos;
using dotnet_Warehouse_Management_System.GoodsIn.Entities.Mappers;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace dotnet_Warehouse_Management_System.GoodsIn.Entities.Repositories
{
    public class GrnItemRepository(ApplicationDBContext context) : IGrnItemRepository
    {
        public async Task<GrnItem> CreateAsync(GrnItem item)
        {
            await context.GrnItems.AddAsync(item);
            await context.SaveChangesAsync();
            return item;
        }

        public async Task DeleteAsync(GrnItem item)
        {
            context.GrnItems.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task<List<GrnItem>> GetAllAsync(QueryObject query)
        {
            return await context.GrnItems.AsNoTracking().AsQueryable().ToListAsync();
        }

        public async Task<GrnItem?> GetByCodeAsync(string code, bool track)
        {
            var query = context.GrnItems.AsQueryable();
            if (!track) query = query.AsNoTracking();
            return await query.Include(x => x.CheckingInfoList).FirstOrDefaultAsync(p => p.Code == code);
        }

        public async Task<GrnItem?> GetById(long id, bool track)
        {
            var query = context.GrnItems.AsQueryable();
            if (!track) query = query.AsNoTracking();
            return await query.Include(x => x.CheckingInfoList).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateAsync() => await context.SaveChangesAsync();
        
        public async Task<GrnItem?> UpdateStateAsync(string code, State newState)
        {
            var entity = await context.GrnItems.FirstOrDefaultAsync(x => x.Code == code);

            if (entity != null)
            {
                entity.State = newState;
                await context.SaveChangesAsync();
            }
            return entity;
        }

    }
}
