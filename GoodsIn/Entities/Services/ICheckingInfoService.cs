using dotnet_Warehouse_Management_System.GoodsIn.Dtos;

namespace dotnet_Warehouse_Management_System.GoodsIn.Entities.Services
{
    public interface ICheckingInfoService
    {
        Task<CheckingInfoDto> CreateAsync(CheckingInfoDto checkingInfo);
        Task<List<CheckingInfoDto>> GetAllAsync();
        Task<CheckingInfoDto> GetByCode(string code);
        Task<CheckingInfoDto> GetById(long id);

        Task<CheckingInfoDto> UpdateAsync(CheckingInfoDto checkingInfo);
        Task<CheckingInfoDto> DeleteAsync(long id);
        Task<CheckingInfoDto> DeleteAsync(string code);

    }
}
