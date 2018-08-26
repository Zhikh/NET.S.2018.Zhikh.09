using System;
using System.Collections.Generic;
using Task2.DAL.Interfaces.DTO;
using Task2.ORM;

namespace Task2.DAL.Mappers
{
    public static class PersonMappers
    {
        public static DalPerson ToDalPerson(this Person person)
        {
            if (person == null)
            {
                return null;
            }

            return new DalPerson
            {
                Id = person.Id,
                FirstName = person.FirstName,
                SecondName = person.MiddleName,
                LastName = person.LastName,
                SerialNumber = person.Passport.PassportSeries,
                Contact = new DalContactData
                {
                    Email = person.ContactData.Email
                },
                Accounts = person.Accounts.ToDalAccounts()
            };
        }

        public static IEnumerable<DalPerson> ToDalPersons(this IEnumerable<Person> person)
        {
            return ToMany(person, ToDalPerson);
        }

        public static Person ToPerson(this DalPerson person)
        {
            if (person == null)
            {
                return null;
            }

            return new Person()
            {
                Id = person.Id,
                FirstName = person.FirstName,
                MiddleName = person.SecondName,
                LastName = person.LastName,
                Passport = new Passport
                {
                    PassportSeries = person.SerialNumber
                },
                ContactData = new ContactData
                {
                    Email = person.Contact.Email
                },
                Accounts = person.Accounts.ToAccounts()
            };
        }

        public static IEnumerable<Person> ToPersons(this IEnumerable<DalPerson> person)
        {
            return ToMany(person, ToPerson);
        }

        private static ICollection<TTo> ToMany<TFrom, TTo>(IEnumerable<TFrom> accounts, Func<TFrom, TTo> func)
        {
            if (accounts == null)
            {
                return null;
            }

            var result = new List<TTo>();
            foreach (var element in accounts)
            {
                result.Add(func(element));
            }

            return result;
        }
    }
}
