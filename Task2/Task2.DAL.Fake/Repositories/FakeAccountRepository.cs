using System;
using Task2.DAL.Interface.Repositories;
using Task2.DAL.Interfaces.DTO;

namespace DAL.Task2.Repositories
{
    public class FakeAccountRepository : BaseRepository<DalAccount>, IAccountRepository
    {
        #region Internal methods
        internal sealed override DalAccount FindEntity(string number)
        {
            if (string.IsNullOrEmpty(number))
            {
                throw new ArgumentException("Value can't be null or empty!", nameof(number));
            }

            foreach (var entity in Entities)
            {
                if (entity.Number == number)
                {
                    return entity;
                }
            }

            return null;
        }

        internal sealed override bool Update(DalAccount entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity can't be null!");
            }

            DalAccount account = GetByValue(entity.Number);

            if (account == null)
            {
                return false;
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

            return true;
        }

        internal sealed override bool IsInvalid(DalAccount entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return string.IsNullOrEmpty(entity.Number) || entity.Owner == null ||
                          entity.AccountType == null;
        }
        #endregion
    }
}
