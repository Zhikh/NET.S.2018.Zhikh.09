using System.Collections.Generic;
using Task2.BLL.Interface.Entities;
using Task2.DAL.Interfaces.DTO;

namespace Task2.BLL.Mappers
{
    public static class PersonMappers
    {
        public static DalPerson ToDalPerson(this Person person)
        {
            return new DalPerson
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                SecondName = person.SecondName,
                Contact = new DalContactData
                {
                    Email = person.Email
                },
                Accounts = person.Accounts.ToDalAccount()
            };
        }

        public static Person ToPerson(this DalPerson person)
        {
            return new Person
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                SecondName = person.SecondName,
                Email = person.Contact.Email,
                Accounts = person.Accounts.ToAccount()
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
