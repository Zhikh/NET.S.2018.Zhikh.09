using System;
using System.Collections.Generic;
using System.Linq;
using Task2.BLL.Exceptions;
using Task2.BLL.Interface.Entities;
using Task2.BLL.Interface.Services;
using Task2.BLL.Mappers;
using Task2.DAL.Interface.Repositories;
using Task2.DAL.Interface.Strategies;

namespace Task2.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountNumberGenerator<int> _accountNumberGenerator;
        private readonly IPersonService _personService;

        public AccountService(IAccountRepository accountRepository, IAccountNumberGenerator<int> accountNumberGenerator, IPersonService personService)
        {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            _accountNumberGenerator = accountNumberGenerator ?? throw new ArgumentNullException(nameof(accountNumberGenerator));
            _personService = personService ?? throw new ArgumentNullException(nameof(personService));
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

        public AccountBase GetAccount(string AccountNumber)
        {
            if (string.IsNullOrEmpty(AccountNumber))
            {
                throw new ArgumentException("message", nameof(AccountNumber));
            }

            return _accountRepository.GetByValue(AccountNumber).ToAccountBase();
        }

        public IEnumerable<AccountBase> GetUserAccounts(Person person)
        {
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }

            IEnumerable<AccountBase> accounts = _accountRepository.GetAll().ToAccount();

            return accounts?.Where(a => a.Owner == person);
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

            _personService.Create(entity.Owner);

            entity.Number = _accountNumberGenerator.GenerateNumber(entity.GetHashCode());
            // TODO: add return value on create for checking
            _accountRepository.Create(entity.ToDalAccount());

            entity.Owner.Accounts.Add(entity);
            _personService.Update(entity.Owner);
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
