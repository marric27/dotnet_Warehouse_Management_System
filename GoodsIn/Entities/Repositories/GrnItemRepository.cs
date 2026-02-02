using dotnet_Warehouse_Management_System.BaseRepository;
using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Data;
using dotnet_Warehouse_Management_System.GoodsIn.Dtos;
using dotnet_Warehouse_Management_System.GoodsIn.Entities.Mappers;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace dotnet_Warehouse_Management_System.GoodsIn.Entities.Repositories
{
    public class GrnItemRepository(ApplicationDBContext context) : BaseRepository<GrnItem>(context), IGrnItemRepository
    {
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
    }
}
