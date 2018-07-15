using System;
using System.Collections.Generic;

namespace Logic.Task2
{
    public sealed class Account
    {
        private Person _owner;
        private string _number;
        private AccountType _accountType;

        public int Id { get; set; }

        public string Number
        {
            get
            {
                return this._number;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Number can't be null or empty!");
                }

                this._number = value;
            }
        }

        public Person Owner
        {
            get
            {
                return this._owner;
            }

            set
            {
                this._owner = value ?? throw new ArgumentException("Owner can't be null!");
            }
        }

        public decimal InvoiceAmount { get; set; }

        public int Bonuses { get; set; }

        public AccountType AccountType
        {
            get
            {
                return this._accountType;
            }

            set
            {
                this._accountType = value ?? throw new ArgumentException("Account type can't be null!");
            }
        }

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
