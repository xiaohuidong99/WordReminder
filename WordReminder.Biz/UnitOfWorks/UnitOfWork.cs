using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordReminder.Biz.Repositories;
using WordReminder.Data;

namespace WordReminder.Biz.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WordReminderContext context;
        private bool disposed = false;

        public UnitOfWork(WordReminderContext context)
        {
            this.context = context;
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return new Repository<T>(context);
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        public async  Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing)
            {
                context.Dispose();
            }
            disposed = true;
        }
    }
}
