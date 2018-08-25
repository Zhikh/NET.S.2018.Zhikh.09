using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.DAL.Interfaces.DTO;
using Task2.ORM;

namespace Task2.DAL.Mappers
{
    public static class PersonMappers
    {
        public static DalPerson ToDalPerson(this Person person)
        {
            throw new NotImplementedException();
        }

        public static Person ToDalPerson(this DalPerson person)
        {
            throw new NotImplementedException();
        }
    }
}
