using System;
using System.Collections.Generic;
using Task2.DAL.Interfaces.DTO;
using Task2.ORM;

namespace Task2.DAL.Mappers
{
    public static class PersonMappers
    {
        #region Extensions
        /// <summary>
        /// Converts entity of <see cref="Person"/> to <see cref="DalPerson"/>
        /// </summary>
        /// <param name="person"> Entity for converting from <see cref="Person"/> </param>
        /// <returns> Entity of  <see cref="DalPerson"/> </returns>
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
                Accounts = person.Accounts.ToDalAccounts(),
                IsDeleted = person.IsDeleted
            };
        }

        /// <summary>
        /// Converts collection of <see cref="Person"/> in <see cref="DalPerson"/>
        /// </summary>
        /// <param name="person"> Collection for converting from <see cref="Person"/> </param>
        /// <returns> Collection of  <see cref="DalPerson"/> </returns>
        public static ICollection<DalPerson> ToDalPersons(this IEnumerable<Person> person)
        {
            return ToMany(person, ToDalPerson);
        }

        /// <summary>
        /// Converts entity of <see cref="DalPerson"/> to <see cref="Person"/>
        /// </summary>
        /// <param name="person"> Entity for converting from <see cref="DalPerson"/> </param>
        /// <returns> Entity of  <see cref="Person"/> </returns>
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
                Accounts = person.Accounts.ToAccounts(),
                IsDeleted = person.IsDeleted
            };
        }

        /// <summary>
        /// Converts collection of <see cref="DalPerson"/> in <see cref="Person"/>
        /// </summary>
        /// <param name="person"> Collection for converting from <see cref="DalPerson"/> </param>
        /// <returns> Collection of  <see cref="Person"/> </returns>
        public static ICollection<Person> ToPersons(this IEnumerable<DalPerson> person)
        {
            return ToMany(person, ToPerson);
        }
        #endregion

        #region Additional methods
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
        #endregion
    }
}
