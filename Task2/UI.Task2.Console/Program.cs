using System;
using System.Linq;
using Ninject;
using Task2.BLL.Interface.Entities;
using Task2.BLL.Interface.Services;
using Task2.DAL.Interface.Strategies;
using Task2.DependencyResolver;

namespace UI.Task2.ConsoleApp
{
    class Program
    {
        private static readonly IKernel _resolver;
        private static readonly Account[] _accountData;
        private static readonly IAccountNumberGenerator<int> _numberGenerator;

        static Program()
        {
            _resolver = new StandardKernel();
            _resolver.ConfigurateResolver();

            _numberGenerator = _resolver.Get<IAccountNumberGenerator<int>>();

            _accountData = new Account[]
            {
                new Account(_numberGenerator)
                {
                    Owner = new Person
                    {
                        LastName = "Smbd",
                        FirstName = "Smbd",
                        Email = "podg@test.com",
                        SerialNumber = "12345678FF"
                    },

                    InvoiceAmount = 100,
                    Bonuses = 0,
                    AccountType = new AccountType
                    {
                        Name = ":)",
                        DepositCost = 100,
                        WithdrawCost = 10
                    }
                },
                new Account(_numberGenerator)
                {
                    Owner = new Person
                    {
                        LastName = "Smbd1",
                        FirstName = "Smbd1",
                        Email = "podg1@test.com",
                        SerialNumber = "11045678FF"
                    },

                    InvoiceAmount = 200,
                    Bonuses = 0,
                    AccountType = new AccountType
                    {
                        Name = ":)",
                        DepositCost = 100,
                        WithdrawCost = 10
                    }
                }
            };
        }

        static void Main(string[] args)
        {
            IAccountService service = _resolver.Get<IAccountService>();

            foreach (var account in _accountData)
            {
                service.Open(account);
                Console.WriteLine(account);
            }
            Console.WriteLine("Opened accounts: ");
            PrintAllAccounts(service);

            var accounts = service.GetAll();

            foreach (var account in accounts)
            {
                service.Deposit(account.Number, 1000);
            }

            Console.WriteLine("After deposit operation: ");
            PrintAllAccounts(service);

            foreach (var account in accounts)
            {
                service.Withdraw(account.Number, 10);
            }

            Console.WriteLine("After withdraw operation: ");
            PrintAllAccounts(service);

            Account firstAccount = accounts.First();
            Account lastAccount = accounts.Last();
            service.Transfer(firstAccount.Number, lastAccount.Number, firstAccount.InvoiceAmount);


            Console.WriteLine("After transfer operation: ");
            Console.WriteLine(firstAccount);
            Console.WriteLine(lastAccount);

            service.Close(firstAccount.Number);
            Console.WriteLine("After close operation: ");
            PrintAllAccounts(service);
        }

        private static void PrintAllAccounts(IAccountService service)
        {
            foreach (var item in service.GetAll())
            {
                Console.WriteLine(item);
            }
        }
    }
}
