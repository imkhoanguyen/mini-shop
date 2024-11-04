using System.Linq.Expressions;

namespace Shop.Application.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T entity);
        IQueryable<T> Query();
        Task<T?> GetAsync(Expression<Func<T, bool>> expression, bool tracked = false);
        void Remove(T entity);
        void Update(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void DeleteRange(IEnumerable<T> entities);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
    }
}
