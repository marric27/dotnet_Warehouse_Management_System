

using dotnet_Warehouse_Management_System.Data;

namespace dotnet_Warehouse_Management_System.Picking.Entities.Repository
{
    public class PickingInfoRepository : IPickingInfoRepository
    {
        private readonly ApplicationDBContext _context;
        public PickingInfoRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<PickingInfo> CreateAsync(PickingInfo pickingInfo)
        {
            await _context.PickingInfos.AddAsync(pickingInfo);
            await _context.SaveChangesAsync();
            return pickingInfo;
        }
    }
}
