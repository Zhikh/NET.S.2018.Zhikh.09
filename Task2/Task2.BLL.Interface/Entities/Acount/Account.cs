using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Task2.BLL.Exceptions;
using Task2.DAL.Interface.Strategies;

[assembly: InternalsVisibleTo("Task2.BLL")]

namespace Task2.BLL.Interface.Entities
{
    public sealed class Account
    {
        #region Private fields
        private Person _owner;
        private AccountType _accountType;
        #endregion

        #region Public API
        /// <summary>
        /// Initializes a new instance of the <see cref="Account" />.
        /// </summary>
        public Account()
        {
            InvoiceAmount = 0;
            Bonuses = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Account" />.
        /// </summary>
        /// <param name="strategy"> Strategy of generation of account nmber </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="strategy"/> is null.
        /// </exception>
        public Account(IAccountNumberGenerator<int> strategy) : this()
        {
            AccountNumberGenerator = strategy ?? throw new ArgumentNullException(nameof(strategy) + " can't be null!");
        }

        /// <summary>
        /// Strategy of generation of account nmber
        /// </summary>
        public IAccountNumberGenerator<int> AccountNumberGenerator { get; }

        /// <summary>
        /// Account number
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Owner of account
        /// </summary>
        public Person Owner
        {
            get
            {
                return _owner;
            }

            set
            {
                _owner = value ?? throw new ArgumentException("Owner can't be null!");
            }
        }

        /// <summary>
        /// Indicates whether the account is open or not.
        /// </summary>
        public bool IsOpen { get; set; }

        /// <summary>
        /// Balance of account
        /// </summary>
        public decimal InvoiceAmount { get; set; }

        /// <summary>
        /// Bonuses of account
        /// </summary>
        public int Bonuses { get; set; }

        /// <summary>
        /// Type of account
        /// </summary>
        public AccountType AccountType
        {
            get
            {
                return _accountType;
            }

            set
            {
                _accountType = value ?? throw new ArgumentException("Account type can't be null!");
            }
        }

        /// <summary>
        /// Adds <paramref name="value"/> to account balance
        /// </summary>
        /// <param name="value"> Deposit value </param>
        internal void Deposit(decimal value)
        {
            Bonuses += (int)(value / AccountType.WithdrawCost);
            InvoiceAmount += value;
        }

        /// <summary>
        /// Takes away <paramref name="value"/> from account balance
        /// </summary>
        /// <param name="value"> Withdraw value </param>
        internal void Withdraw(decimal value)
        {
            if (InvoiceAmount == 0)
            {
                throw new InvalidAccountOperationException("The invoice is empty!");
            }

            if (InvoiceAmount - value < 0)
            {
                throw new InvalidAccountOperationException("There is not enough money to perform the operation!");
            }

            Bonuses -= (int)(value / AccountType.DepositCost);
            InvoiceAmount -= value;
        }

        /// <summary>
        /// Transferes money from this account to another
        /// </summary>
        /// <param name="account"> Account for getting </param>
        /// <param name="value"> Amount of money for transfer </param>
        internal void Transfer(Account account, decimal value)
        {
            Withdraw(value);
            account.Deposit(value);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">  The object to compare with the current object. </param>
        /// <returns>  true if the specified object is equal to the current object; otherwise, false. </returns>
        public override bool Equals(object obj)
        {
            var account = obj as Account;
            return account != null &&
                   base.Equals(obj) &&
                   this.Number == account.Number &&
                   EqualityComparer<Person>.Default.Equals(this.Owner, account.Owner) &&
                   this.InvoiceAmount == account.InvoiceAmount &&
                   this.Bonuses == account.Bonuses &&
                   EqualityComparer<AccountType>.Default.Equals(this.AccountType, account.AccountType);
        }

        /// <summary>
        /// Finds hash code.
        /// </summary>
        /// <returns> A hash code for the current object. </returns>
        public override int GetHashCode()
        {
            var hashCode = -482127840;
            hashCode = (hashCode * -1521134295) + base.GetHashCode();
            hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(this.Number);
            hashCode = (hashCode * -1521134295) + EqualityComparer<Person>.Default.GetHashCode(this.Owner);
            hashCode = (hashCode * -1521134295) + this.InvoiceAmount.GetHashCode();
            hashCode = (hashCode * -1521134295) + this.Bonuses.GetHashCode();
            hashCode = (hashCode * -1521134295) + EqualityComparer<AccountType>.Default.GetHashCode(this.AccountType);
            return hashCode;
        }
        #endregion
    }
}
