namespace dotnet_Warehouse_Management_System.GoodsIn.Entities.Repositories
{
    public interface ICheckingInfoRepository
    {
        Task<List<CheckingInfo>> GetAll();
        Task<CheckingInfo?> GetById(long id);
        Task<CheckingInfo?> GetByCode(string code);
        Task<CheckingInfo> Create(CheckingInfo entity);
        Task<CheckingInfo> Update(CheckingInfo entity);
        Task<CheckingInfo> Delete(long id);
        Task<CheckingInfo> Delete(string code);


    }
}
