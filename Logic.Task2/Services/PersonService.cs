using System;

namespace Logic.Task2.Services
{
    public sealed class PersonService : IService<Person>
    {
        private DataProvider _provider;

        #region Public API
        public PersonService()
        {
            _provider = DataProvider.Instance;
        }

        public void Create(Person entity)
        {
            if (_provider.Persons.FindFirst(entity) != null)
            {
                throw new ArgumentException("This person already exists!");
            }

            _provider.Persons.Add(entity);
        }

        public void Delete(Person entity)
        {
            if (_provider.Persons.FindFirst(entity) == null)
            {
                throw new ArgumentException("This person doesn't exist!");
            }

            _provider.Persons.Remove(entity);
        }

        public Person GetByValue(string value)
        {
            foreach (var element in _provider.Persons)
            {
                if (element.Passport.SerialNumber == value)
                {
                    return element;
                }
            }

            return null;
        }

        public void Update(Person entity)
        {
            Person person = _provider.Persons.FindFirst(entity);

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
            if (entity.Bill != null)
            {
                person.Bill = entity.Bill;
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
        private void UpdateAdress(AddressData entity, AddressData updatedEntity)
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
