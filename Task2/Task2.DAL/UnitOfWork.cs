using System;
using System.Data.Entity;
using Task2.DAL.Interface.Repositories;

namespace Task2.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposed = false;

        public UnitOfWork(DbContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public DbContext Context { get; private set; }

        #region Dispose
        public void Dispose()
        {
            Dispose(true);
        }

        protected void Dispose(bool disposing)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(Context));

            if (disposing)
                GC.SuppressFinalize(this);

            if (Context != null)
                Context.Dispose();

            _disposed = true;
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
        #endregion

        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}
