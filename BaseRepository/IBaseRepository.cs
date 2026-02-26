namespace dotnet_Warehouse_Management_System.BaseRepository
{
    public interface IBaseRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T> CreateAsync(T entity);
        Task<List<T>> CreateRangeAsync(List<T> entities);
        Task UpdateAsync();
        Task DeleteAsync(T entity);
    }
}
