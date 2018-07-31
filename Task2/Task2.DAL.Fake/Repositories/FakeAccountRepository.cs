using System;
using Task2.DAL.Interface.Repositories;
using Task2.DAL.Interfaces.DTO;

namespace DAL.Task2.Repositories
{
    public class FakeAccountRepository : BaseRepository<DalAccount>, IAccountRepository
    {
        internal sealed override DalAccount FindEntity(string value)
        {
            foreach (var entity in Entities)
            {
                if (entity.Number == value)
                {
                    return entity;
                }
            }

            return null;
        }

        internal sealed override void Update(DalAccount entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity can't be null!");
            }

            DalAccount account = GetByValue(entity.Number);

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

        internal sealed override bool IsInvalid(DalAccount entity) 
            => string.IsNullOrEmpty(entity.Number) || entity.Owner == null ||
               entity.AccountType == null;
    }
}
