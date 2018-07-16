using System;
using System.Collections.Generic;
using Core.Task2.Entities;

namespace DAL.Task2.Repositories
{
    public class FakePersonRepository : IRepository<Person>
    {
        private ICollection<Person> Entities { get; set; }

        public FakePersonRepository()
        {
            Entities = new List<Person>();
        }

        #region Public API
        /// <summary>
        /// Creates new person
        /// </summary>
        /// <param name="entity"> New person </param>
        public void Create(Person entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity can't be null!");
            }

            if (string.IsNullOrEmpty(entity.FirstName) || string.IsNullOrEmpty(entity.LastName) ||
                entity.Address == null || entity.Contact == null || entity.Passport == null)
            {
                throw new ArgumentException("Entity person has unfilled field!");
            }

            if (Entities.FindFirst(entity) != null)
            {
                throw new ArgumentException("This person already exists!");
            }
            
            Entities.Add(entity);
        }

        /// <summary>
        /// Deletes person by id
        /// </summary>
        /// <param name="id"> Person's id </param>
        public void Delete(int id)
        {
            Person entity = GetById(id);
            if (Entities.FindFirst(entity) == null)
            {
                throw new ArgumentException("This person doesn't exist!");
            }

            Entities.Remove(entity);
        }

        /// <summary>
        /// Gets all persons
        /// </summary>
        /// <returns> Persons </returns>
        public ICollection<Person> GetAll() => Entities;

        public Person GetById(int id)
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
        /// Gets persons by serial number of passport
        /// </summary>
        /// <param name="value"> Serial number of passport </param>
        /// <returns> Person </returns>
        public Person GetByValue(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("Value can't be null or empty!");
            }

            foreach (var entity in Entities)
            {
                if (entity.Passport.SerialNumber == value)
                {
                    return entity;
                }
            }

            return null;
        }

        /// <summary>
        /// Updates person
        /// </summary>
        /// <param name="entity"> Updated person </param>
        public void Update(Person entity)
        {
            Person person = Entities.FindFirst(entity);

            if (person == null)
            {
                throw new ArgumentException("This person doesn't exist!");
            }

            if (!string.IsNullOrEmpty(entity.LastName))
            {
                person.LastName = entity.LastName;
            }

            if (!string.IsNullOrEmpty(entity.FirstName))
            {
                person.LastName = entity.FirstName;
            }

            if (entity.Accounts != null)
            {
                person.Accounts = entity.Accounts;
            }

            if (entity.Address != null)
            {
                UpdateAdress(person.Address, entity.Address);
            }

            if (entity.Contact != null)
            {
                UpdateContact(person.Contact, entity.Contact);
            }

            if (entity.Passport != null)
            {
                UpdatePassport(person.Passport, entity.Passport);
            }
        }
        #endregion

        #region Private methods
        private void UpdateAdress(AdressData entity, AdressData updatedEntity)
        {
            if (!string.IsNullOrEmpty(updatedEntity.Country))
            {
                entity.Country = updatedEntity.Country;
            }

            if (!string.IsNullOrEmpty(updatedEntity.State))
            {
                entity.State = updatedEntity.State;
            }

            if (!string.IsNullOrEmpty(updatedEntity.City))
            {
                entity.City = updatedEntity.City;
            }

            if (!string.IsNullOrEmpty(updatedEntity.Street))
            {
                entity.Street = updatedEntity.Street;
            }
        }

        private void UpdateContact(ContactData entity, ContactData updatedEntity)
        {
            if (!string.IsNullOrEmpty(updatedEntity.ContactPhone))
            {
                entity.ContactPhone = updatedEntity.ContactPhone;
            }

            if (!string.IsNullOrEmpty(updatedEntity.Email))
            {
                entity.ContactPhone = updatedEntity.Email;
            }
        }

        private void UpdatePassport(PassportData entity, PassportData updatedEntity)
        {
            if (!string.IsNullOrEmpty(updatedEntity.IdentityNumber))
            {
                entity.IdentityNumber = updatedEntity.IdentityNumber;
            }

            if (!string.IsNullOrEmpty(updatedEntity.SerialNumber))
            {
                entity.IdentityNumber = updatedEntity.SerialNumber;
            }
        }
        #endregion
    }
}
