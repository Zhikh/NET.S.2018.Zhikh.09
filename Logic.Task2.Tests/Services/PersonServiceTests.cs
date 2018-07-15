using System;
using System.Collections.Generic;
using NUnit.Framework;
using Logic.Task2.Services;

namespace Logic.Task2.Tests
{
    [TestFixture]
    class PersonServiceTests
    {
        #region Test data
        private PersonService _person = new PersonService();
        private DataProvider _dataProvider = DataProvider.Instance;

        private Person[] _clients =
        {
            new Person
            {
                LastName = "Pupkin",
                FirstName = "Vasiliy",
                Passport = new PassportData
                {
                    IdentityNumber = "1234567T912TT5",
                    SerialNumber = "TT1112233"
                },
                Contact = new ContactData
                {
                    Email = "dfdg@t.t",
                    ContactPhone = "+ 375 33 333 33 33"
                },
                Address = new AdressData
                {
                    Country = "Somewhere",
                    State = "Somewhere",
                    City = "Somewhere",
                    Street = "Without name"
                }
            },
            new Person
            {
                LastName = "Ivanov",
                FirstName = "Ivan",
                Passport = new PassportData
                {
                    IdentityNumber = "1234567V912VV5",
                    SerialNumber = "VV1112233"
                },
                Contact = new ContactData
                {
                    Email = "dfdg@t.t",
                    ContactPhone = "+ 375 44 343 33 33"
                },
                Address = new AdressData
                {
                    Country = "Somewhere",
                    State = "Somewhere",
                    City = "Somewhere",
                    Street = "Without name"
                }
            },
            new Person
            {
                LastName = "Somebody",
                FirstName = "Somebody",
                Passport = new PassportData
                {
                    IdentityNumber = "2224567V912VV5",
                    SerialNumber = "VV2222233"
                },
                Contact = new ContactData
                {
                    Email = "doodg@t.t",
                    ContactPhone = "+ 375 44 343 55 33"
                },
                Address = new AdressData
                {
                    Country = "Somewhere",
                    State = "Somewhere",
                    City = "Somewhere",
                    Street = "Without name"
                }
            }
        };
        private Person _client = new Person
        {
            LastName = "Update",
            FirstName = "Delete",
            Passport = new PassportData
            {
                IdentityNumber = "3424567V912VV5",
                SerialNumber = "VV2272233"
            },
            Contact = new ContactData
            {
                Email = "podg@t.t",
                ContactPhone = "+ 375 29 303 55 33"
            },
            Address = new AdressData
            {
                Country = "Somewhere",
                State = "Somewhere",
                City = "Somewhere",
                Street = "Without name"
            }
        };
        #endregion

        #region Exceptions
        [Test]
        public void Create_NullEntity_ArgumentNullException()
            => Assert.Throws<ArgumentNullException>(() => _person.Create(null));

        [Test]
        public void Update_NullEntity_ArgumentNullException()
           => Assert.Throws<ArgumentNullException>(() => _person.Update(null));
        
        [Test]
        public void GetByValue_NullEntity_ArgumentNullException()
            => Assert.Throws<ArgumentNullException>(() => _person.GetByValue(null));

        [Test]
        public void Create_SameEntity_ArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _person.Create(_clients[0]));
        }
        #endregion

        #region Create
        [Test]
        public void Create_PersonEntity_SaveToCollection()
        {
            foreach (var client in _clients)
            {
                _person.Create(client);

                Person actual = _dataProvider.Persons.GetLast();
                Assert.AreEqual(client, actual);
            }
        }

        [Test]
        public void Delete_PersonEntity_DeleteFromCollection()
        {
            _person.Create(_client);

            Person person = _person.GetByValue(_client.Passport.SerialNumber); 
            _person.Delete(person.Id);

            Person actual = _person.GetById(person.Id);
            Assert.AreEqual(null, actual);
        }

        [Test]
        public void Update_PersonEntity_DeleteFromCollection()
        {
            _person.Create(_client);

            _client.LastName = "After updating";
            Person person = _person.GetByValue(_client.Passport.SerialNumber);
            person.LastName = _client.LastName;
            _person.Update(_client);

            Person actual = _person.GetById(person.Id);
            Assert.AreEqual(_client, actual);
        }
        #endregion
    }
}
