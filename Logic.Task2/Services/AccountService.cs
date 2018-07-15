using System;

namespace Logic.Task2
{
    public class AccountService : IService<Account>
    {
        private DataProvider _provider;

        public AccountService()
        {
            _provider = DataProvider.Instance;
        }

        public void Create(Account entity)
        {
            if (_provider.Accounts.FindFirst(entity) != null)
            {
                throw new ArgumentException("This account already exists!");
            }

            _provider.Accounts.Add(entity);
        }

        public void Delete(Account entity)
        {
            if (_provider.Accounts.FindFirst(entity) == null)
            {
                throw new ArgumentException("This account doesn't exist!");
            }

            _provider.Accounts.Remove(entity);
        }

        public Account GetByValue(string value)
        {
            foreach (var element in _provider.Accounts)
            {
                if (element.Number == value)
                {
                    return element;
                }
            }

            return null;
        }

        public void Update(Account entity)
        {
            Account account = GetByValue(entity.Number);

            if (entity.BillHistory != null)
            {
                account.BillHistory = entity.BillHistory;
            }
            if (entity.BillType != null)
            {
                account.BillType = entity.BillType;
            }
            if (entity.Bonuses != 0)
            {
                account.Bonuses = entity.Bonuses;
            }
            if (entity.InvoiceAmount != 0)
            {
                account.InvoiceAmount = entity.InvoiceAmount;
            }
        }
    }
}
