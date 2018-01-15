using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WordReminder.Data;

namespace WordReminder.Biz.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext dbContext;
        private readonly DbSet<T> dbSet;

        public Repository(WordReminderContext context)
        {
            dbContext = context;
            dbSet = dbContext.Set<T>();
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Delete(int id)
        {
            T entity = dbSet.Find(id);
            dbSet.Remove(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public void Edit(T entity)
        {
            dbSet.Attach(entity);
            dbContext.Attach(entity).State = EntityState.Modified;
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            return dbSet.Where(predicate).AsNoTracking().SingleOrDefault();
        }

        public List<T> GetAll()
        {
            return dbSet.ToList();
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public List<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return dbSet.Where(predicate).ToList();
        }
        public async Task<List<T>> FindByAsync(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.Where(predicate).ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.Where(predicate).SingleOrDefaultAsync();
        }

        public T GetById(int id)
        {
            return dbSet.Find(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }
    
    }
}
