
namespace TravelTracker.Core.Abstractions
{
    public interface IRepositoryBase<T> where T : class
    {
        Task CreateAsync(T entity);
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task DeleteAsync(T entity);
        Task UpdateAsync(T entity);
    }
}