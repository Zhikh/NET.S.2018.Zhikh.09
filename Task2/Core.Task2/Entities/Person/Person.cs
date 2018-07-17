using System;
using System.Collections.Generic;

namespace Core.Task2.Entities
{
    public sealed class Person : BaseEntity
    {
        private AdressData _adressData;
        private ContactData _contactData;
        private PassportData _passportData;
        private string _firstName;
        private string _lastName;
        private static int _id = 0;

        public Person()
        {
            Id = _id++;
            Accounts = new List<Account>();
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

        public AdressData Address
        {
            get
            {
                return _adressData;
            }

            set
            {
                _adressData = value ?? throw new ArgumentException(nameof(Address) + " can't be null or empty!");
            }
        }

        public ContactData Contact
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

        public PassportData Passport
        {
            get
            {
                return _passportData;
            }

            set
            {
                _passportData = value ?? throw new ArgumentException(nameof(Passport) + " can't be null or empty!");
            }
        }

        public ICollection<Account> Accounts { get;}

        public override bool Equals(object obj)
        {
            var person = obj as Person;
            return person != null &&
                   base.Equals(obj) &&
                   FirstName == person.FirstName &&
                   LastName == person.LastName &&
                   SecondName == person.SecondName &&
                   EqualityComparer<AdressData>.Default.Equals(Address, person.Address) &&
                   EqualityComparer<ContactData>.Default.Equals(Contact, person.Contact) &&
                   EqualityComparer<PassportData>.Default.Equals(Passport, person.Passport) &&
                   EqualityComparer<ICollection<Account>>.Default.Equals(Accounts, person.Accounts);
        }

        public override int GetHashCode()
        {
            var hashCode = 681110399;
            hashCode = (hashCode * -1521134295) + base.GetHashCode();
            hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(FirstName);
            hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(LastName);
            hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(SecondName);
            hashCode = (hashCode * -1521134295) + EqualityComparer<AdressData>.Default.GetHashCode(Address);
            hashCode = (hashCode * -1521134295) + EqualityComparer<ContactData>.Default.GetHashCode(Contact);
            hashCode = (hashCode * -1521134295) + EqualityComparer<PassportData>.Default.GetHashCode(Passport);
            hashCode = (hashCode * -1521134295) + EqualityComparer<ICollection<Account>>.Default.GetHashCode(Accounts);
            return hashCode;
        }
    }
}
