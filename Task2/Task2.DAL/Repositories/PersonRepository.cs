using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
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

            var user = new Person()
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                MiddleName = entity.SecondName,
                ContactData = new ContactData()
                {
                    Email = entity.Contact.Email
                },
                Passport = new Passport()
                {
                    PassportSeries = entity.SerialNumber
                }
            };

            context.Set<Person>().Add(user);
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
            return context.Set<Person>().Select(person => new DalPerson()
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                SecondName = person.MiddleName,
                SerialNumber = person.Passport.Number,
                Contact = new DalContactData
                {
                    Email = person.ContactData.Email
                }
            });
        }

        public DalPerson GetById(int id)
        {
            return context.Set<Person>().FirstOrDefault(person => person.Id == id).ToDalPerson(); ;
        }

        public DalPerson GetByPredicate(Expression<Func<DalPerson, bool>> predicate)
        {
            if (predicate == null) return null;

            return context.Set<Person>().SingleOrDefault(predicate);
        }

        public void Update(DalPerson entity)
        {
            var entityToUpdate = context.Set<Person>().First(e => e.Id == entity.Id);
            return UpdateEntity(entityToUpdate, entity);
        }

        protected bool UpdateEntity(Person entityToUpdate, Person updatedEntity)
        {
            if (entityToUpdate == null) return false;
            if (updatedEntity == null) return false;

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
