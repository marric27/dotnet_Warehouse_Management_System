using Microsoft.EntityFrameworkCore;

namespace dotnet_Warehouse_Management_System.Common
{
    public static class QueryableExtensions
    {
        public static async Task<Page<T>> ToPagedListAsync<T>(this IQueryable<T> source, int pageNumber, int pageSize)
        {
            pageNumber = Math.Max(0, pageNumber);
            pageSize = Math.Clamp(pageSize, 1, 100);

            var count = await source.CountAsync();
            var items = await source.Skip(pageNumber * pageSize).Take(pageSize).ToListAsync();

            return new Page<T>
            {
                Content = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalElements = count
            };
        }
    }
}
