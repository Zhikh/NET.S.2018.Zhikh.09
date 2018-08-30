using System;
using System.Collections.Generic;
using System.Linq;
using Task2.BLL.Interface.Entities;
using Task2.DAL.Interfaces.DTO;

namespace Task2.BLL.Mappers
{
    public static class AccountMappers
    {
        #region Extensions
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
                Accounts = baseAccount.Owner.Accounts.ToDalAccount(),
                Contact = new DalContactData
                {
                    Email = baseAccount.Owner.Email
                }
            };

            return new DalAccount
            {
                Number = baseAccount.Number,
                Owner = dalPerson,
                InvoiceAmount = baseAccount.InvoiceAmount,
                Bonuses = baseAccount.Bonuses,
                AccountType = baseAccount.AccountType.ToDalAccountType(),
                IsOpen = baseAccount.IsOpen
            };
        }

        /// <summary>
        /// Converts collection of <see cref="Account"/> in collection of <see cref="DalAccount"/>
        /// </summary>
        /// <param name="accounts"> Collection for converting from <see cref="Account"/> </param>
        /// <returns> Collection of <see cref="DalAccount"/> </returns>
        public static ICollection<DalAccount> ToDalAccount(this IEnumerable<Account> accounts)
        {
            return ToMany(accounts, ToDalAccount);
        }

        /// <summary>
        /// Converts entity of <see cref="DalAccount"/> in <see cref="Account"/>
        /// </summary>
        /// <param name="account"> Entity for converting from <see cref="DalAccount"/> </param>
        /// <returns> Entity of  <see cref="Account"/> </returns>
        public static Account ToAccount(this DalAccount account)
        {
            if (account == null)
            {
                return null;
            }

            return new Account
            {
                Number = account.Number,
                Owner = new Person()
                {
                    FirstName = account.Owner.FirstName,
                    SecondName = account.Owner.SecondName,
                    LastName = account.Owner.LastName,
                    SerialNumber = account.Owner.SerialNumber,
                    Email = account.Owner.Contact.Email,
                    Accounts = account.Owner.Accounts.ToAccounts().ToList()
                },
                InvoiceAmount = account.InvoiceAmount,
                Bonuses = account.Bonuses,
                AccountType = account.AccountType.ToAccountType(),
                IsOpen = account.IsOpen
            };
        }

        /// <summary>
        /// Converts collection of <see cref="DalAccount"/> in collection of <see cref="Account"/>
        /// </summary>
        /// <param name="baseAccounts"> Collection for converting from <see cref="DalAccount"/> </param>
        /// <returns> Collection of <see cref="Account"/> </returns>
        public static ICollection<Account> ToAccounts(this IEnumerable<DalAccount> accounts)
        {
            return ToMany(accounts, ToAccount);
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
