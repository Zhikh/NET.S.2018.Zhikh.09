using System.Collections.Generic;
using Task2.BLL.Interface.Entities;

namespace Task2.BLL.Interface.Services
{
    public interface IPersonService
    {
        bool Create(Person person);

        Person Get(string value);

        IEnumerable<Person> GetAll();

        void Update(Person person);
    }
}
