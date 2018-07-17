using System;
using Core.Task2.Entities;
using DAL.Task2;
using DAL.Task2.Repositories;

namespace Logic.Task2
{
    public class AccountService : IAccountService<Account>
    {
        public IRepository<Account> _accountRepository { get; set; }
        private IRepository<Person> _personRepository;

        public AccountService()
        {
            _accountRepository = new FakeAccountRepository();
            _personRepository = new FakePersonRepository();
        }

        /// <summary>
        /// It is the operation of closing the account.
        /// </summary>
        /// <param name="accounNumber"> Number of account </param>
        public void Close(string accounNumber)
        {
            if (string.IsNullOrEmpty(accounNumber))
            {
                throw new ArgumentException("Account number can't be null or empty!");
            }

            Account account = _accountRepository.GetByValue(accounNumber);

            if (account == null)
            {
                throw new ArgumentException("This account doesn't exist!");
            }

            if (account == null)
            {
                throw new ArgumentNullException("Account can't be null!");
            }

            _accountRepository.Delete(account.Id);
        }

        /// <summary>
        /// It is the operation of account replenishment.
        /// </summary>
        /// <param name="accounNumber"> Number of account </param>
        /// <param name="value"> Amount of money </param>
        public void Deposit(string accounNumber, decimal value)
        {
            Account account = _accountRepository.GetByValue(accounNumber);

            if (account == null)
            {
                throw new ArgumentException("This account doesn't exist!");
            }

            account.Bonuses += (int)(value / account.AccountType.ReplenishmentCost);
            account.InvoiceAmount += value;
            _accountRepository.Update(account);
        }

        /// <summary>
        /// Opens account
        /// </summary>
        /// <param name="account"> Account entity </param>
        public void Open(Account entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Account can't be null!");
            }

            if (entity.Owner == null)
            {
                throw new ArgumentNullException("Owner of account can't be null!");
            }

            Person person = _personRepository.GetAll().FindFirst(entity.Owner);

            if (person == null)
            {
                person = entity.Owner;
                _personRepository.Create(person);
            }

            _accountRepository.Create(entity);

            person.Accounts.Add(entity);
        }

        /// <summary>
        /// It is the operation of withdrawing money from the account.
        /// </summary>
        /// <param name="accounNumber"> Number of account </param>
        /// <param name="value"> Amount of money </param>
        public void Withdraw(string accounNumber, decimal value)
        {
            Account account = _accountRepository.GetByValue(accounNumber);

            if (account == null)
            {
                throw new ArgumentException("This account doesn't exist!");
            }

            if (account.InvoiceAmount == 0)
            {
                throw new ArgumentException("The invoice is empty!");
            }

            if (account.InvoiceAmount - value < 0)
            {
                throw new ArgumentException("There is not enough money to perform the operation!");
            }

            account.Bonuses -= (int)(value / account.AccountType.BalanceCost);
            account.InvoiceAmount -= value;
            _accountRepository.Update(account);
        }
    }
}
