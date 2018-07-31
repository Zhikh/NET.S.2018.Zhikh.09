using System;
using System.Linq;
using Task2.BLL.Exceptions;
using Task2.BLL.Interface.Entities;
using Task2.BLL.Interface.Services;
using Task2.BLL.Mappers;
using Task2.DAL.Interface.Repositories;
using Task2.DAL.Interface.Strategies;

namespace Task2.BLL
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IPersonRepository _personRepository;

        private IAccountNumberGenerator<int> _accountNumberGenerator;

        public AccountService(IAccountRepository accountRepository, IPersonRepository personRepository, IAccountNumberGenerator<int> numberGenerator)
        {
            _accountRepository = accountRepository ?? 
                throw new ArgumentNullException($"The {nameof(accountRepository)} can't be null!");
            _personRepository = personRepository ?? 
                throw new ArgumentNullException($"The {nameof(personRepository)} can't be null!");
            _accountNumberGenerator = numberGenerator ?? 
                throw new ArgumentNullException($"The {nameof(numberGenerator)} can't be null!");
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
            if (value <= 0)
            {
                throw new ArgumentException($"The {nameof(value)} can't be less than 0 or be 0!");
            }

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
                _personRepository.Create(person.ToDalPerson());
            }

            entity.Number = _accountNumberGenerator.GenerateNumber(entity.GetHashCode());

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
            if (value <= 0)
            {
                throw new ArgumentException($"The {nameof(value)} can't be less than 0 or be 0!");
            }

            AccountBase account = _accountRepository.GetByValue(accounNumber).ToAccountBase();

            if (account == null)
            {
                throw new ArgumentException("This account doesn't exist!");
            }

            account.Withdraw(value);
            
            _accountRepository.Update(account.ToDalAccount());
        }

        /// <summary>
        /// It is the operation of withdrawing money from the account.
        /// </summary>
        /// <param name="accounNumber"> Number of account </param>
        /// <param name="value"> Amount of money </param>
        public void Transfer(string sourceAccountNumber, string destinationAccountNumber, decimal value)
        {
            if (value <= 0)
            {
                throw new ArgumentException($"The {nameof(value)} can't be less than 0 or be 0!");
            }

            AccountBase sourceAccount = _accountRepository.GetByValue(sourceAccountNumber).ToAccountBase();

            AccountBase destinationAccount = _accountRepository.GetByValue(destinationAccountNumber).ToAccountBase();

            if (sourceAccount == null)
            {
                throw new InvalidAccountOperationException($"This account {sourceAccountNumber} doesn't exist!");
            }

            if (destinationAccount == null)
            {
                throw new InvalidAccountOperationException($"This account {destinationAccountNumber} doesn't exist!");
            }

            sourceAccount.Transfer(destinationAccount, value);

            _accountRepository.Update(sourceAccount.ToDalAccount());
        }
    }
}
