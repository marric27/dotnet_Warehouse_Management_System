using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Data;
using dotnet_Warehouse_Management_System.Outbound.Dtos;
using dotnet_Warehouse_Management_System.Products.Entities;
using Microsoft.EntityFrameworkCore;

namespace dotnet_Warehouse_Management_System.Outbound.Entities.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDBContext _context;
        public OrderRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Order> CreateAsync(Order Order)
        {
            await _context.Orders.AddAsync(Order);
            await _context.SaveChangesAsync();
            return Order;
        }

        public async Task<Order?> DeleteAsync(string code)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.Code == code);
            if (order != null)
            {
                return null;
            }
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _context.Orders.AsNoTracking().ToListAsync();
        }

        public async Task<Page<Order>> GetAllPaginatedAsync(QueryObject query)
        {
            int pageNumber = Math.Max(0, query.PageNumber);
            int pageSize = Math.Clamp(query.PageSize, 1, 100);

            var orders = _context.Orders.AsNoTracking().AsQueryable();

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

        public async Task<Order?> GetByCodeAsync(string code)
        {
            return await _context.Orders.Include(o => o.SalesOrderLineList).AsNoTracking()
                .Where(o => o.Code == code).FirstOrDefaultAsync();
        }

        public async Task<List<Order>> GetByStateAndIds(OrderState state, List<long> ids)
        {
            return await _context.Orders
                .Include(o => o.SalesOrderLineList)
                .Where(o =>
                    (o.State == state) &&
                    (ids == null || ids.Contains(o.Id))).ToListAsync();
        }

        public Task<Order> UpdateAsync(string code, OrderRequestDto orderDto)
        {
            throw new NotImplementedException();
        }

        public async Task<Order> UpdateStateAsync(string code, OrderState state)
        {
            var existing = await _context.Orders.FirstOrDefaultAsync(o => o.Code == code);
            if (existing == null)
            {
                return null;
            }

            existing.State = state;
            await _context.SaveChangesAsync();
            return existing;
        }
    }
}
