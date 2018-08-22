using System.Collections.Generic;
using Task2.BLL.Interface.Entities;

namespace Task2.BLL.Interface.Services
{
    interface IPersonService
    {
        void Create(Person person);

        void Update(Person person);

        void Delete(Person person);

        void Get(string value);

        IEnumerable<Person> GetAll();
    }
}
