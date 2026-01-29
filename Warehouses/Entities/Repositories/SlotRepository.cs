using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Data;
using dotnet_Warehouse_Management_System.Warehouses.Entities.Dtos;
using Microsoft.EntityFrameworkCore;

namespace dotnet_Warehouse_Management_System.Warehouses.Entities.Repositories
{
    public class SlotRepository(ApplicationDBContext context) : ISlotRepository
    {
        public async Task<Slot> CreateAsync(Slot slot)
        {
            await context.Slots.AddAsync(slot);
            await context.SaveChangesAsync();
            return slot;
        }

        public async Task DeleteAsync(string code)
        {
            var slot = await GetByCodeAsync(code);
            context.Slots.Remove(slot);
            await context.SaveChangesAsync();

        }

        public async Task<Page<Slot>> GetAllAsync(QueryObject query)
        {
            int pageNumber = Math.Max(0, query.PageNumber);
            int pageSize = Math.Clamp(query.PageSize, 1, 100);

            var slots = context.Slots.AsNoTracking().AsQueryable();

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
        public async Task<List<Slot>> GetAllAsync()
        {
            return await context.Slots.AsNoTracking().ToListAsync();
        }

        public async Task<Slot?> GetByCodeAsync(string code)
        {
            return await context.Slots.AsNoTracking().AsQueryable().FirstOrDefaultAsync(s => s.Code == code);
        }

        public async Task<Slot?> GetSlotContainingProduct(string productCode)
        {
            return await context.Slots.AsNoTracking().Where(s => s.Product.Code == productCode).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync() => await context.SaveChangesAsync();

    }
}
