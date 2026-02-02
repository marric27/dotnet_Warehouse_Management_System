using dotnet_Warehouse_Management_System.BaseRepository;
using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Data;
using dotnet_Warehouse_Management_System.Outbound.Dtos;
using dotnet_Warehouse_Management_System.Products.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace dotnet_Warehouse_Management_System.Outbound.Entities.Repositories
{
    public class OrderRepository(ApplicationDBContext context) : BaseRepository<Order>(context), IOrderRepository
    {
        public async Task<Page<Order>> GetAllPaginatedAsync(QueryObject query)
        {
            return await _context.Orders
                .AsNoTracking()
                .Where(p => string.IsNullOrEmpty(query.Code) || p.Code.Contains(query.Code))
                .OrderBy(p => p.Id)
                .ToPagedListAsync(query.PageNumber, query.PageSize);
        }

        public async Task<Order?> GetByCodeAsync(string code, bool track)
        {
            var query = context.Orders.AsQueryable();
            if (!track) query = query.AsNoTracking();
            return await query.Include(o => o.SalesOrderLineList).FirstOrDefaultAsync(p => p.Code == code);
        }

        public async Task<List<Order>> GetByStateAndIds(OrderState state, List<long> ids)
        {
            return await context.Orders
                .Include(o => o.SalesOrderLineList)
                .Where(o =>
                    (o.State == state) &&
                    (ids == null || ids.Contains(o.Id))).ToListAsync();
        }
    }
}
