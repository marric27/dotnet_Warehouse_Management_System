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
    public class StockUnitRepository : IStockUnitRepository
    {
        private readonly ApplicationDBContext _context;
        public StockUnitRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<StockUnit> CreateAsync(StockUnit stockUnit)
        {
            await _context.StockUnits.AddAsync(stockUnit);
            await _context.SaveChangesAsync();
            return stockUnit;
        }

        public async Task<StockUnit?> DeleteAsync(string code)
        {
            var stockUnit = await GetByCodeAsync(code);
            if (stockUnit == null)
            {
                return null;
            }
            _context.StockUnits.Remove(stockUnit);
            await _context.SaveChangesAsync();
            return stockUnit;
        }

        public async Task<StockUnit> GetById(long id)
        {
            return await _context.StockUnits
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<StockUnit?> GetByCodeAsync(string code)
        {
            return await _context.StockUnits.AsNoTracking().Where(s => s.Code == code).FirstOrDefaultAsync();
        }

        public async Task<StockUnit> UpdateAsync(StockUnit stockUnit)
        {
            _context.StockUnits.Update(stockUnit);
            await _context.SaveChangesAsync();
            return stockUnit;
        }

        public Task<List<StockUnit>> GetAllAsync()
        {
            return _context.StockUnits.AsNoTracking().ToListAsync();
        }
    }
}