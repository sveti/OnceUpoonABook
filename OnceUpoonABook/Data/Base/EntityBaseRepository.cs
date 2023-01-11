using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using OnceUpoonABook.Models;
using System.Linq.Expressions;

namespace OnceUpoonABook.Data.Base
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        private readonly AppDBContext appDBContext;

        public EntityBaseRepository(AppDBContext appDBContext)
        {
            this.appDBContext = appDBContext;   
        }

        public T Add(T entity)
        {
            appDBContext.Set<T>().Add(entity);
            appDBContext.SaveChanges();
            return entity;
        }

        public async Task AddAsync(T entity)
        {
            await appDBContext.Set<T>().AddAsync(entity);
            await appDBContext.SaveChangesAsync();
        }


        public void Delete(int id)
        {
            var entity = appDBContext.Set<T>().FirstOrDefault(n => n.Id == id);
            if (entity != null)
            {
                EntityEntry entityEntry = appDBContext.Entry<T>(entity);
                entityEntry.State = EntityState.Deleted;

                appDBContext.SaveChanges();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await appDBContext.Set<T>().FirstOrDefaultAsync(n => n.Id == id);
            if (entity != null)
            {
                EntityEntry entityEntry = appDBContext.Entry<T>(entity);
                entityEntry.State = EntityState.Deleted;

                await appDBContext.SaveChangesAsync();
            }
        }

        public IEnumerable<T> GetAll()
        {
            return appDBContext.Set<T>().ToList();
        }

        public IEnumerable<T> GetAll(params Expression<Func<T, object>>[] expressions)
        {
            IQueryable<T> query = appDBContext.Set<T>();
            query = expressions.Aggregate(query, (item, expression) => item.Include(expression));
            return query.ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var result = await appDBContext.Set<T>().ToListAsync();
            return result;
        }

        public T GetById(int id)
        {
            return appDBContext.Set<T>().FirstOrDefault(item => item.Id == id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var result = await appDBContext.Set<T>().FirstOrDefaultAsync(item => item.Id == id);
            return result;
        }

        public T Update(int id, T entity)
        {
            appDBContext.Entry(entity).State = EntityState.Modified;
            appDBContext.Set<T>().Update(entity);
            appDBContext.SaveChanges();
            return entity;
        }

        public async Task UpdateAsync(int id, T entity)
        {
            EntityEntry entityEntry = appDBContext.Entry(entity);
            entityEntry.State = EntityState.Modified;
            await appDBContext.SaveChangesAsync();
        }
    }
}
