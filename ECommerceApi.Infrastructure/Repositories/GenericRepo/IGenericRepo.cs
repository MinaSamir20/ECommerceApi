using System.Linq.Expressions;

namespace ECommerceApi.Infrastructure.Repositories.GenericRepo
{
    public interface IGenericRepo<T> where T : class
    {
        Task<ICollection<T>> GetAllAsync(string[]? includes = null);
        Task<T> GetByIdAsync(int id, string[]? includes = null);
        Task<ICollection<T>> GetByCriteriaAsync(Expression<Func<T, bool>> criteria, string[]? includes = null);
        Task<T> UpdateAsync(T entity);
        Task<string> CreateAsync(T entity);
        Task<string> DeleteAsync(int id);
    }
}
