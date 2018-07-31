using Task2.BLL.Interface.Entities;

namespace Task2.BLL.Interface.Services
{
    public interface IAccountService
    {
        //IRepository<TEntity> _accountRepository { get; set; }

        void Open(AccountBase entity);

        void Deposit(string accounNumber, decimal value);

        void Withdraw(string accounNumber, decimal value);

        void Close(string accounNumber);
    }
}
