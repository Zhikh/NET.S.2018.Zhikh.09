using System;
using System.Collections.Generic;
using Core.Task2.Entities;

namespace DAL.Task2.Repositories
{
    public class FakeAccountRepository : IRepository<Account>
    {
        private ICollection<Account> Entities { get; set; }

        public FakeAccountRepository()
        {
            Entities = new List<Account>();
        }

        /// <summary>
        /// Creates new account
        /// </summary>
        /// <param name="entity"> Account entity </param>
        public void Create(Account entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity can't be null!");
            }

            if (string.IsNullOrEmpty(entity.Number) || entity.Owner == null ||
                entity.AccountType == null)
            {
                throw new ArgumentException("Entity account has unfilled field!");
            }

            if (Entities.FindFirst(entity) != null)
            {
                throw new ArgumentException("This account already exists!");
            }
            
            Entities.Add(entity);
        }

        /// <summary>
        /// Delete account by id
        /// </summary>
        /// <param name="id"> Account id </param>
        public void Delete(int id)
        {
            Account entity = GetById(id);
            if (GetById(id) == null)
            {
                throw new ArgumentException("This account doesn't exist!");
            }

            Entities.Remove(entity);
        }

        /// <summary>
        /// Gets all accounts
        /// </summary>
        /// <returns> Accounts </returns>
        public ICollection<Account> GetAll() => Entities;

        public Account GetById(int id)
        {
            foreach (var entity in Entities)
            {
                if (entity.Id == id)
                {
                    return entity;
                }
            }

            return null;
        }

        /// <summary>
        /// Get account by its number
        /// </summary>
        /// <param name="value"> Number of account </param>
        /// <returns> Account </returns>
        public Account GetByValue(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("Value can't be null or empty!");
            }

            foreach (var entity in Entities)
            {
                if (entity.Number == value)
                {
                    return entity;
                }
            }

            return null;
        }

        /// <summary>
        /// Update account
        /// </summary>
        /// <param name="entity"> Update entity </param>
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

            if (entity.AccountType != null)
            {
                account.AccountType = entity.AccountType;
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
