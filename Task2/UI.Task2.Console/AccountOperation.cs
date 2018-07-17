using Core.Task2.Entities;
using Core.Task2.Strategies;
using Logic.Task2;
using System;
namespace UI.Task2.ConsoleApp
{
    internal static class AccountOperation
    {
        private static AccountType[] _accountsTypes = new AccountType[]
        {
            new AccountType
            {
                Name = nameof(TypeOfAccount.Silver),
                BalanceCost = 400,
                ReplenishmentCost = 200
            },
            new AccountType
            {
                Name = nameof(TypeOfAccount.Gold),
                BalanceCost = 300,
                ReplenishmentCost = 300
            },
            new AccountType
            {
                Name = nameof(TypeOfAccount.Platinum),
                BalanceCost = 200,
                ReplenishmentCost = 400
            }
        };

        internal enum Operation { Open, Deposit, Withdraw, Close, Exit }
        internal enum TypeOfAccount { Silver, Gold, Platinum }

        internal static void Close(IAccountService<Account> service)
        {
            Console.WriteLine("Account number:");
            string accountNumber = Console.ReadLine();
            service.Close(accountNumber);
        }

        internal static void Withdraw(IAccountService<Account> service)
        {
            Console.WriteLine("Account number:");
            string accountNumber = Console.ReadLine();

            Console.WriteLine("Withdraw amount:");
            if (decimal.TryParse(Console.ReadLine(), out decimal value))
            {
                service.Withdraw(accountNumber, value);
            }
        }

        internal static void Deposite(IAccountService<Account> service)
        {
            Console.WriteLine("Account number:");
            string accountNumber = Console.ReadLine();

            Console.WriteLine("Deposite amount:");
            if(decimal.TryParse(Console.ReadLine(), out decimal value))
            {
                service.Deposit(accountNumber, value);
            }
        }

        internal static void Open(IAccountService<Account> service) 
            => CreateAccount(service, CreatePerson());

        private static void CreateAccount(IAccountService<Account> service, Person person)
        {
            Console.WriteLine("Account data: \n Choose type of account:");
            int i = 0;
            foreach (var element in _accountsTypes)
            {
                Console.WriteLine($"{i++} - {element.Name} ");
            }

            if (int.TryParse(Console.ReadLine(), out int temp))
            {
                var operation = (TypeOfAccount)Enum.ToObject(typeof(TypeOfAccount), temp);

                AccountType type = GetAccountType(operation);

                var account = new Account(new FakeAccountNumberGenerator());
                account.AccountType = type;
                account.Owner = person;

                service.Open(account);
            }
        }

        private static Person CreatePerson()
        {
            #region Data
            Person person = new Person
            {
                Passport = new PassportData
                {
                    IdentityNumber = "3056767V912VV5",
                    SerialNumber = "VV2345233"
                },
                Contact = new ContactData
                {
                    ContactPhone = "+ 375 29 399 05 33"
                },
                Address = new AdressData
                {
                    Country = "Somewhere",
                    State = "Somewhere",
                    City = "Somewhere",
                    Street = "Without name"
                }
            };
            #endregion

            Console.WriteLine("Person data:");
            Console.Write("First name:");
            person.FirstName = Console.ReadLine();

            Console.Write("Last name:");
            person.LastName = Console.ReadLine();

            Console.Write("Email:");
            person.Contact.Email = Console.ReadLine();
            return person;
        }

        private static AccountType GetAccountType(TypeOfAccount operation)
        {
            AccountType type = new AccountType();

            foreach (var element in _accountsTypes)
            {
                if (element.Name == operation.ToString())
                {
                    return element;
                }
            }

            return null;
        }
    }
}
