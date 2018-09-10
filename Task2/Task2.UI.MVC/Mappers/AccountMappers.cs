using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Task2.BLL.Interface.Entities;
using Task2.UI.MVC.Models.Account;
using Task2.UI.MVC.Models.Person;

namespace Task2.UI.MVC.Mappers
{
    public static class AccountMappers
    {
        public static AccountModel ToAccountModel(this Account entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new AccountModel
            {
                // TODO: Id = entity.Id - BLL (+mappers)
                Number = entity.Number,
                Balance = entity.InvoiceAmount,
                Owner = new PersonModel
                {
                    FirstName = entity.Owner.FirstName,
                    MiddleName = entity.Owner.SecondName,
                    LastName = entity.Owner.LastName
                }
            };
        }

        public static AccountDetailModel ToAccountDetailModel(this Account entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new AccountDetailModel
            {
                // TODO: Id = entity.Id - BLL (+mappers)
                Number = entity.Number,
                Balance = entity.InvoiceAmount,
                Owner = new PersonModel
                {
                    FirstName = entity.Owner.FirstName,
                    MiddleName = entity.Owner.SecondName,
                    LastName = entity.Owner.LastName
                },
                Bonuses = entity.Bonuses,
                AccountType = new AccountTypeModel
                {
                    Name = entity.AccountType.Name,
                    DepositCost = entity.AccountType.DepositCost,
                    WithdrawCost = entity.AccountType.WithdrawCost
                }
            };
        }

        public static Account ToAccount(this AccountCreateModel entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new Account
            {
                // TODO: Id = entity.Id - BLL (+mappers)
                Number = entity.Number,
                InvoiceAmount = entity.Balance,
                Owner = new Person
                {
                    FirstName = entity.Owner.FirstName,
                    SecondName = entity.Owner.MiddleName,
                    LastName = entity.Owner.LastName
                },
                AccountType = new AccountType
                {
                    Name = entity.AccountType.Name,
                    DepositCost = entity.AccountType.DepositCost,
                    WithdrawCost = entity.AccountType.WithdrawCost
                }
            };
        }
    }
}