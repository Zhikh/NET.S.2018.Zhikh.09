using System;
using System.Collections.Generic;
using Core.Task2.Entities;

namespace DAL.Task2.Repositories
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        public BaseRepository()
        {
            Entities = new List<TEntity>();
        }

        public ICollection<TEntity> Entities { get; }
        
        public void Create(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity can't be null!");
            }

            if (IsInvalid(entity))
            {
                throw new ArgumentException("Entity has invalid field!");
            }

            if (Entities.FindFirst(entity) != null)
            {
                throw new ArgumentException("This entity already exists!");
            }

            Entities.Add(entity);
        }
        
        public void Delete(int id)
        {
            TEntity entity = GetById(id);
            if (Entities.FindFirst(entity) == null)
            {
                throw new ArgumentException("This entity doesn't exist!");
            }

            Entities.Remove(entity);
        }
        
        public ICollection<TEntity> GetAll() => Entities;

        public TEntity GetById(int id)
        {
            foreach (var entity in Entities)
            {
                if (entity.Id == id)
                {
                    return entity;
                }
            }

            return null;
        }
        
        public TEntity GetByValue(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("Value can't be null or empty!");
            }

            return FindEntity(value);
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        void IRepository<TEntity>.Update(TEntity entity)
        {
            Update(entity);
        }

        internal abstract TEntity FindEntity(string value);
        internal abstract void Update(TEntity entity);
        internal abstract bool IsInvalid(TEntity entity);
        
    }
}
