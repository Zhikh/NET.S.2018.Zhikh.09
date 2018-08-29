using System.Collections.Generic;
using Task2.BLL.Interface.Entities;

namespace Task2.BLL.Interface.Services
{
    public interface IAccountService
    {
        /// <summary>
        /// Returns all accounts 
        /// </summary>
        /// <returns> Collection of <see cref="Account"/> </returns>
        IEnumerable<Account> GetAll();

        /// <summary>
        /// Returns account with <paramref name="accountNumber"/>
        /// </summary>
        /// <param name="accountNumber"> Number of account </param>
        /// <returns> Entity of <see cref="Account"/></returns>
        Account GetAccount(string accountNumber);

        /// <summary>
        /// Returns a collection of <see cref="Account"/> objects
        /// </summary>
        /// <param name="owner"> Person for searching </param>
        /// <returns> A collection of <see cref="Account"/> objects </returns>
        IEnumerable<Account> GetUserAccounts(Person owner);

        /// <summary> Opens new account </summary>
        /// <param name="entity"> Entity of <see cref="Account"> </param>
        void Open(Account entity);

        /// <summary>
        /// It is the operation of account replenishment.
        /// </summary>
        /// <param name="accountNumber"> Number of account </param>
        /// <param name="value"> Amount of money </param>
        void Deposit(string accounNumber, decimal value);

        /// <summary>
        /// It is the operation of withdrawing money from the account.
        /// </summary>
        /// <param name="accountNumber"> Number of account </param>
        /// <param name="value"> Amount of money </param>
        void Withdraw(string accounNumber, decimal value);

        /// <summary>
        /// It is the operation of transfer of money from one account to another.
        /// </summary>
        /// <param name="sourceAccountNumber"> Number of source account </param>
        /// <param name="destinationAccountNumber"> Number of destination account </param>
        /// <param name="value"> Amount of money </param>
        void Transfer(string sourceAccountNumber, string destinationAccountNumber, decimal value);

        /// <summary>
        /// It is the operation of closing the account.
        /// </summary>
        /// <param name="accountNumber"> Number of account </param>
        void Close(string accounNumber);
    }
}
