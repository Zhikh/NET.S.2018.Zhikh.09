using System;
using Core.Task2.Entities;
using Core.Task2.Strategies;
using DAL.Task2.Repositories;
using Logic.Task2;

namespace UI.Task2.ConsoleApp
{
    class Program
    {
        private static Account _accountData = new Account(new FakeAccountNumberGenerator())
        {
            Owner = new Person
            {
                LastName = "Smbd1",
                FirstName = "Smbd1",
                Passport = new PassportData
                {
                    IdentityNumber = "3024567V912VV5",
                    SerialNumber = "VV2000233"
                },
                Contact = new ContactData
                {
                    Email = "podg@test.com",
                    ContactPhone = "+ 375 29 399 05 33"
                },
                Address = new AdressData
                {
                    Country = "Somewhere",
                    State = "Somewhere",
                    City = "Somewhere",
                    Street = "Without name"
                }
            },
            InvoiceAmount = 100,
            Bonuses = 0,
            AccountType = new AccountType
            {
                Name = ":)",
                BalanceCost = 100,
                ReplenishmentCost = 10
            }
        };
        private static Account _anotherAccountData = new Account(new FakeAccountNumberGenerator())
        {
            Owner = new Person
            {
                LastName = "Smbd2",
                FirstName = "Smbd2",
                Passport = new PassportData
                {
                    IdentityNumber = "3024567V912VV5",
                    SerialNumber = "VV2000233"
                },
                Contact = new ContactData
                {
                    Email = "podg@test.com",
                    ContactPhone = "+ 375 29 399 05 33"
                },
                Address = new AdressData
                {
                    Country = "Somewhere",
                    State = "Somewhere",
                    City = "Somewhere",
                    Street = "Without name"
                }
            },
            InvoiceAmount = 100,
            Bonuses = 0,
            AccountType = new AccountType
            {
                Name = ":)",
                BalanceCost = 100,
                ReplenishmentCost = 10
            }
        };
        private static Account _newAccountData = new Account(new FakeAccountNumberGenerator())
        {
            Owner = new Person
            {
                LastName = "Smbd3",
                FirstName = "Smbd3",
                Passport = new PassportData
                {
                    IdentityNumber = "3024567V912VV0",
                    SerialNumber = "AV2000233"
                },
                Contact = new ContactData
                {
                    Email = "pogg@test.com",
                    ContactPhone = "+ 375 29 382 65 33"
                },
                Address = new AdressData
                {
                    Country = "Somewhere",
                    State = "Somewhere",
                    City = "Somewhere",
                    Street = "Without name"
                }
            },
            InvoiceAmount = 1900,
            Bonuses = 20,
            AccountType = new AccountType
            {
                Name = ":)",
                BalanceCost = 10,
                ReplenishmentCost = 10
            }
        };
        static void Main(string[] args)
        {
            try
            {
                IRepository<Account> repository = new FakeAccountRepository();
                
                CheckCreationByRepository(repository);

                IAccountService<Account> service = new AccountService();

                service.Open(_newAccountData);
                PrintAllAccounts(service._accountRepository);

                Console.ReadKey();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void CheckCreationByRepository(IRepository<Account> repository)
        {
            Console.WriteLine("IRepository:");

            repository.Create(_accountData);
            PrintAllAccounts(repository);

            repository.Create(_anotherAccountData);
            PrintAllAccounts(repository);
        }

        private static void PrintAllAccounts(IRepository<Account> repository)
        {
            var accounts = repository.GetAll();
            foreach (var account in accounts)
            {
                Console.WriteLine("Account number: " + account.Number);
                Console.WriteLine("Owner info: " + account.Owner.FirstName + " " + account.Owner.LastName + 
                    " (Amount of accounts: " + account.Owner.Accounts.Count + ")");
                Console.WriteLine("Balance: " + account.InvoiceAmount);
                Console.WriteLine("Bonuses: " + account.Bonuses);
                Console.WriteLine("-------------");
            }
        }
    }
}
