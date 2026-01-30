using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Data;
using dotnet_Warehouse_Management_System.Outbound.Dtos;
using dotnet_Warehouse_Management_System.Products.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace dotnet_Warehouse_Management_System.Outbound.Entities.Repositories
{
    public class OrderRepository(ApplicationDBContext context) : IOrderRepository
    {
        public async Task<Order> CreateAsync(Order Order)
        {
            await context.Orders.AddAsync(Order);
            await context.SaveChangesAsync();
            return Order;
        }

        public async Task DeleteAsync(Order order)
        {
            context.Orders.Remove(order);
            await context.SaveChangesAsync();
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await context.Orders.AsNoTracking().ToListAsync();
        }

        public async Task<Page<Order>> GetAllPaginatedAsync(QueryObject query)
        {
            int pageNumber = Math.Max(0, query.PageNumber);
            int pageSize = Math.Clamp(query.PageSize, 1, 100);

            var orders = context.Orders.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Code))
            {
                orders = orders.Where(o => o.Code.Contains(query.Code));
            }
            orders = query.SortBy?.ToLower() switch
            {
                "code" => query.IsDescending
                    ? orders.OrderByDescending(g => g.Code)
                    : orders.OrderBy(g => g.Code),

                _ => query.IsDescending
                    ? orders.OrderByDescending(g => g.Id)
                    : orders.OrderBy(g => g.Id)
            };

            var totalItems = await orders.CountAsync();

            var items = await orders
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new Page<Order>
            {
                Content = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalElements = totalItems
            };
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

        public async Task UpdateAsync() => await context.SaveChangesAsync();

    }
}
