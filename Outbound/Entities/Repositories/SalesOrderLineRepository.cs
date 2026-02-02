using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Data;
using dotnet_Warehouse_Management_System.Outbound.Dtos;
using dotnet_Warehouse_Management_System.Products.Entities;
using Microsoft.EntityFrameworkCore;

namespace dotnet_Warehouse_Management_System.Outbound.Entities.Repositories
{
    public class SalesOrderLineRepository(ApplicationDBContext context) : ISalesOrderLineRepository
    {
        public async Task<SalesOrderLine> CreateAsync(SalesOrderLine salesOrderLine)
        {
            await context.SalesOrderLines.AddAsync(salesOrderLine);
            await context.SaveChangesAsync();
            return salesOrderLine;
        }

        public Task DeleteAsync(SalesOrderLine salesOrderLine)
        {
            throw new NotImplementedException();
        }

        public Task<Page<SalesOrderLine>> GetAllAsync(QueryObject query)
        {
            throw new NotImplementedException();
        }

        public async Task<SalesOrderLine?> GetByCodeAsync(string code)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
