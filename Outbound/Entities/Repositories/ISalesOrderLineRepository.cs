using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Outbound.Dtos;
using dotnet_Warehouse_Management_System.Products.Entities;
using dotnet_Warehouse_Management_System.Products.Entities.Dtos;

namespace dotnet_Warehouse_Management_System.Outbound.Entities.Repositories
{
    public interface ISalesOrderLineRepository
    {
        Task<Page<SalesOrderLine>> GetAllAsync(QueryObject query);
        Task<SalesOrderLine?> GetByCodeAsync(string code);
        Task<SalesOrderLine> CreateAsync(SalesOrderLine salesOrderLine);
        Task UpdateAsync();
        Task DeleteAsync(SalesOrderLine salesOrderLine);
    }
}
