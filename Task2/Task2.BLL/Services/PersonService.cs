using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.BLL.Interface.Entities;
using Task2.BLL.Interface.Services;
using Task2.BLL.Mappers;
using Task2.DAL.Interface.Repositories;

namespace Task2.BLL.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
        }

        public bool Create(Person person)
        {
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }

            Person repoPerson = Get(person.SerialNumber);
           
            if (repoPerson != null)
            {
                return false;
            }

            // TODO: add return value for repository.Create
            _personRepository.Create(person.ToDalPerson());

            return true;
        }

        public Person Get(string serialNumber)
        {
            if (string.IsNullOrEmpty(serialNumber))
            {
                throw new ArgumentException("message", nameof(serialNumber));
            }

            return _personRepository.GetByValue(serialNumber).ToPerson();
        }

        public IEnumerable<Person> GetAll()
            => _personRepository.GetAll().ToPerson();

        public void Update(Person person)
        {
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }

            _personRepository.Update(person.ToDalPerson());
        }
    }
}
