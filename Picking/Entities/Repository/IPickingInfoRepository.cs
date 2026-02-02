namespace dotnet_Warehouse_Management_System.Picking.Entities.Repository
{
    public interface IPickingInfoRepository
    {
        Task<PickingInfo> CreateAsync(PickingInfo pickingInfo);
    }
}
