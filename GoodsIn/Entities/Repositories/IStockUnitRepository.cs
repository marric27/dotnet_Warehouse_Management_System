using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_Warehouse_Management_System.GoodsIn;


namespace dotnet_Warehouse_Management_System.GoodsIn.Entities.Repositories
{
    public interface IStockUnitRepository
    {
        Task<StockUnit> GetById(long id);
        Task<StockUnit?> GetByCodeAsync(string code);
        Task<StockUnit> CreateAsync(StockUnit stockUnit);
        Task<StockUnit> UpdateAsync(string code, StockUnit stockUnit);
        Task<StockUnit?> DeleteAsync(string code);
    }
}