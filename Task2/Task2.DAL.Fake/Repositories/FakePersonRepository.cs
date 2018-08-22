using System;
using Task2.DAL.Interface.Repositories;
using Task2.DAL.Interfaces.DTO;

namespace DAL.Task2.Repositories
{
    public class FakePersonRepository : BaseRepository<DalPerson>, IPersonRepository
    {
        #region Private and internal methods
        //private void UpdateAdress(DalAdressData entity, DalAdressData updatedEntity)
        //{
        //    if (!string.IsNullOrEmpty(updatedEntity.Country))
        //    {
        //        entity.Country = updatedEntity.Country;
        //    }

        //    if (!string.IsNullOrEmpty(updatedEntity.State))
        //    {
        //        entity.State = updatedEntity.State;
        //    }

        //    if (!string.IsNullOrEmpty(updatedEntity.City))
        //    {
        //        entity.City = updatedEntity.City;
        //    }

        //    if (!string.IsNullOrEmpty(updatedEntity.Street))
        //    {
        //        entity.Street = updatedEntity.Street;
        //    }
        //}

        private void UpdateContact(DalContactData entity, DalContactData updatedEntity)
        {
            if (!string.IsNullOrEmpty(updatedEntity.Email))
            {
                entity.Email = updatedEntity.Email;
            }
        }

        //private void UpdatePassport(DalPassportData entity, DalPassportData updatedEntity)
        //{
        //    if (!string.IsNullOrEmpty(updatedEntity.IdentityNumber))
        //    {
        //        entity.IdentityNumber = updatedEntity.IdentityNumber;
        //    }

        //    if (!string.IsNullOrEmpty(updatedEntity.SerialNumber))
        //    {
        //        entity.IdentityNumber = updatedEntity.SerialNumber;
        //    }
        //}

        internal override DalPerson FindEntity(string value)
        {
            foreach (var entity in Entities)
            {
                if (entity.SerialNumber == value)
                {
                    return entity;
                }
            }

            return null;
        }

        internal override void Update(DalPerson entity)
        {
            DalPerson person = Entities.FindFirst(entity);

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

            //if (entity.Address != null)
            //{
            //    UpdateAdress(person.Address, entity.Address);
            //}

            if (entity.Contact != null)
            {
                UpdateContact(person.Contact, entity.Contact);
            }

        //    if (entity.Passport != null)
        //    {
        //        UpdatePassport(person.Passport, entity.Passport);
        //    }
        }

        internal override bool IsInvalid(DalPerson entity)
            => string.IsNullOrEmpty(entity.FirstName) || string.IsNullOrEmpty(entity.LastName) || 
            entity.Contact == null || entity.SerialNumber == null;
        #endregion
    }
}
