using System.Collections.Generic;

namespace Logic.Task2
{
    public interface IService<T> where T : class
    {
        void Create(T entity);

        void Update(T entity);

        void Delete(int id);

        T GetById(int id);

        T GetByValue(string value);

        ICollection<T> GetAll();
    }
}
