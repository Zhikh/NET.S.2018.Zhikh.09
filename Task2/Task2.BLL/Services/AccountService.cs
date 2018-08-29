using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Task2.BLL.Exceptions;
using Task2.BLL.Interface;
using Task2.BLL.Interface.Entities;
using Task2.BLL.Interface.Services;
using Task2.BLL.Mappers;
using Task2.DAL.Interface;
using Task2.DAL.Interface.Repositories;

namespace Task2.BLL.Services
{
    public class AccountService : IAccountService
    {
        #region Private fields
        private static readonly object _syncRoot = new object();
        private static volatile AccountService _instance;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountRepository _accountRepository;
        private readonly IPersonService _personService;
        private readonly ILogger _logger;
        #endregion

        #region Constructors
        private AccountService(IUnitOfWork unitOfWork, IAccountRepository accountRepository, IPersonService personService)
        {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _personService = personService ?? throw new ArgumentNullException(nameof(personService));

            _logger = new Logger();
        }
        #endregion

        #region Public API
        /// <summary>
        /// Returns an object of <see cref="Account"/>
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
        public static AccountService GetInstance(
            IUnitOfWork unitOfWork = null,
            IAccountRepository accountRepository = null,
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

        /// <summary> Opens new account </summary>
        /// <param name="account"> Entity of <see cref="Account"> </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="entity"/> is null.
        /// </exception>
        public void Open(Account entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Account can't be null!");
            }

            try
            {
                _personService.Create(entity.Owner);
                _accountRepository.Create(entity.ToDalAccount());

                entity.Owner.Accounts.Add(entity);
                _personService.Update(entity.Owner);

                _unitOfWork.SaveChanges();
            }
            catch (SqlException ex)
            {
                _logger.LogFatal(ex.Message, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
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

            Account account = _accountRepository.GetByPredicate(a => a.Number == accountNumber).ToAccountBase();

            if (account == null)
            {
                throw new InvalidAccountOperationException($"This account {accountNumber} doesn't exist!");
            }

            try
            {
                account.Deposit(value);

                _accountRepository.Update(account.ToDalAccount());
                _unitOfWork.SaveChanges();
            }
            catch (SqlException ex)
            {
                _logger.LogFatal(ex.Message, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
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

            Account account = _accountRepository.
                GetByPredicate(a => a.Number == accountNumber).ToAccountBase();

            if (account == null)
            {
                throw new InvalidAccountOperationException($"This account {accountNumber} doesn't exist!");
            }

            try
            {
                account.Withdraw(value);

                _accountRepository.Update(account.ToDalAccount());
                _unitOfWork.SaveChanges();
            }
            catch (SqlException ex)
            {
                _logger.LogFatal(ex.Message, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
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

            Account sourceAccount = _accountRepository.
                GetByPredicate(a => a.Number == sourceAccountNumber).ToAccountBase();
            Account destinationAccount = _accountRepository.
                GetByPredicate(a => a.Number == destinationAccountNumber).ToAccountBase();

            if (sourceAccount == null)
            {
                throw new InvalidAccountOperationException($"This account {sourceAccountNumber} doesn't exist!");
            }

            if (destinationAccount == null)
            {
                throw new InvalidAccountOperationException($"This account {destinationAccountNumber} doesn't exist!");
            }

            try
            {
                sourceAccount.Transfer(destinationAccount, value);

                _accountRepository.Update(sourceAccount.ToDalAccount());
                _accountRepository.Update(destinationAccount.ToDalAccount());
                _unitOfWork.SaveChanges();
            }
            catch (SqlException ex)
            {
                _logger.LogFatal(ex.Message, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        }

        /// <summary>
        /// It is the operation of closing the account.
        /// </summary>
        /// <param name="accountNumber"> Number of account </param>
        /// <exception cref="ArgumentException">
        ///     <paramref name="accountNumber"/> is null or empty.
        /// </exception>
        /// <exception cref="InvalidAccountOperationException">
        ///     If account with <paramref name="accountNumber"/> doesn't exist.
        /// </exception>
        public void Close(string accountNumber)
        {
            if (string.IsNullOrEmpty(accountNumber))
            {
                throw new ArgumentException("Account number can't be null or empty!");
            }
            
            Account account = _accountRepository.GetByPredicate(a => a.Number == accountNumber).ToAccountBase();

            if (account == null)
            {
                throw new InvalidAccountOperationException($"This account {accountNumber} doesn't exist!");
            }

            try
            {
                _accountRepository.Delete(account.ToDalAccount());
                _unitOfWork.SaveChanges();
            }
            catch (SqlException ex)
            {
                _logger.LogFatal(ex.Message, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        }

        /// <summary>
        /// Returns account with <paramref name="accountNumber"/>
        /// </summary>
        /// <param name="accountNumber"> Number of account </param>
        /// <returns> Entity of <see cref="Account"/></returns>
        /// <exception cref="ArgumentException">
        ///     <paramref name="accountNumber"/> is null or empty.
        /// </exception>
        public Account GetAccount(string accountNumber)
        {
            if (string.IsNullOrEmpty(accountNumber))
            {
                throw new ArgumentException("The parameter {0} can't be null or empty!", nameof(accountNumber));
            }

            var result = new Account();
            try
            {
                result = _accountRepository.GetByPredicate(a => a.Number == accountNumber).ToAccountBase();
            }
            catch (SqlException ex)
            {
                _logger.LogFatal(ex.Message, ex);
            }

            return result;
        }

        /// <summary>
        /// Returns a collection of <see cref="Account"/> objects
        /// </summary>
        /// <returns> A collection of <see cref="Account"/> objects </returns>
        public IEnumerable<Account> GetAll()
        {
            IEnumerable<Account> result = null;
            try
            {
                result = _accountRepository.GetAll().ToAccount();
            }
            catch (SqlException ex)
            {
                _logger.LogFatal(ex.Message, ex);
            }

            return result;
        }

        /// <summary>
        /// Returns a collection of <see cref="Account"/> objects
        /// </summary>
        /// <param name="person"> Person for searching </param>
        /// <returns> A collection of <see cref="Account"/> objects </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="person"/> is null.
        /// </exception>
        public IEnumerable<Account> GetUserAccounts(Person person)
        {
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }
            
            IEnumerable<Account> result = null;
            try
            {
                IEnumerable<Account> accounts = _accountRepository.GetAll().ToAccount();
                result = accounts.Where(a => a.Owner == person);
            }
            catch (SqlException ex)
            {
                _logger.LogFatal(ex.Message, ex);
            }

            return result;
        }
        #endregion
    }
}
