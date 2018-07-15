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
                Address = new AddressData
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
                Address = new AddressData
                {
                    Country = "Somewhere",
                    State = "Somewhere",
                    City = "Somewhere",
                    Street = "Without name"
                }
            }
        };
        #endregion

        #region Exceptions
        [Test]
        public void Create_NullEntity_ArgumentNullException()
            => Assert.Catch(() => _person.Create(null));

        [Test]
        public void Delete_NullEntity_ArgumentNullException()
           => Assert.Catch(() => _person.Delete(null));

        [Test]
        public void Update_NullEntity_ArgumentNullException()
           => Assert.Catch(() => _person.Update(null));
        
        [Test]
        public void GetByValue_NullEntity_ArgumentNullException()
            => Assert.Catch(() => _person.GetByValue(null));

        [Test]
        public void Create_SameEntity_ArgumentException()
        {
            _person.Create(_clients[0]);
            Assert.Catch(() => _person.Create(_clients[0]));
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
        #endregion
    }
}
