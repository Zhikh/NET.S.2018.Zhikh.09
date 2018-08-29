using System;
using System.Data.Entity;
using Task2.DAL.Interface;

namespace Task2.DAL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        #region Fields
        private bool _disposed = false;
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork" />.
        /// </summary>
        /// <param name="context"> The instance of the <see cref="DbContext"/> class </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="context"/> is null.
        /// </exception>
        public UnitOfWork(DbContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// The instance of the <see cref="DbContext"/> class 
        /// </summary>
        public DbContext Context { get; private set; }

        #region IUnitOfWork
        /// <summary>
        /// Save context changes
        /// </summary>
        public void SaveChanges()
        {
            Context.SaveChanges();
        }
        #endregion

        #region Dispose
        /// <summary>
        /// Performs tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
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
    }
}
