using System;
using System.Collections.Generic;
using Task2.BLL.Interface.Entities;
using Task2.DAL.Interfaces.DTO;

namespace Task2.BLL.Mappers
{
    public static class PersonMappers
    {
        public static DalPerson ToDalPerson(this Person person)
        {
            throw new NotImplementedException();
        }

        public static Person ToPerson(this DalPerson person)
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<Person> ToPerson(this IEnumerable<DalPerson> person)
        {
            throw new NotImplementedException();
        }
    }
}
