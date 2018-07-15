using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Task2
{
    interface IService<T> where T: class
    {
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        T GetByValue(string value);
    }
}
