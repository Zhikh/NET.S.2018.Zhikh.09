using System;
using System.Collections.Generic;
using Task2.DAL.Interface.DTO;

namespace Task2.DAL.Interfaces.DTO
{
    public sealed class DalPerson : IEntity
    {
        private DalContactData _contactData;

        private string _firstName;
        private string _lastName;

        public int Id { get; set; }

        public string FirstName
        {
            get
            {
                return _firstName;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(nameof(FirstName) + " can't be null or empty!");
                }

                _firstName = value;
            }
        }

        public string LastName
        {
            get
            {
                return _lastName;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(nameof(LastName) + " can't be null or empty!");
                }

                _lastName = value;
            }
        }

        public string SecondName { get; set; }

        public DalContactData Contact
        {
            get
            {
                return _contactData;
            }

            set
            {
                _contactData = value ?? throw new ArgumentException(nameof(Contact) + " can't be null or empty!");
            }
        }
        
        public IEnumerable<DalAccount> Accounts { get; set; }
    }
}
