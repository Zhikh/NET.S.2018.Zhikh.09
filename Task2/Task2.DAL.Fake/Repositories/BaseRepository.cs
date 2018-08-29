using System;
using System.Collections.Generic;
using System.Linq;
using Task2.DAL.Interface.DTO;
using Task2.DAL.Interface.Repositories;

namespace DAL.Task2.Repositories
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity>
        where TEntity : IEntity
    {
        #region Public API
        /// <summary>
        ///  Initializes a new instance
        /// </summary>
        public BaseRepository()
        {
            Entities = new List<TEntity>();
        }

        /// <summary>
        /// Collection of <see cref="TEntity"/>
        /// </summary>
        public ICollection<TEntity> Entities { get; }

        /// <summary>
        /// Adds entity of the <see cref="TEntity"/> class to context
        /// </summary>
        /// <param name="entity"> Entity for saving </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="entity"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="entity"/> is invalid.
        ///     <paramref name="entity"/> isn't find.
        /// </exception>
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

        /// <summary>
        /// Removes entity of the <see cref="TEntity"/> class
        /// </summary>
        /// <param name="entity"> Entity for removing </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="entity"/> is null.
        /// </exception>
        public void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("This entity doesn't exist!");
            }

            Entities.Remove(entity);
        }

        /// <summary>
        /// Returns a collection of <see cref="TEntity"/> objects
        /// </summary>
        /// <returns> A collection of <see cref="TEntity"/> objects </returns>
        public IEnumerable<TEntity> GetAll() => Entities;

        public TEntity GetById(int id)
        {
            foreach (var entity in Entities)
            {
                if (entity.Id == id)
                {
                    return entity;
                }
            }

            return default(TEntity);
        }

        /// <summary>
        /// Returns account with <paramref name="value"/>
        /// </summary>
        /// <param name="value"> Some value for searching </param>
        /// <returns> Entity of the <see cref="TEntity"/> class </returns>
        public TEntity GetByValue(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("Value can't be null or empty!");
            }

            return FindEntity(value);
        }

        /// <summary>
        /// Returns entity of <see cref="TEntity"/> finding by <paramref name="predicate"/>
        /// </summary>
        /// <param name="predicate"> Rule for searching </param>
        /// <returns> Entity of the <see cref="TEntity"/> class </returns>
        public TEntity GetByPredicate(Func<TEntity, bool> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return Entities.FirstOrDefault(predicate);
        }

        /// <summary>
        /// Updates account by values from entity of the <see cref="TEntity"/> class
        /// </summary>
        /// <param name="entity">  Entity of the <see cref="TEntity"/> class </param>
        void IRepository<TEntity>.Update(TEntity entity)
        {
            Update(entity);
        }
        #endregion

        #region Additional abstract methods
        internal abstract TEntity FindEntity(string value);
        internal abstract void Update(TEntity entity);
        internal abstract bool IsInvalid(TEntity entity);
        #endregion
    }
}
