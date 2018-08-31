using System;
using System.Collections.Generic;
using System.Linq;
using Task2.BLL.Interface.Entities;
using Task2.DAL.Interfaces.DTO;

namespace Task2.BLL.Mappers
{
    public static class PersonMappers
    {
        #region Extensions
        /// <summary>
        /// Converts entity of <see cref="Person"/> in <see cref="DalPerson"/>
        /// </summary>
        /// <param name="person"> Entity for converting from <see cref="Person"/> </param>
        /// <returns> Entity of  <see cref="DalPerson"/> </returns>
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
                Accounts = person.Accounts.ToDalAccount().ToList(),
                IsDeleted = person.IsDeleted
            };

            dalPerson.Contact = new DalContactData
            {
                Email = person.Email
            };

            return dalPerson;
        }

        /// <summary>
        /// Converts entity of <see cref="DalPerson"/> in <see cref="Person"/>
        /// </summary>
        /// <param name="person"> Entity for converting from <see cref="DalPerson"/> </param>
        /// <returns> Entity of  <see cref="Person"/> </returns>
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
                Accounts = person.Accounts.ToAccounts().ToList(),
                IsDeleted = person.IsDeleted
            };
        }

        /// <summary>
        /// Converts collection of <see cref="Person"/> in <see cref="DalPerson"/>
        /// </summary>
        /// <param name="person"> Collection for converting from <see cref="Person"/> </param>
        /// <returns> Collection of  <see cref="DalPerson"/> </returns>
        public static ICollection<Person> ToPersons(this IEnumerable<DalPerson> persons)
        {
            return ToMany(persons, ToPerson);
        }

        /// <summary>
        /// Converts collection of <see cref="Person"/> in <see cref="DalPerson"/>
        /// </summary>
        /// <param name="person"> Collection for converting from <see cref="Person"/> </param>
        /// <returns> Collection of  <see cref="DalPerson"/> </returns>
        public static ICollection<DalPerson> ToDalPersons(this IEnumerable<Person> persons)
        {
            return ToMany(persons, ToDalPerson);
        }
        #endregion

        #region Additional methods
        private static ICollection<TTo> ToMany<TFrom, TTo>(IEnumerable<TFrom> persons, Func<TFrom, TTo> func)
        {
            if (persons == null)
            {
                return null;
            }

            var result = new List<TTo>();
            foreach (var element in persons)
            {
                result.Add(func(element));
            }

            return result;
        }
        #endregion
    }
}
