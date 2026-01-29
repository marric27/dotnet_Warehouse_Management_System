using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_Warehouse_Management_System.GoodsIn;


namespace dotnet_Warehouse_Management_System.GoodsIn.Entities.Repositories
{
    public interface IStockUnitRepository
    {
        Task<StockUnit> GetByIdAsync(long id, bool track);
        Task<StockUnit?> GetByCodeAsync(string code, bool track);
        Task<StockUnit> CreateAsync(StockUnit stockUnit);
        Task UpdateAsync();
        Task DeleteAsync(StockUnit stockUnit);
        Task<List<StockUnit>> GetAllAsync();
        Task<List<StockUnit>> GetByCodesAsync(List<string> codes);
    }
}