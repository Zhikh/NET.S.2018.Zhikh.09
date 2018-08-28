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
    public class PersonRepository : IPersonRepository
    {
        private readonly DbContext context;

        public PersonRepository(DbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Create(DalPerson entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            context.Set<Person>().Add(entity.ToPerson());
        }

        public void Delete(DalPerson entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var person = context.Set<Person>().Single(u => u.Id == entity.Id);

            context.Set<Person>().Remove(person);
        }

        public IEnumerable<DalPerson> GetAll()
        {
            return context.Set<Person>().ToDalPersons();
        }

        public DalPerson GetById(int id)
        {
            return context.Set<Person>().FirstOrDefault(person => person.Id == id).ToDalPerson();
        }

        public DalPerson GetByPredicate(Func<DalPerson, bool> predicate)
        {
            if (predicate == null)
            {
                return null;
            }

            IEnumerable<DalPerson> persons = context.Set<Person>().ToDalPersons();

            return persons.FirstOrDefault(predicate);
        }

        public void Update(DalPerson entity)
        {
            var entityToUpdate = context.Set<Person>()?.First(e => e.Id == entity.Id);

            UpdateEntity(entityToUpdate, entity.ToPerson());
        }

        private bool UpdateEntity(Person entityToUpdate, Person updatedEntity)
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
