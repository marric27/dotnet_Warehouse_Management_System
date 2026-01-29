using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Data;
using dotnet_Warehouse_Management_System.Products.Entities.Dtos;
using Microsoft.EntityFrameworkCore;
using System;

namespace dotnet_Warehouse_Management_System.Products.Entities.Repository
{
    public class ProductRepository(ApplicationDBContext context) : IProductRepository
    {
        public async Task<Page<Product>> GetAllPaginatedAsync(QueryObject query)
        {
            int pageNumber = Math.Max(0, query.PageNumber);
            int pageSize = Math.Clamp(query.PageSize, 1, 100);

            var prods = context.Products.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Code))
            {
                prods = prods.Where(p => p.Code.Contains(query.Code));
            }
            prods = query.SortBy?.ToLower() switch
            {
                "code" => query.IsDescending
                    ? prods.OrderByDescending(g => g.Code)
                    : prods.OrderBy(g => g.Code),

                _ => query.IsDescending
                    ? prods.OrderByDescending(g => g.Id)
                    : prods.OrderBy(g => g.Id)
            };

            var totalItems = await prods.CountAsync();

            var items = await prods
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new Page<Product>
            {
                Content = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalElements = totalItems
            };
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await context.Products.AsNoTracking().ToListAsync();
        }

        public async Task<Product> CreateAsync(Product product)
        {
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> GetByCodeAsync(string code)
        {
            return await context.Products.AsNoTracking().AsQueryable().FirstOrDefaultAsync(p => p.Code == code);
        }

        public async Task UpdateAsync() => await context.SaveChangesAsync();

        public async Task DeleteAsync(string code)
        {
            var product = await GetByCodeAsync(code);
            context.Products.Remove(product);
            await context.SaveChangesAsync();
        }
    }
}
