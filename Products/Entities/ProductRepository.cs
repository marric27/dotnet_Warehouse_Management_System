using dotnet_Warehouse_Management_System.Data;
using dotnet_Warehouse_Management_System.Products.Entities.Dtos;
using dotnet_Warehouse_Management_System.Products.Helpers;
using Microsoft.EntityFrameworkCore;
using System;

namespace dotnet_Warehouse_Management_System.Products.Entities
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDBContext _context;
        public ProductRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync(QueryObject query)
        {
            var prods = _context.Products.AsNoTracking().AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                prods = prods.Where(p => p.Name.Contains(query.Name));
            }
            if (query.Category.HasValue)
            {
                prods = prods.Where(p => p.Category == query.Category);
            }
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    prods = query.IsDescending ? prods.OrderByDescending(s => s.Name) : prods.OrderBy(s => s.Name);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return await prods.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Product?> GetByCodeAsync(string code)
        {
            return await _context.Products.AsNoTracking()
                .Where(p => p.Code == code).FirstOrDefaultAsync();
        }

        public async Task<Product> CreateAsync(Product product)
        {
            product.GenerateCode();
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
