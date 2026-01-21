using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Data;
using dotnet_Warehouse_Management_System.Products.Entities.Dtos;
using Microsoft.EntityFrameworkCore;
using System;

namespace dotnet_Warehouse_Management_System.Products.Entities.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDBContext _context;
        public ProductRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Page<Product>> GetAllPaginatedAsync(QueryObject query)
        {
            int pageNumber = Math.Max(0, query.PageNumber);
            int pageSize = Math.Clamp(query.PageSize, 1, 100);

            var prods = _context.Products.AsNoTracking().AsQueryable();

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
            return await _context.Products.AsNoTracking().ToListAsync();
        }

        public async Task<Product?> GetByCodeAsync(string code)
        {
            return await _context.Products.AsNoTracking()
                .Where(p => p.Code == code).FirstOrDefaultAsync();
        }

        public async Task<Product> CreateAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateAsync(string code, ProductRequestDto productDto)
        {
            var existing = await _context.Products.FirstOrDefaultAsync(p => p.Code == code);
            if (existing != null)
            {
                return null;
            }

            existing.Name = productDto.Name;
            existing.Category = productDto.Category;
            await _context.SaveChangesAsync();
            return existing;

        }
        public async Task<Product?> DeleteAsync(string code)
        {
            var prod = await _context.Products.FirstOrDefaultAsync(x => x.Code == code);
            if (prod != null)
            {
                return null;
            }
            _context.Products.Remove(prod);
            await _context.SaveChangesAsync();
            return prod;
        }
    }
}
