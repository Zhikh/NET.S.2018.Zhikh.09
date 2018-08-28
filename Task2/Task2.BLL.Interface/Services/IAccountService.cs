﻿using System.Collections.Generic;
using Task2.BLL.Interface.Entities;

namespace Task2.BLL.Interface.Services
{
    public interface IAccountService
    {
        IEnumerable<Account> GetAll();

        Account GetAccount(string accountNumber);

        IEnumerable<Account> GetUserAccounts(Person owner);

        void Open(Account entity);

        void Deposit(string accounNumber, decimal value);

        void Withdraw(string accounNumber, decimal value);

        void Transfer(string sourceAccountNumber, string destinationAccountNumber, decimal value);

        void Close(string accounNumber);
    }
}
