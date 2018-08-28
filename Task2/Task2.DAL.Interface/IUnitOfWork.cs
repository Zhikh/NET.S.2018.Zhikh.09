using System;

namespace Task2.DAL.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
    }
}
