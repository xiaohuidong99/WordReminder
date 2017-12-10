using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordReminder.Biz.Repositories;

namespace WordReminder.Biz.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class;
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
