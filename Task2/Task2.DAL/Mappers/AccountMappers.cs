using System;
using System.Collections.Generic;
using Task2.DAL.Interfaces.DTO;
using Task2.ORM;

namespace Task2.DAL.Mappers
{
    public static class AccountMappers
    {
        public static DalAccount ToDalAccount(this Account account)
        {
            if (account == null)
            {
                return null;
            }

            return new DalAccount
            {
                Id = account.Id,
                Number = account.Number,
                Owner = new DalPerson()
                {
                    Id = account.Person.Id,
                    FirstName = account.Person.FirstName,
                    SecondName = account.Person.MiddleName,
                    LastName = account.Person.LastName,
                    SerialNumber = account.Person.Passport.PassportSeries,
                    Contact = new DalContactData
                    {
                        Email = account.Person.ContactData.Email
                    },
                    Accounts = account.Person.Accounts.ToDalAccounts()
                },
                InvoiceAmount = account.Invoice,
                Bonuses = account.Bonuses,
                AccountType = new DalAccountType
                {
                    Id = account.AccountType.Id,
                    Name = account.AccountType.Name,
                    BalanceCost = account.AccountType.DepositBonusCost,
                    ReplenishmentCost = account.AccountType.WithdrawBonusSost
                },
                IsOpen = account.IsOpen
            };
        }

        public static ICollection<DalAccount> ToDalAccounts(this IEnumerable<Account> accounts)
        {
            return ToMany(accounts, ToDalAccount);
        }

        public static Account ToAccount(this DalAccount account)
        {
            if (account == null)
            {
                return null;
            }

            return new Account
            {
                Id = account.Id,
                Number = account.Number,
                Person = new Person()
                {
                    Id = account.Owner.Id,
                    FirstName = account.Owner.FirstName,
                    MiddleName = account.Owner.SecondName,
                    LastName = account.Owner.LastName,
                    Passport = new Passport
                    {
                        PassportSeries = account.Owner.SerialNumber
                    },
                    ContactData = new ContactData
                    {
                        Email = account.Owner.Contact.Email
                    },
                    Accounts = account.Owner.Accounts.ToAccounts()
                },
                Invoice = account.InvoiceAmount,
                Bonuses = account.Bonuses,
                AccountType = new AccountType
                {
                    Id = account.AccountType.Id,
                    Name = account.AccountType.Name,
                    DepositBonusCost = account.AccountType.BalanceCost,
                    WithdrawBonusSost = account.AccountType.ReplenishmentCost
                },
                IsOpen = account.IsOpen
            };
        }

        public static ICollection<Account> ToAccounts(this IEnumerable<DalAccount> accounts)
        {
            return ToMany(accounts, ToAccount);
        }

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
    }
}
