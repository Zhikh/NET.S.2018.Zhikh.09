using System;
using System.Collections.Generic;

namespace Task2.BLL.Interface.Entities
{
    public sealed class Person 
    {
        #region Private fields
        private string _firstName;
        private string _lastName;
        private string _serialNumber;
        #endregion

        #region Public API
        /// <summary>
        /// Initializes a new instance of the <see cref="Person" />.
        /// </summary>
        public Person()
        {
            Accounts = new List<Account>();
        }

        /// <summary>
        /// First name of the person
        /// </summary>
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

        /// <summary>
        /// Last name of the person
        /// </summary>
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

        /// <summary>
        /// Middle name of the person
        /// </summary>
        public string SecondName { get; set; }

        /// <summary>
        /// Email of person
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Passport serial number
        /// </summary>
        public string SerialNumber
        {
            get
            {
                return _serialNumber;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(nameof(LastName) + " can't be null or empty!");
                }

                _serialNumber = value;
            }
        }

        /// <summary>
        /// Accounts of the person
        /// </summary>
        public ICollection<Account> Accounts { get; set; }
        #endregion
    }
}
