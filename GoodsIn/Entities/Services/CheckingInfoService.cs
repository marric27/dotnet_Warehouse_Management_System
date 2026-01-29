using dotnet_Warehouse_Management_System.GoodsIn.Dtos;
using dotnet_Warehouse_Management_System.GoodsIn.Entities.Mappers;
using dotnet_Warehouse_Management_System.GoodsIn.Entities.Repositories;

namespace dotnet_Warehouse_Management_System.GoodsIn.Entities.Services
{
    public class CheckingInfoService : ICheckingInfoService
    {
        private readonly ICheckingInfoRepository _checkingInfoRepository;
        public CheckingInfoService(ICheckingInfoRepository checkingInfoRepository)
        {
            _checkingInfoRepository = checkingInfoRepository;
        }
        public async Task<CheckingInfoDto> CreateAsync(CheckingInfoDto checkingInfo)
        {
            var ci = checkingInfo.ToEntity();
            ci.GenerateCode();
            var created = await _checkingInfoRepository.Create(ci);
            return created.ToResponseDto();
        }

        public async Task<CheckingInfoDto> DeleteAsync(long id)
        {
            var ci = await _checkingInfoRepository.Delete(id);
            return ci.ToResponseDto();
        }

        public async Task<CheckingInfoDto> DeleteAsync(string code)
        {
            var ci = await _checkingInfoRepository.Delete(code);
            return ci.ToResponseDto();
        }

        public async Task<List<CheckingInfoDto>> GetAllAsync()
        {
            var list = await _checkingInfoRepository.GetAll();
            return list.Select(ci => ci.ToResponseDto()).ToList();

        }

        public async Task<CheckingInfoDto> GetByCode(string code)
        {
            var ci = await _checkingInfoRepository.GetByCode(code);
            if (ci == null)
            {
                throw new KeyNotFoundException();
            }
            return ci.ToResponseDto();
        }

        public async Task<CheckingInfoDto> GetById(long id)
        {
            var ci = await _checkingInfoRepository.GetById(id);
            if (ci == null)
            {
                throw new KeyNotFoundException();
            }
            return ci.ToResponseDto();
        }

        public async Task<CheckingInfoDto> GetByStockUnitIdAsync(long stockUnitId)
        {
            var ci = await _checkingInfoRepository.GetByStockUnitIdAsync(stockUnitId);
            return ci.ToResponseDto();
        }

        public async Task<CheckingInfoDto> UpdateAsync(CheckingInfoDto checkingInfo)
        {
            var updated = await _checkingInfoRepository.Update(checkingInfo.ToEntity());
            if (updated == null)
                throw new KeyNotFoundException($"GRN {checkingInfo.Code} non trovata");

            return updated.ToResponseDto();
        }

        public async Task<CheckingInfoDto> UpdateStateAsync(CheckingInfoDto checkinginfo)
        {
            var updated = await _checkingInfoRepository.UpdateStateAsync(checkinginfo.ToEntity());
            return updated.ToResponseDto();
        }
    }
}
