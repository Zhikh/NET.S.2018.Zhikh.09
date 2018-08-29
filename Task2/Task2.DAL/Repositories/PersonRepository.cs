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
    public class PersonRepository : IPersonRepository
    {
        #region Fields
        private readonly DbContext context;
        #endregion

        #region Public API
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonRepository" />.
        /// </summary>
        /// <param name="context"> The instance of the <see cref="DbContext"/> class </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="context"/> is null.
        /// </exception>
        public PersonRepository(DbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Adds entity of the <see cref="DalPerson"/> class to context
        /// </summary>
        /// <param name="entity"> Entity for saving </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="entity"/> is null.
        /// </exception>
        public void Create(DalPerson entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            context.Set<Person>().Add(entity.ToPerson());
        }

        /// <summary>
        /// Removes entity of the <see cref="DalPerson"/> class from context
        /// </summary>
        /// <param name="entity"> Entity for removing </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="entity"/> is null.
        /// </exception>
        public void Delete(DalPerson entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var person = context.Set<Person>().Single(u => u.Id == entity.Id);

            context.Set<Person>().Remove(person);
        }

        /// <summary>
        /// Returns a collection of <see cref="DalPerson"/> objects
        /// </summary>
        /// <returns> A collection of <see cref="DalPerson"/> objects </returns>
        public IEnumerable<DalPerson> GetAll()
        {
            return context.Set<Person>().ToDalPersons();
        }

        /// <summary>
        /// Returns person with <paramref name="id"/>
        /// </summary>
        /// <param name="id"> Id of account </param>
        /// <returns> Entity of the <see cref="DalPerson"/> class </returns>
        public DalPerson GetById(int id)
        {
            return context.Set<Person>().FirstOrDefault(person => person.Id == id).ToDalPerson();
        }

        /// <summary>
        /// Returns entity of <see cref="DalPerson"/> finding by <paramref name="predicate"/>
        /// </summary>
        /// <param name="predicate"> Rule for searching </param>
        /// <returns> Entity of the <see cref="DalPerson"/> class </returns>
        public DalPerson GetByPredicate(Func<DalPerson, bool> predicate)
        {
            if (predicate == null)
            {
                return null;
            }

            IEnumerable<DalPerson> persons = context.Set<Person>().ToDalPersons();

            return persons.FirstOrDefault(predicate);
        }

        /// <summary>
        /// Updates person by values from entity of the <see cref="DalPerson"/> class
        /// </summary>
        /// <param name="entity">  Entity of the <see cref="DalPerson"/> class </param>
        public void Update(DalPerson entity)
        {
            var entityToUpdate = context.Set<Person>().First(e => e.Id == entity.Id);

            UpdateEntity(entityToUpdate, entity.ToPerson());
        }
        #endregion

        #region Additional methods
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
        #endregion
    }
}
