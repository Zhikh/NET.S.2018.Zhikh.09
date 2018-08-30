using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Task2.DAL.Interface.Repositories;
using Task2.DAL.Interfaces.DTO;
using Task2.DAL.Mappers;
using Task2.ORM;

namespace Task2.DAL.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        #region Fields
        private readonly DbContext context;
        #endregion

        #region Public API
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountRepository" />.
        /// </summary>
        /// <param name="context"> The instance of the <see cref="DbContext"/> class </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="context"/> is null.
        /// </exception>
        public AccountRepository(DbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Adds entity of the <see cref="DalAccount"/> class to context
        /// </summary>
        /// <param name="entity"> Entity for saving </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="entity"/> is null.
        /// </exception>
        public void Create(DalAccount entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            context.Set<Account>().Add(entity.ToAccount());
        }

        /// <summary>
        /// Removes entity of the <see cref="DalAccount"/> class from context
        /// </summary>
        /// <param name="entity"> Entity for removing </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="entity"/> is null.
        /// </exception>
        public void Delete(DalAccount entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var account = context.Set<Account>().Single(a => a.Id == entity.Id);

            account.IsOpen = false;
        }

        /// <summary>
        /// Returns a collection of <see cref="DalAccount"/> objects
        /// </summary>
        /// <returns> A collection of <see cref="DalAccount"/> objects </returns>
        public IEnumerable<DalAccount> GetAll()
        {
            return context.Set<Account>().ToDalAccounts();
        }

        /// <summary>
        /// Returns account with <paramref name="id"/>
        /// </summary>
        /// <param name="id"> Id of account </param>
        /// <returns> Entity of the <see cref="DalAccount"/> class </returns>
        public DalAccount GetById(int id)
        {
            return context.Set<Account>().FirstOrDefault(a => a.Id == id).ToDalAccount();
        }

        /// <summary>
        /// Returns entity of <see cref="DalAccount"/> finding by <paramref name="predicate"/>
        /// </summary>
        /// <param name="predicate"> Rule for searching </param>
        /// <returns> Entity of the <see cref="DalAccount"/> class </returns>
        public DalAccount GetByPredicate(Func<DalAccount, bool> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            IEnumerable<DalAccount> accounts = context.Set<Account>().ToDalAccounts();

            return accounts.FirstOrDefault(predicate);
        }

        /// <summary>
        /// Updates account by values from entity of the <see cref="DalAccount"/> class
        /// </summary>
        /// <param name="entity"> Entity of the <see cref="DalAccount"/> class </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="entity"/> is null.
        /// </exception>
        public bool Update(DalAccount entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            Account entityToUpdate = context.Set<Account>().First(e => e.Id == entity.Id);

            if (entityToUpdate == null)
            {
                return false;
            }

            try
            {
                context.Entry(entityToUpdate).CurrentValues.SetValues(entity.ToAccount());
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
    }
}
