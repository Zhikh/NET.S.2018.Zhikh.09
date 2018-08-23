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
    public class PersonRepository : IPersonRepository
    {
        public void Create(DalPerson entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(DalPerson entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DalPerson> GetAll()
        {
            throw new NotImplementedException();
        }

        public DalPerson GetById(int id)
        {
            throw new NotImplementedException();
        }

        public DalPerson GetByPredicate(Expression<Func<DalPerson, bool>> func)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(DalPerson entity)
        {
            throw new NotImplementedException();
        }
    }
}
