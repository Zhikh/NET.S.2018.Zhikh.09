using System;
using System.Linq;
using Task2.BLL.Interface.Entities;
using Task2.BLL.Interface.Services;
using Task2.BLL.Mappers;
using Task2.DAL.Interface.Repositories;

namespace Task2.BLL
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IPersonRepository _personRepository;

        public AccountService(IAccountRepository accountRepository, IPersonRepository personRepository)
        {
            _accountRepository = accountRepository;
            _personRepository = personRepository;
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
            
            AccountBase account = _accountRepository.GetByValue(accounNumber).ToAccountBase();

            if (account == null)
            {
                throw new ArgumentException("This account doesn't exist!");
            }

            if (account == null)
            {
                throw new ArgumentNullException("Account can't be null!");
            }
            
            _accountRepository.Delete(account.ToDalAccount());
        }

        /// <summary>
        /// It is the operation of account replenishment.
        /// </summary>
        /// <param name="accounNumber"> Number of account </param>
        /// <param name="value"> Amount of money </param>
        public void Deposit(string accounNumber, decimal value)
        {
            AccountBase account = _accountRepository.GetByValue(accounNumber).ToAccountBase();

            if (account == null)
            {
                throw new ArgumentException("This account doesn't exist!");
            }

            account.Deposit(value);
            
            _accountRepository.Update(account.ToDalAccount());
        }

        /// <summary>
        /// Opens account
        /// </summary>
        /// <param name="account"> Account entity </param>
        public void Open(AccountBase entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Account can't be null!");
            }

            if (entity.Owner == null)
            {
                throw new ArgumentNullException("Owner of account can't be null!");
            }
            
            Person person = _personRepository.GetAll().ToPerson().First( x => x == entity.Owner);

            if (person == null)
            {
                person = entity.Owner;
            }

            _accountRepository.Create(entity.ToDalAccount());

            person.Accounts.Add(entity);
        }

        /// <summary>
        /// It is the operation of withdrawing money from the account.
        /// </summary>
        /// <param name="accounNumber"> Number of account </param>
        /// <param name="value"> Amount of money </param>
        public void Withdraw(string accounNumber, decimal value)
        {
            AccountBase account = _accountRepository.GetByValue(accounNumber).ToAccountBase();

            if (account == null)
            {
                throw new ArgumentException("This account doesn't exist!");
            }

            account.Withdraw(value);
            
            _accountRepository.Update(account.ToDalAccount());
        }
    }
}
