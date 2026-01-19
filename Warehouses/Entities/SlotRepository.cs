using dotnet_Warehouse_Management_System.Data;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Warehouses.Entities.Dtos;
using Microsoft.EntityFrameworkCore;

namespace dotnet_Warehouse_Management_System.Warehouses.Entities
{
    public class SlotRepository : ISlotRepository
    {
        private readonly ApplicationDBContext _context;
        public SlotRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Slot> CreateAsync(Slot slot)
        {
            slot.GenerateCode();
            await _context.Slots.AddAsync(slot);
            await _context.SaveChangesAsync();
            return slot;
        }

        public async Task<Slot?> DeleteAsync(string code)
        {
            var slot = await _context.Slots.FirstOrDefaultAsync(x => x.Code == code);
            if (slot == null)
            {
                return null;
            }
            _context.Slots.Remove(slot);
            await _context.SaveChangesAsync();
            return slot;

        }

        public async Task<List<Slot>> GetAllAsync(QueryObject query)
        {
            var slots = _context.Slots.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Code))
            {
                slots = slots.Where(s => s.Code.Contains(query.Code));
            }
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Code", StringComparison.OrdinalIgnoreCase))
                {
                    slots = query.IsDescending
                        ? slots.OrderByDescending(s => s.Code)
                        : slots.OrderBy(s => s.Code);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await slots
                .Skip(skipNumber)
                .Take(query.PageSize)
                .ToListAsync();
        }

        public async Task<Slot?> GetByCodeAsync(string code)
        {
            return await _context.Slots.AsNoTracking().Where(s => s.Code == code).FirstOrDefaultAsync();
        }

        public Task<Slot> UpdateAsync(string code, SlotRequestDto productDto)
        {
            throw new NotImplementedException();
        }
    }
}
