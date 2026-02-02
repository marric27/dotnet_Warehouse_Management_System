using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Data;
using Microsoft.EntityFrameworkCore;

namespace dotnet_Warehouse_Management_System.Outbound.Entities.Repositories
{
    public class PicklistItemRepository(ApplicationDBContext context) : IPicklistItemRepository
    {
        public async Task<List<PicklistItem>> GetAllAsync()
        {
            return await context.PicklistItems.AsNoTracking().ToListAsync();
        }

        public async Task<PicklistItem?> FindItemsByStateOrdered(List<long> plIds, PicklistItemState state)
        {
            return await context.PicklistItems
                .Where(pli => plIds.Contains(pli.PicklistId) && pli.State == PicklistItemState.OPEN)
                .OrderBy(pli => pli.PickingSequence)
                .ThenBy(pli => pli.SlotCode)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateAsync() => await context.SaveChangesAsync();

        public async Task<PicklistItem?> GetByCodeAsync(string code, bool track = false)
        {
            var query = context.PicklistItems.AsQueryable();
            if (!track) query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(p => p.code == code);
        }
    }
}
