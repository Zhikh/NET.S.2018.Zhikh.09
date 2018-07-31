using System;
using Task2.BLL.Interface.Entities;
using Task2.DAL.Interfaces.DTO;

namespace Task2.BLL.Mappers
{
    public static class AccountMappers
    {
        public static DalAccount ToDalAccount(this AccountBase baseAccount)
        {
            throw new NotImplementedException();
        }

        public static AccountBase ToAccountBase(this DalAccount baseAccount)
        {
            throw new NotImplementedException();
        }
    }
}
