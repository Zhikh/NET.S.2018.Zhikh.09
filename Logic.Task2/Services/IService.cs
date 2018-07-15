using System.Collections.Generic;

namespace Logic.Task2
{
    public interface IService<T> where T : class
    {
        /// <summary>
        /// Create new entity
        /// </summary>
        /// <param name="entity"> New entity </param>
        void Create(T entity);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity"> Update entity </param>
        void Update(T entity);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="id"> Entity id</param>
        void Delete(int id);

        /// <summary>
        /// Get entity by id
        /// </summary>
        /// <param name="id"> Entity id </param>
        /// <returns> Entity </returns>
        T GetById(int id);

        /// <summary>
        /// Get entity by value
        /// </summary>
        /// <param name="value"> Value for searching </param>
        /// <returns> Entity </returns>
        T GetByValue(string value);

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns></returns>
        ICollection<T> GetAll();
    }
}
