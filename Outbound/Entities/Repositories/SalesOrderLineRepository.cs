using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Data;
using dotnet_Warehouse_Management_System.Outbound.Dtos;
using dotnet_Warehouse_Management_System.Products.Entities;
using Microsoft.EntityFrameworkCore;

namespace dotnet_Warehouse_Management_System.Outbound.Entities.Repositories
{
    public class SalesOrderLineRepository : ISalesOrderLineRepository
    {
        private readonly ApplicationDBContext _context;
        public SalesOrderLineRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<SalesOrderLine> CreateAsync(SalesOrderLine salesOrderLine)
        {
            await _context.SalesOrderLines.AddAsync(salesOrderLine);
            await _context.SaveChangesAsync();
            return salesOrderLine;
        }

        public Task<SalesOrderLine?> DeleteAsync(string code)
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

        public Task<SalesOrderLine> UpdateAsync(string code, SalesOrderLineRequestDto salesOrderLineRequestDto)
        {
            throw new NotImplementedException();
        }
    }
}
