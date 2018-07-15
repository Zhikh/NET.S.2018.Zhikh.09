using System;
using System.Collections.Generic;

namespace Logic.Task2
{
    public class AccountService : IService<Account>
    {
        private DataProvider _provider;
        private static int _id;

        public AccountService()
        {
            _provider = DataProvider.Instance;
            _id = 0;
        }

        public void Create(Account entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity can't be null!");
            }

            if (string.IsNullOrEmpty(entity.Number) || entity.Owner == null ||
                entity.BillType == null)
            {
                throw new ArgumentException("Entity account has unfilled field!");
            }

            if (_provider.Accounts.FindFirst(entity) != null)
            {
                throw new ArgumentException("This account already exists!");
            }

            entity.Id = _id++;
            _provider.Accounts.Add(entity);
        }

        public void Delete(int id)
        {
            Account entity = GetById(id);
            if (GetById(id) == null)
            {
                throw new ArgumentException("This account doesn't exist!");
            }

            _provider.Accounts.Remove(entity);
        }

        public ICollection<Account> GetAll() => _provider.Accounts;

        public Account GetById(int id)
        {
            foreach(var entity in _provider.Accounts)
            {
                if (entity.Id == id)
                {
                    return entity;
                }
            }

            return null;
        }

        public Account GetByValue(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("Value can't be null or empty!");
            }

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
            if (entity == null)
            {
                throw new ArgumentNullException("Entity can't be null!");
            }

            Account account = GetByValue(entity.Number);

            if (account == null)
            {
                throw new ArgumentException("This account doesn't exist!");
            }

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
