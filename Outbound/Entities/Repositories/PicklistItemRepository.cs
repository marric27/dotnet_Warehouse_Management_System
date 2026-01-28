
using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Data;
using dotnet_Warehouse_Management_System.Outbound.Dtos;
using dotnet_Warehouse_Management_System.Outbound.Entities.Mappers;
using Microsoft.EntityFrameworkCore;

namespace dotnet_Warehouse_Management_System.Outbound.Entities.Repositories
{
    public class PicklistItemRepository : IPicklistItemRepository
    {
        private readonly ApplicationDBContext _context;
        public PicklistItemRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<PicklistItem>> GetAllAsync()
        {
            return await _context.PicklistItems.AsNoTracking().ToListAsync();
        }

        public async Task<PicklistItem?> FindItemsByStateOrdered(List<long> plIds, PicklistItemState state)
        {
            return await _context.PicklistItems
                .Where(pli => plIds.Contains(pli.PicklistId) && pli.State == PicklistItemState.OPEN)
                .OrderBy(pli => pli.PickingSequence)
                .ThenBy(pli => pli.SlotCode)
                .FirstOrDefaultAsync();
        }

        public async Task<PicklistItem> UpdateAsync(string code, PicklistItemDto item)
        {
            var existing = await _context.PicklistItems.FirstOrDefaultAsync(i => i.code == code);
            if (existing == null)
            {
                return null;
            }
            existing.State = item.State;
            existing.ErrorReason = item.ErrorReason;
            existing.qty = item.Quantity;
            existing.PickedQty = item.PickedQty;
            await _context.SaveChangesAsync();
            return existing;
        }
    }
}
