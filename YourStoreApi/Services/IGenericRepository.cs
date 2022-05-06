using YourStoreApi.Models;

namespace YourStoreApi.Services
{
    public interface IGenericRepository<T> where T :BaseEntity
    {
        
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> GetEntityWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
        Task<int> CountAsync(ISpecification<T> spec);
    }
}
