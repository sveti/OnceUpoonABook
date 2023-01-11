using OnceUpoonABook.Models;
using System.Linq.Expressions;

namespace OnceUpoonABook.Data.Base
{
    public interface IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(params Expression<Func<T, object>>[] expressions);
        T GetById(int id);
        T Add(T entity);
        T Update(int id, T entity);
        void Delete(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(int id, T entity);
        Task DeleteAsync(int id);

    }
}
