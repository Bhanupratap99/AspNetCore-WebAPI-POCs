namespace EFCore_Relationships.DAL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        // Commands
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);

        // Queries
        Task<T?> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
    }
}
