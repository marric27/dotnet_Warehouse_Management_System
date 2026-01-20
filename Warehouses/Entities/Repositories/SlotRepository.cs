using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Data;
using dotnet_Warehouse_Management_System.Warehouses.Entities.Dtos;
using Microsoft.EntityFrameworkCore;

namespace dotnet_Warehouse_Management_System.Warehouses.Entities.Repositories
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

        public async Task<Page<Slot>> GetAllAsync(QueryObject query)
        {
            int pageNumber = Math.Max(0, query.PageNumber);
            int pageSize = Math.Clamp(query.PageSize, 1, 100);

            var slots = _context.Slots.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Code))
            {
                slots = slots.Where(s => s.Code.Contains(query.Code));
            }
            slots = query.SortBy?.ToLower() switch
            {
                "code" => query.IsDescending
                    ? slots.OrderByDescending(g => g.Code)
                    : slots.OrderBy(g => g.Code),

                _ => query.IsDescending
                    ? slots.OrderByDescending(g => g.Id)
                    : slots.OrderBy(g => g.Id)
            };

            var totalItems = await slots.CountAsync();

            var items = await slots
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new Page<Slot>
            {
                Content = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalElements = totalItems
            };
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
