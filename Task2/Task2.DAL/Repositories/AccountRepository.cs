using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Task2.DAL.Interface.Repositories;
using Task2.DAL.Interfaces.DTO;
using Task2.DAL.Mappers;
using Task2.ORM;

namespace Task2.DAL.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DbContext context;

        public AccountRepository(DbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Create(DalAccount entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            context.Set<Account>().Add(entity.ToAccount());
        }

        public void Delete(DalAccount entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var account = context.Set<Account>().Single(a => a.Id == entity.Id);

            context.Set<Account>().Remove(account);
        }

        public IEnumerable<DalAccount> GetAll()
        {
            return context.Set<Account>().ToDalAccounts();
        }

        public DalAccount GetById(int id)
        {
            return context.Set<Account>().FirstOrDefault(a => a.Id == id).ToDalAccount();
        }

        public DalAccount GetByPredicate(Expression<Func<DalAccount, bool>> predicate)
        {
            if (predicate == null)
            {
                return null;
            }

            IEnumerable<DalAccount> accounts = context.Set<Account>().ToDalAccounts();

            return accounts.FirstOrDefault(predicate);
        }

        public void Update(DalAccount entity)
        {
            var entityToUpdate = context.Set<Account>().First(e => e.Id == entity.Id);

            UpdateEntity(entityToUpdate, entity.ToAccount());
        }

        private bool UpdateEntity(Account entityToUpdate, Account updatedEntity)
        {
            if (entityToUpdate == null)
            {
                return false;
            }

            if (updatedEntity == null)
            {
                return false;
            }

            try
            {
                context.Entry(entityToUpdate).CurrentValues.SetValues(updatedEntity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
