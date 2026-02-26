using dotnet_Warehouse_Management_System.Data;
using Microsoft.EntityFrameworkCore;

namespace dotnet_Warehouse_Management_System.BaseRepository
{
    public class BaseRepository<T>(ApplicationDBContext context) : IBaseRepository<T> where T : class
    {

        public virtual async Task<List<T>> GetAllAsync() =>
            await context.Set<T>().AsNoTracking().ToListAsync();

        public async Task<T?> GetByIdAsync(int id) =>
            await context.Set<T>().FindAsync(id);

        public async Task<T> CreateAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<T>> CreateRangeAsync(List<T> entities)
        {
            await context.Set<T>().AddRangeAsync(entities);
            await context.SaveChangesAsync();
            return entities;
        }

        public async Task UpdateAsync() => await context.SaveChangesAsync();

        public async Task DeleteAsync(T entity)
        {
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
        }

    }
}
