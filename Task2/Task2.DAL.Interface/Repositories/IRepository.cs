using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Task2.DAL.Interface.DTO;

namespace Task2.DAL.Interface.Repositories
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        /// <summary>
        /// Create new entity
        /// </summary>
        /// <param name="entity"> New entity </param>
        void Create(TEntity entity);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity"> Update entity </param>
        void Update(TEntity entity);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="id"> Entity id</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Get entity by id
        /// </summary>
        /// <param name="id"> Entity id </param>
        /// <returns> Entity </returns>
        TEntity GetById(int id);

        /// <summary>
        /// Get entity by value
        /// </summary>
        /// <param name="func"> Expression for searching </param>
        /// <returns> Entity </returns>
        TEntity GetByPredicate(Expression<Func<TEntity, bool>> func);

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns> Collection of Entities</returns>
        IEnumerable<TEntity> GetAll();
    }
}
