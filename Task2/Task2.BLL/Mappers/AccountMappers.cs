using System.Collections.Generic;
using Task2.BLL.Interface.Entities;
using Task2.DAL.Interfaces.DTO;

namespace Task2.BLL.Mappers
{
    public static class AccountMappers
    {
        /// <summary>
        /// Converts entity of <see cref="Account"/> in <see cref="DalAccount"/>
        /// </summary>
        /// <param name="baseAccount"> Entity for converting from <see cref="Account"/> </param>
        /// <returns> Entity of  <see cref="DalAccount"/> </returns>
        public static DalAccount ToDalAccount(this Account baseAccount)
        {
            if (baseAccount == null)
            {
                return null;
            }

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

        /// <summary>
        /// Converts collection of <see cref="Account"/> in collectiono of <see cref="DalAccount"/>
        /// </summary>
        /// <param name="baseAccounts"> Collection for converting from <see cref="Account"/> </param>
        /// <returns> Collection of <see cref="DalAccount"/> </returns>
        public static IEnumerable<DalAccount> ToDalAccount(this IEnumerable<Account> baseAccounts)
        {
            foreach (var element in baseAccounts)
            {
                yield return element.ToDalAccount();
            }
        }

        /// <summary>
        /// Converts entity of <see cref="DalAccount"/> in <see cref="Account"/>
        /// </summary>
        /// <param name="dalAccount"> Entity for converting from <see cref="DalAccount"/> </param>
        /// <returns> Entity of  <see cref="Account"/> </returns>
        public static Account ToAccountBase(this DalAccount dalAccount)
        {
            if (dalAccount == null)
            {
                return null;
            }

            return new Account
            {
                Number = dalAccount.Number,
                Owner = dalAccount.Owner.ToPerson(),
                InvoiceAmount = dalAccount.InvoiceAmount,
                Bonuses = dalAccount.Bonuses,
                AccountType = dalAccount.AccountType.ToAccountType()
            };
        }

        /// <summary>
        /// Converts collection of <see cref="DalAccount"/> in collectiono of <see cref="Account"/>
        /// </summary>
        /// <param name="baseAccounts"> Collection for converting from <see cref="DalAccount"/> </param>
        /// <returns> Collection of <see cref="Account"/> </returns>
        public static IEnumerable<Account> ToAccount(this IEnumerable<DalAccount> dalAccounts)
        {
            foreach (var element in dalAccounts)
            {
                yield return element.ToAccountBase();
            }
        }
    }
}
