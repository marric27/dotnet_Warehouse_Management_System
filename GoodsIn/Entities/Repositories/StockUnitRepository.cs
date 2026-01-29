using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Data;
using Microsoft.EntityFrameworkCore;

namespace dotnet_Warehouse_Management_System.GoodsIn.Entities.Repositories
{
    public class StockUnitRepository(ApplicationDBContext context) : IStockUnitRepository
    {
        public async Task<StockUnit> CreateAsync(StockUnit stockUnit)
        {
            await context.StockUnits.AddAsync(stockUnit);
            await context.SaveChangesAsync();
            return stockUnit;
        }

        public async Task DeleteAsync(StockUnit stockUnit)
        {
            context.StockUnits.Remove(stockUnit);
            await context.SaveChangesAsync();
        }

        public async Task<StockUnit> GetByIdAsync(long id, bool track = false)
        {
            var query = context.StockUnits.AsQueryable();
            if (!track) query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<StockUnit?> GetByCodeAsync(string code, bool track = false)
        {
            var query = context.StockUnits.AsQueryable();
            if (!track) query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(s => s.Code == code);
        }

        public async Task UpdateAsync() => await context.SaveChangesAsync();

        public Task<List<StockUnit>> GetAllAsync()
        {
            return context.StockUnits.AsNoTracking().ToListAsync();
        }

        public async Task<List<StockUnit>> GetByCodesAsync(List<string> codes)
        {
            return await context.StockUnits
                .Where(su => codes.Contains(su.Code))
                .ToListAsync();
        }
    }
}