using dotnet_Warehouse_Management_System.GoodsIn.Dtos;
using dotnet_Warehouse_Management_System.GoodsIn.Entities.Mappers;
using dotnet_Warehouse_Management_System.GoodsIn.Entities.Repositories;
using dotnet_Warehouse_Management_System.Products.Entities.Repository;

namespace dotnet_Warehouse_Management_System.GoodsIn.Entities.Services
{
    public class CheckingInfoService(ICheckingInfoRepository checkingInfoRepository) : ICheckingInfoService
    {
        public async Task<CheckingInfoDto> CreateAsync(CheckingInfoDto checkingInfo)
        {
            var ci = checkingInfo.ToEntity();
            ci.GenerateCode();
            var created = await checkingInfoRepository.CreateAsync(ci);
            return created.ToResponseDto();
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var ci = await checkingInfoRepository.GetByIdAsync(id, true);
            if (ci == null) return false;

            await checkingInfoRepository.DeleteAsync(ci);
            return true;
        }

        public async Task<bool> DeleteAsync(string code)
        {
            var ci = await checkingInfoRepository.GetByCodeAsync(code, true);
            if (ci == null) return false;

            await checkingInfoRepository.DeleteAsync(ci);
            return true;
        }

        public async Task<List<CheckingInfoDto>> GetAllAsync()
        {
            var list = await checkingInfoRepository.GetAllAsync();
            return list.Select(ci => ci.ToResponseDto()).ToList();

        }

        public async Task<CheckingInfoDto> GetByCode(string code)
        {
            var ci = await checkingInfoRepository.GetByCodeAsync(code, false);
            if (ci == null)
            {
                throw new KeyNotFoundException();
            }
            return ci.ToResponseDto();
        }

        public async Task<CheckingInfoDto> GetById(long id)
        {
            var ci = await checkingInfoRepository.GetByIdAsync(id, false);
            if (ci == null)
            {
                throw new KeyNotFoundException();
            }
            return ci.ToResponseDto();
        }

        public async Task<CheckingInfoDto> GetByStockUnitIdAsync(long stockUnitId)
        {
            var ci = await checkingInfoRepository.GetByStockUnitIdAsync(stockUnitId);
            return ci.ToResponseDto();
        }

        public async Task<CheckingInfoDto> UpdateAsync(CheckingInfoDto checkingInfo)
        {
            var existingCi = await checkingInfoRepository.GetByCodeAsync(checkingInfo.Code, true) ?? throw new KeyNotFoundException();
            existingCi.State = checkingInfo.State;
            await checkingInfoRepository.UpdateAsync();
            return existingCi.ToResponseDto();
        }

        public async Task<CheckingInfoDto> UpdateStateAsync(CheckingInfoDto checkinginfo)
        {
            var updated = await checkingInfoRepository.UpdateStateAsync(checkinginfo.ToEntity());
            return updated.ToResponseDto();
        }
    }
}
