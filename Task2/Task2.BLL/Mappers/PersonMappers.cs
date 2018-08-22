using System.Collections.Generic;
using System.Linq;
using Task2.BLL.Interface.Entities;
using Task2.DAL.Interfaces.DTO;

namespace Task2.BLL.Mappers
{
    public static class PersonMappers
    {
        public static DalPerson ToDalPerson(this Person person)
        {
            if (person == null)
            {
                return null;
            }

            var dalPerson = new DalPerson
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                SecondName = person.SecondName,
                SerialNumber = person.SerialNumber,
                Accounts = person.Accounts.ToDalAccount().ToList()
            };

            dalPerson.Contact = new DalContactData
            {
                Email = person.Email
            };

            return dalPerson;
        }

        public static Person ToPerson(this DalPerson person)
        {
            if (person == null)
            {
                return null;
            }

            return new Person
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                SecondName = person.SecondName,
                SerialNumber = person.SerialNumber,
                Email = person.Contact.Email,
                Accounts = person.Accounts.ToAccount().ToList()
            };
        }

        public static IEnumerable<Person> ToPerson(this IEnumerable<DalPerson> persons)
        {
            foreach (var element in persons)
            {
                yield return element.ToPerson();
            }
        }

        public static IEnumerable<DalPerson> ToDalPerson(this IEnumerable<Person> persons)
        {
            foreach (var element in persons)
            {
                yield return element.ToDalPerson();
            }
        }
    }
}
