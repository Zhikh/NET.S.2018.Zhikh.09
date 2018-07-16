﻿using System;
using System.Collections.Generic;
using Core.Task2.Strategies;

namespace Core.Task2.Entities
{
    public sealed class Account
    {
        private Person _owner;
        private string _number;
        private AccountType _accountType;
        private static int _id = 0;

        public Account(IAccountNumberGenerator _strategy)
        {
            AccountNumberGenerator = _strategy;
            Id = _id++;

            Number = AccountNumberGenerator.GenerateNumber(Id);
        }

        public IAccountNumberGenerator AccountNumberGenerator { get; set; }

        public int Id { get; private set; }

        public string Number { get;}

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