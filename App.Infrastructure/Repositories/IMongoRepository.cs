using App.Core.Domain.Entities;
using System.Linq.Expressions;

namespace App.Infrastructure.Repositories
{
    public interface IMongoRepository<T> where T : class
    {
        Task CreateAsync(T entity);
        Task DeleteAsync(string id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(string id);
        Task UpdateAsync(string id, T entity);
        Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> filterExpression);

    }
}