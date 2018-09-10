using System;
using System.Collections.Generic;
using Task2.BLL.Interface.Entities;
using Task2.UI.MVC.Models.Person;

namespace Task2.UI.MVC.Mappers
{
    public static class PersonMappers
    {
        public static Person ToPerson(this PersonCreateModel entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new Person
            {
                FirstName = entity.FirstName,
                SecondName = entity.MiddleName,
                LastName = entity.LastName,
                SerialNumber = entity.SerialNumber
            };
        }

        public static Person ToPerson(this PersonDetailModel entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new Person
            {
                FirstName = entity.FirstName,
                SecondName = entity.MiddleName,
                LastName = entity.LastName,
                SerialNumber = entity.SerialNumber
            };
        }

        public static PersonModel ToPersonModel(this Person entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new PersonModel
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName
            };
        }

        public static PersonDetailModel ToPersonDetailModel(this Person entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new PersonDetailModel
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName
            };
        }

        public static ICollection<PersonModel> ToPersonModels(this IEnumerable<Person> entities)
        {
            return ToMany<Person, PersonModel>(entities, ToPersonModel);
        }

        #region Additional methods
        private static ICollection<TTo> ToMany<TFrom, TTo>(IEnumerable<TFrom> entities, Func<TFrom, TTo> func)
        {
            if (entities == null)
            {
                return null;
            }

            var result = new List<TTo>();
            foreach (var element in entities)
            {
                result.Add(func(element));
            }

            return result;
        }
        #endregion
    }
}