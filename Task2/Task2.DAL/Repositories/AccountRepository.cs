using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Task2.DAL.Interface.Repositories;
using Task2.DAL.Interfaces.DTO;

namespace Task2.DAL.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public void Create(DalAccount entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(DalAccount entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DalAccount> GetAll()
        {
            throw new NotImplementedException();
        }

        public DalAccount GetById(int id)
        {
            throw new NotImplementedException();
        }

        public DalAccount GetByPredicate(Expression<Func<DalAccount, bool>> func)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(DalAccount entity)
        {
            throw new NotImplementedException();
        }
    }
}
