using System;
using System.Collections.Generic;
using System.Linq;
using Task2.BLL.Exceptions;
using Task2.BLL.Interface.Entities;
using Task2.BLL.Interface.Services;
using Task2.BLL.Mappers;
using Task2.DAL.Interface.Repositories;

namespace Task2.BLL.Services
{
    public class AccountService : IAccountService
    {
        #region Private fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountRepository _accountRepository;
        private readonly IPersonService _personService;
        private static readonly object _syncRoot = new object();
        private static volatile AccountService _instance;
        #endregion

        #region Constructors
        private AccountService(IUnitOfWork unitOfWork, IAccountRepository accountRepository, IPersonService personService)
        {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _personService = personService ?? throw new ArgumentNullException(nameof(personService));
        }
        #endregion

        #region Public API
        /// <summary>
        /// Opens account
        /// </summary>
        /// <param name="account"> Entity of <see cref="AccountBase"> </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="entity"/> is null.
        /// </exception>
        public void Open(AccountBase entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Account can't be null!");
            }

            _personService.Create(entity.Owner);
            _accountRepository.Create(entity.ToDalAccount());

            entity.Owner.Accounts.Add(entity);
            _personService.Update(entity.Owner);

            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// It is the operation of account replenishment.
        /// </summary>
        /// <param name="accountNumber"> Number of account </param>
        /// <param name="value"> Amount of money </param>
        /// <exception cref="ArgumentException">
        ///     <paramref name="value"/> is less than zero.
        ///     If account with <paramref name="accountNumber"/> doesn't exist.
        ///     <paramref name="accountNumber"/> is null or empty.
        /// </exception>
        /// <exception cref="InvalidAccountOperationException">
        ///     If account with <paramref name="accountNumber"/> doesn't exist.
        /// </exception>
        public void Deposit(string accountNumber, decimal value)
        {
            if (string.IsNullOrEmpty(accountNumber))
            {
                throw new ArgumentException("The parameter {0} can't be null or empty!", nameof(accountNumber));
            }

            if (value <= 0)
            {
                throw new ArgumentException($"The {nameof(value)} can't be less than 0 or be 0!");
            }

            AccountBase account = _accountRepository.GetByPredicate(a => a.Number == accountNumber).ToAccountBase();

            if (account == null)
            {
                throw new InvalidAccountOperationException($"This account {accountNumber} doesn't exist!");
            }

            account.Deposit(value);

            _accountRepository.Update(account.ToDalAccount());
            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// It is the operation of withdrawing money from the account.
        /// </summary>
        /// <param name="accountNumber"> Number of account </param>
        /// <param name="value"> Amount of money </param>
        /// <exception cref="ArgumentException">
        ///     <paramref name="value"/> is less than zero.
        ///     <paramref name="accountNumber"/> is null or empty.
        /// </exception>
        /// <exception cref="InvalidAccountOperationException">
        ///     If account with <paramref name="accountNumber"/> doesn't exist.
        /// </exception>
        public void Withdraw(string accountNumber, decimal value)
        {
            if (string.IsNullOrEmpty(accountNumber))
            {
                throw new ArgumentException("The parameter {0} can't be null or empty!", nameof(accountNumber));
            }

            if (value <= 0)
            {
                throw new ArgumentException($"The {nameof(value)} can't be less than 0 or be 0!");
            }

            AccountBase account = _accountRepository.
                GetByPredicate(a => a.Number == accountNumber).ToAccountBase();

            if (account == null)
            {
                throw new InvalidAccountOperationException($"This account {accountNumber} doesn't exist!");
            }

            account.Withdraw(value);

            _accountRepository.Update(account.ToDalAccount());
            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// It is the operation of transfer of money from one account to another.
        /// </summary>
        /// <param name="sourceAccountNumber"> Number of source account </param>
        /// <param name="destinationAccountNumber"> Number of destination account </param>
        /// <param name="value"> Amount of money </param>
        /// <exception cref="ArgumentException">
        ///     <paramref name="value"/> is less than zero.
        ///     <paramref name="accountNumber"/> is null or empty.
        /// </exception>
        /// <exception cref="InvalidAccountOperationException">
        ///     If account with <paramref name="sourceAccountNumber"/> doesn't exist.
        ///     If account with <paramref name="destinationAccountNumber"/> doesn't exist.
        /// </exception>
        public void Transfer(string sourceAccountNumber, string destinationAccountNumber, decimal value)
        {
            if (string.IsNullOrEmpty(sourceAccountNumber))
            {
                throw new ArgumentException("The parameter {0} can't be null or empty!", nameof(sourceAccountNumber));
            }

            if (string.IsNullOrEmpty(destinationAccountNumber))
            {
                throw new ArgumentException("The parameter {0} can't be null or empty!", nameof(destinationAccountNumber));
            }

            if (value <= 0)
            {
                throw new ArgumentException($"The {nameof(value)} can't be less than 0 or be 0!");
            }

            AccountBase sourceAccount = _accountRepository.
                GetByPredicate(a => a.Number == sourceAccountNumber).ToAccountBase();
            AccountBase destinationAccount = _accountRepository.
                GetByPredicate(a => a.Number == destinationAccountNumber).ToAccountBase();

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
            _accountRepository.Update(destinationAccount.ToDalAccount());
            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// It is the operation of closing the account.
        /// </summary>
        /// <param name="accountNumber"> Number of account </param>
        /// <exception cref="ArgumentException">
        ///     <paramref name="accountNumber"/> is null or empty.
        /// </exception>
        /// <exception cref="InvalidAccountOperationException">
        ///     If account witn <paramref name="accountNumber"/> doesn't exist.
        /// </exception>
        public void Close(string accountNumber)
        {
            if (string.IsNullOrEmpty(accountNumber))
            {
                throw new ArgumentException("Account number can't be null or empty!");
            }
            
            AccountBase account = _accountRepository.GetByPredicate(a => a.Number == accountNumber).ToAccountBase();

            if (account == null)
            {
                throw new InvalidAccountOperationException($"This account {accountNumber} doesn't exist!");
            }
            
            _accountRepository.Delete(account.ToDalAccount());
            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Returns account with <paramref name="accountNumber"/>
        /// </summary>
        /// <param name="accountNumber"> Number of account </param>
        /// <returns> Entity of <see cref="AccountBase"/></returns>
        /// <exception cref="ArgumentException">
        ///     <paramref name="accountNumber"/> is null or empty.
        /// </exception>
        public AccountBase GetAccount(string accountNumber)
        {
            if (string.IsNullOrEmpty(accountNumber))
            {
                throw new ArgumentException("The parameter {0} can't be null or empty!", nameof(accountNumber));
            }

            return _accountRepository.GetByPredicate(a => a.Number == accountNumber).ToAccountBase();
        }

        /// <summary>
        /// Returns a collection of <see cref="AccountBase"/> objects
        /// </summary>
        /// <param name="person"> Person for searching </param>
        /// <returns> A collection of <see cref="AccountBase"/> objects </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="person"/> is null.
        /// </exception>
        public IEnumerable<AccountBase> GetUserAccounts(Person person)
        {
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }

            IEnumerable<AccountBase> accounts = _accountRepository.GetAll().ToAccount();

            return accounts.Where(a => a.Owner == person);
        }

        /// <summary>
        /// Returns an object of <see cref="AccountBase"/>
        /// </summary>
        /// <param name="unitOfWork"> Object of <see cref="IUnitOfWork"> </param>
        /// <param name="accountRepository"> Object of <see cref="IAccountService"> </param>
        /// <param name="personService"> Object of <see cref="IPersonService"> </param>
        /// <returns>  A <see cref="PersonService" /> object </returns>
        /// <exception cref="ArgumentNullException">
        ///      <paramref name="unitOfWork"/> is null.
        ///      <paramref name="accountRepository"/> is null.
        ///      <paramref name="personService"/> is null.
        /// </exception>
        /// <remarks>
        /// If instance of <see cref="AccountService"> was created, parameters will not play any roll.
        /// </remarks>
        public static AccountService Instance(IUnitOfWork unitOfWork = null, IAccountRepository accountRepository = null, 
            IPersonService personService = null)
        {
            if (_instance == null)
            {
                lock (_syncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new AccountService(unitOfWork, accountRepository, personService);
                    }
                }
            }

            return _instance;
        }
        #endregion
    }
}
