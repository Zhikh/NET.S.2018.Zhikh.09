using System;
using Task2.DAL.Interface.Repositories;
using Task2.DAL.Interfaces.DTO;

namespace DAL.Task2.Repositories
{
    public class FakePersonRepository : BaseRepository<DalPerson>, IPersonRepository
    {
        #region Private and internal methods
        internal override DalPerson FindEntity(string serialNumber)
        {
            if (string.IsNullOrEmpty(serialNumber))
            {
                throw new ArgumentException("Value can't be null or empty!", nameof(serialNumber));
            }

            foreach (var entity in Entities)
            {
                if (entity.SerialNumber == serialNumber)
                {
                    return entity;
                }
            }

            return null;
        }

        internal override void Update(DalPerson entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            DalPerson person = null;

            foreach (var element in Entities)
            {
                if (element.SerialNumber == entity.SerialNumber)
                {
                    person = element;
                    break;
                }
            }

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

            if (entity.Contact != null)
            {
                UpdateContact(person.Contact, entity.Contact);
            }
        }

        internal override bool IsInvalid(DalPerson entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return string.IsNullOrEmpty(entity.FirstName) || string.IsNullOrEmpty(entity.LastName) ||
                       entity.Contact == null || entity.SerialNumber == null;
        }

        private void UpdateContact(DalContactData entity, DalContactData updatedEntity)
        {
            if (!string.IsNullOrEmpty(updatedEntity.Email))
            {
                entity.Email = updatedEntity.Email;
            }
        }

        #endregion
    }
}
