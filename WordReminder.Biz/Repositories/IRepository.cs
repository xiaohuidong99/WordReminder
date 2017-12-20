using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WordReminder.Biz.Repositories
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        Task<List<T>> GetAllAsync();
        List<T> FindBy(Expression<Func<T, bool>> predicate);
        Task<List<T>> FindByAsync(Expression<Func<T, bool>> predicate);
        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        T Get(Expression<Func<T, bool>> predicate);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Edit(T entity);
        T Delete(int id);
        void Delete(T entity);
    }
}
