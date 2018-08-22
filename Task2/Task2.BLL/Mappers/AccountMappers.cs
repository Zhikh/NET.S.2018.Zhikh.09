using System.Collections.Generic;
using Task2.BLL.Interface.Entities;
using Task2.DAL.Interfaces.DTO;

namespace Task2.BLL.Mappers
{
    public static class AccountMappers
    {
        public static DalAccount ToDalAccount(this AccountBase baseAccount)
        {
            var dalPerson = new DalPerson
            {
                FirstName = baseAccount.Owner.FirstName,
                LastName = baseAccount.Owner.LastName,
                SecondName = baseAccount.Owner.SecondName,
                SerialNumber = baseAccount.Owner.SerialNumber,
                Accounts = baseAccount.Owner.Accounts.ToDalAccount()
            };

            return new DalAccount
            {
                Number = baseAccount.Number,
                Owner = dalPerson,
                InvoiceAmount = baseAccount.InvoiceAmount,
                Bonuses = baseAccount.Bonuses,
                AccountType = baseAccount.AccountType.ToDalAccountType()
            };

        }

        public static IEnumerable<DalAccount> ToDalAccount(this IEnumerable<AccountBase> baseAccounts)
        {
            foreach (var element in baseAccounts)
            {
                yield return element.ToDalAccount();
            }
        }

        public static AccountBase ToAccountBase(this DalAccount dalAccount)
        {
            return new AccountBase
            {
                Number = dalAccount.Number,
                Owner = dalAccount.Owner.ToPerson(),
                InvoiceAmount = dalAccount.InvoiceAmount,
                Bonuses = dalAccount.Bonuses,
                AccountType = dalAccount.AccountType.ToAccountType()
            };
        }

        public static IEnumerable<AccountBase> ToAccount(this IEnumerable<DalAccount> dalAccounts)
        {
            foreach (var element in dalAccounts)
            {
                yield return element.ToAccountBase();
            }
        }
    }
}
