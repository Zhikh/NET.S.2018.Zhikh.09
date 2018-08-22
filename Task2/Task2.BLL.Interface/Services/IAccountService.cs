using System.Collections.Generic;
using Task2.BLL.Interface.Entities;

namespace Task2.BLL.Interface.Services
{
    public interface IAccountService
    {
        AccountBase GetAccount(string accountNumber);

        IEnumerable<AccountBase> GetUserAccounts(Person owner);

        void Open(AccountBase entity);

        void Deposit(string accounNumber, decimal value);

        void Withdraw(string accounNumber, decimal value);

        void Close(string accounNumber);
    }
}
