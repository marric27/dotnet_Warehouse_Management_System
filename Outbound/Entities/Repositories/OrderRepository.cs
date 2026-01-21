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

        public Task<Page<Order>> GetAllAsync(QueryObject query)
        {
            throw new NotImplementedException();
        }

        public Task<Order?> GetByCodeAsync(string code)
        {
            throw new NotImplementedException();
        }

        public Task<Order> UpdateAsync(string code, OrderRequestDto orderDto)
        {
            throw new NotImplementedException();
        }
    }
}
