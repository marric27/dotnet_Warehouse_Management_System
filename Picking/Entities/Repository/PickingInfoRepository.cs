

using dotnet_Warehouse_Management_System.Data;

namespace dotnet_Warehouse_Management_System.Picking.Entities.Repository
{
    public class PickingInfoRepository(ApplicationDBContext context) : IPickingInfoRepository
    {
        public async Task<PickingInfo> CreateAsync(PickingInfo pickingInfo)
        {
            await context.PickingInfos.AddAsync(pickingInfo);
            await context.SaveChangesAsync();
            return pickingInfo;
        }
    }
}
