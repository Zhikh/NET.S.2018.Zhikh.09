using DAL.Task2.Repositories;

namespace Logic.Task2
{
    public interface IAccountService<TEntity> where TEntity :  class
    {
        IRepository<TEntity> _accountRepository { get; set; }

        void Open(TEntity entity);

        void Deposit(string accounNumber, decimal value);

        void Withdraw(string accounNumber, decimal value);

        void Close(string accounNumber);
    }
}
