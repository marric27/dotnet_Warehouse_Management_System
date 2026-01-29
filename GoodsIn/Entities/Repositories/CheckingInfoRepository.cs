
using dotnet_Warehouse_Management_System.Data;
using Microsoft.EntityFrameworkCore;

namespace dotnet_Warehouse_Management_System.GoodsIn.Entities.Repositories
{
    public class CheckingInfoRepository : ICheckingInfoRepository
    {
        private readonly ApplicationDBContext _context;
        public CheckingInfoRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<CheckingInfo> Create(CheckingInfo entity)
        {
            await _context.CheckingInfos.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<CheckingInfo> Delete(long id)
        {
            var entity = await _context.CheckingInfos.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                return null;
            }
            _context.CheckingInfos.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<CheckingInfo> Delete(string code)
        {
            var entity = await _context.CheckingInfos.FirstOrDefaultAsync(x => x.Code == code);
            if (entity == null)
            {
                return null;
            }
            _context.CheckingInfos.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<CheckingInfo>> GetAll()
        {
            return await _context.CheckingInfos.AsNoTracking().ToListAsync();
        }

        public async Task<CheckingInfo?> GetByCode(string code)
        {
            return await _context.CheckingInfos.AsNoTracking().Where(i => i.Code == code).FirstOrDefaultAsync();
        }

        public async Task<CheckingInfo> GetById(long id)
        {
            return await _context.CheckingInfos.AsNoTracking().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<CheckingInfo?> GetByStockUnitIdAsync(long stockUnitId)
        {
            return await _context.CheckingInfos.FirstOrDefaultAsync(ci => ci.StockUnitId == stockUnitId);
        }

        public async Task<CheckingInfo> Update(CheckingInfo entity)
        {
            _context.CheckingInfos.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<CheckingInfo> UpdateStateAsync(CheckingInfo checkingInfo)
        {
            var entity = await _context.CheckingInfos.FindAsync(checkingInfo.Id)
                         ?? throw new KeyNotFoundException("CheckingInfo not found");

            entity.State = checkingInfo.State;

            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
