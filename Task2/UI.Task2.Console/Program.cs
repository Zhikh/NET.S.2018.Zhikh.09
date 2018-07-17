using System;
using System.Diagnostics;
using Core.Task2.Entities;
using Core.Task2.Strategies;
using DAL.Task2.Repositories;
using Logic.Task2;
using static UI.Task2.ConsoleApp.AccountOperation;

namespace UI.Task2.ConsoleApp
{
    class Program
    {
        #region Test data
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
        #endregion

        static void Main(string[] args)
        {
            try
            {
                IRepository<Account> repository = new FakeAccountRepository();
                
                CheckCreationByRepository(repository);

                IAccountService<Account> service = new AccountService();

                service.Open(_newAccountData);
                PrintAllAccounts(service._accountRepository);

                CheckByInput(service);

                Console.ReadKey();
            }
            catch (ArgumentException ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                Console.WriteLine("Smth's gone wrong!");
            }
        }

        private static void CheckByInput(IAccountService<Account> service)
        {
            bool IsExit = false;
            Operation operation = Operation.Exit;

            do
            {
                Console.WriteLine($"Check operation: " +
                    $"\n {(int)Operation.Open} - {Operation.Open} " +
                    $"\n {(int)Operation.Deposit} - {Operation.Deposit}  " +
                    $"\n {(int)Operation.Withdraw} - {Operation.Withdraw}  " +
                    $"\n {(int)Operation.Close} - {Operation.Close} " +
                    $"\n {(int)Operation.Exit} - {Operation.Exit} ");

                if (int.TryParse(Console.ReadLine(), out int temp))
                {
                    operation = (Operation)Enum.ToObject(typeof(Operation), temp); 
                }

                switch (operation)
                {
                    case Operation.Open:
                        Open(service);
                        break;
                    case Operation.Deposit:
                        Deposite(service);
                        break;
                    case Operation.Withdraw:
                        Withdraw(service);
                        break;
                    case Operation.Close:
                        Close(service);
                        break;
                    default:
                        IsExit = true;
                        break;
                }
                PrintAllAccounts(service._accountRepository);
            }
            while (!IsExit);
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
