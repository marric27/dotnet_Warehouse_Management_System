using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Data;
using dotnet_Warehouse_Management_System.GoodsIn.Dtos;
using dotnet_Warehouse_Management_System.Products.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
            grn.GenerateCode();
            grn.State = Common.State.OPEN;
            await _context.Grns.AddAsync(grn);
            await _context.SaveChangesAsync();
            return grn;
        }

        public async Task<Grn?> DeleteAsync(string code)
        {
            var grn = await _context.Grns.FirstOrDefaultAsync(x => x.Code == code);
            if (grn != null)
            {
                return null;
            }
            _context.Grns.Remove(grn);
            await _context.SaveChangesAsync();
            return grn;
        }

        public async Task<List<Grn>> GetAllAsync(QueryObject query)
        {
            var grns = _context.Grns.AsNoTracking().AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Code))
            {
                grns = grns.Where(p => p.Code.Contains(query.Code));
            }
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Code", StringComparison.OrdinalIgnoreCase))
                {
                    grns = query.IsDescending ? grns.OrderByDescending(s => s.Code) : grns.OrderBy(s => s.Code);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return await grns.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Grn?> GetByCodeAsync(string code)
        {
            return await _context.Grns.AsNoTracking().Where(g => g.Code == code).FirstOrDefaultAsync();
        }

        Task<Grn> IGrnRepository.UpdateAsync(string code, GrnRequestDto grnDto)
        {
            throw new NotImplementedException();
        }
    }
}
