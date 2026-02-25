using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Customers.Entities.Dtos;
using dotnet_Warehouse_Management_System.Data;
using Microsoft.EntityFrameworkCore;

namespace dotnet_Warehouse_Management_System.Customers.Entities.Repository
{
    public class CustomerRepository(ApplicationDBContext context) : ICustomerRepository
    {
        public async Task<Customer> CreateAsync(Customer Customer)
        {
            await context.Customers.AddAsync(Customer);
            await context.SaveChangesAsync();
            return Customer;
        }

        public async Task<Customer?> DeleteAsync(string code)
        {
            var cust = await context.Customers.FirstOrDefaultAsync(x => x.Code == code);
            if (cust == null)
            {
                return null;
            }
            context.Customers.Remove(cust);
            await context.SaveChangesAsync();
            return cust;
        }

        public async Task<Page<Customer>> GetAllPaginatedAsync(QueryObject query)
        {
            int pageNumber = Math.Max(0, query.PageNumber);
            int pageSize = Math.Clamp(query.PageSize, 1, 100);

            var customers = context.Customers.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Code))
            {
                customers = customers.Where(p => p.Code.Contains(query.Code));
            }
            customers = query.SortBy?.ToLower() switch
            {
                "code" => query.IsDescending
                    ? customers.OrderByDescending(g => g.Code)
                    : customers.OrderBy(g => g.Code),

                _ => query.IsDescending
                    ? customers.OrderByDescending(g => g.Id)
                    : customers.OrderBy(g => g.Id)
            };

            var totalItems = await customers.CountAsync();

            var items = await customers
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new Page<Customer>
            {
                Content = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalElements = totalItems
            };
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await context.Customers.AsNoTracking().ToListAsync();
        }

        public async Task<Customer?> GetByCodeAsync(string code)
        {
            return await context.Customers.AsNoTracking()
                .Where(p => p.Code == code).FirstOrDefaultAsync();
        }

        public Task<Customer> UpdateAsync(string code, CustomerRequestDto CustomerDto)
        {
            throw new NotImplementedException();
        }
    }
}
