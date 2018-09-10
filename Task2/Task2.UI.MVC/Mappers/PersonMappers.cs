using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Task2.BLL.Interface.Entities;
using Task2.UI.MVC.Models.Person;

namespace Task2.UI.MVC.Mappers
{
    public static class PersonMappers
    {
        public static Person ToPerson(this PersonCreateModel entity)
        {
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
            return new PersonModel
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                AccountsCount = entity.Accounts.Count
            };
        }
    }
}