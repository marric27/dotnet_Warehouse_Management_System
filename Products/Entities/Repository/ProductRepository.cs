using dotnet_Warehouse_Management_System.BaseRepository;
using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Data;
using Microsoft.EntityFrameworkCore;

namespace dotnet_Warehouse_Management_System.Products.Entities.Repository
{
    public class ProductRepository(ApplicationDBContext context) : BaseRepository<Product>(context), IProductRepository
    {
        public async Task<Page<Product>> GetAllPaginatedAsync(QueryObject query)
        {
            return await context.Products
                .AsNoTracking()
                .Where(p => string.IsNullOrEmpty(query.Code) || p.Code.Contains(query.Code))
                .OrderBy(p => p.Id)
                .ToPagedListAsync(query.PageNumber, query.PageSize);
        }

        public async Task<Product?> GetByCodeAsync(string code, bool track = false)
        {
            var query = context.Products.AsQueryable();
            if (!track) query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(p => p.Code == code);
        }
    }
}
