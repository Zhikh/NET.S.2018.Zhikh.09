using System;
using System.Collections.Generic;

namespace Logic.Task2
{
    public sealed class Account: BaseEntity
    {
        private Person _owner;
        private string _number;
        private AccountType _accountType;

        public string Number
        {
            get
            {
                return _number;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Number can't be null or empty!");
                }

                _number = value;
            }
        }

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

        public override bool Equals(object obj)
        {
            var account = obj as Account;
            return account != null &&
                   base.Equals(obj) &&
                   Number == account.Number &&
                   EqualityComparer<Person>.Default.Equals(Owner, account.Owner) &&
                   InvoiceAmount == account.InvoiceAmount &&
                   Bonuses == account.Bonuses &&
                   EqualityComparer<AccountType>.Default.Equals(AccountType, account.AccountType);
        }

        public override int GetHashCode()
        {
            var hashCode = -482127840;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Number);
            hashCode = hashCode * -1521134295 + EqualityComparer<Person>.Default.GetHashCode(Owner);
            hashCode = hashCode * -1521134295 + InvoiceAmount.GetHashCode();
            hashCode = hashCode * -1521134295 + Bonuses.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<AccountType>.Default.GetHashCode(AccountType);
            return hashCode;
        }
    }
}
