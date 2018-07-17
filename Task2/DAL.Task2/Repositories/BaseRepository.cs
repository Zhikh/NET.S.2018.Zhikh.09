using System;
using System.Collections.Generic;

namespace DAL.Task2.Repositories
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        public ICollection<TEntity> Entities { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Create(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public TEntity GetById(int id)
        {
            throw new NotImplementedException();
        }

        public TEntity GetByValue(string value)
        {
            throw new NotImplementedException();
        }

        public abstract void Update(TEntity entity);
    }
}
