using System;
using System.Collections.Generic;
using Task2.BLL.Exceptions;
using Task2.DAL.Interface.Strategies;

namespace Task2.BLL.Interface.Entities
{
    public sealed class AccountBase
    {
        private Person _owner;
        private AccountType _accountType;
        private string _number;

        public AccountBase(IAccountNumberGenerator<int> strategy)
        {
            AccountNumberGenerator = strategy ?? throw new ArgumentNullException(nameof(strategy) + " can't be null!");
            
            InvoiceAmount = 0;
            Bonuses = 0;
        }

        public IAccountNumberGenerator<int> AccountNumberGenerator { get; }

        public string Number { get; private set; }

        public Person Owner
        {
            get
            {
                return _owner;
            }

            set
            {
                _owner = value ?? throw new ArgumentException("Owner can't be null!");

                Number = AccountNumberGenerator.GenerateNumber(Owner.GetHashCode());
            }
        }

        public decimal InvoiceAmount { get; set; }

        public int Bonuses { get; set; }

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

        public void Deposit(decimal value)
        {
            Bonuses += (int)(value / AccountType.ReplenishmentCost);
            InvoiceAmount += value;
        }

        public void Withdraw(decimal value)
        {
            if (InvoiceAmount == 0)
            {
                throw new InvalidAccountOperationException("The invoice is empty!");
            }

            if (InvoiceAmount - value < 0)
            {
                throw new InvalidAccountOperationException("There is not enough money to perform the operation!");
            }

            Bonuses -= (int)(value / AccountType.BalanceCost);
            InvoiceAmount -= value;
        }

        public void Transfer(AccountBase account, decimal value)
        {
            Withdraw(value);
            account.Deposit(value);
        }

        public override bool Equals(object obj)
        {
            var account = obj as AccountBase;
            return account != null &&
                   base.Equals(obj) &&
                   this.Number == account.Number &&
                   EqualityComparer<Person>.Default.Equals(this.Owner, account.Owner) &&
                   this.InvoiceAmount == account.InvoiceAmount &&
                   this.Bonuses == account.Bonuses &&
                   EqualityComparer<AccountType>.Default.Equals(this.AccountType, account.AccountType);
        }

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
    }
}
