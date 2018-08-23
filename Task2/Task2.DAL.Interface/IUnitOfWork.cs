using System;

namespace Task2.DAL.Interface.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
    }
}
