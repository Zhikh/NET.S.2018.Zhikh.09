using System;
using System.Collections.Generic;

namespace Task2.BLL.Interface.Entities
{
    public sealed class Person 
    {
        private string _firstName;
        private string _lastName;

        public Person()
        {
            Accounts = new List<AccountBase>();
        }

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

        public string Email { get; set; }

        public IEnumerable<AccountBase> Accounts { get; set; }
      
    }
}
