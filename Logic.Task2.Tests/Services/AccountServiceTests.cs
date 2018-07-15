using System;
using NUnit.Framework;

namespace Logic.Task2.Tests
{
    [TestFixture]
    public class AccountServiceTests
    {
        #region Test data
        private AccountService _account = new AccountService();
        private DataProvider _dataProvider = DataProvider.Instance;

        private Account _accountData = new Account
        {
            Number = "123456789",
            Owner = new Person
            {
                LastName = "Smbd",
                FirstName = "Smbd",
                Passport = new PassportData
                {
                    IdentityNumber = "3024567V912VV5",
                    SerialNumber = "VV2000233"
                },
                Contact = new ContactData
                {
                    Email = "podg@t.t",
                    ContactPhone = "+ 375 29 399 05 33"
                },
                Address = new AddressData
                {
                    Country = "Somewhere",
                    State = "Somewhere",
                    City = "Somewhere",
                    Street = "Without name"
                }
            },
            InvoiceAmount = 100,
            Bonuses = 0,
            BillType = new AccountType
            {
                Name = ":)",
                BalanceCost = 100,
                ReplenishmentCost = 10
            }
        };
        private Account _anotherAccountData = new Account
        {
            Number = "103050709",
            Owner = new Person
            {
                LastName = "Smbd",
                FirstName = "Smbd",
                Passport = new PassportData
                {
                    IdentityNumber = "3024567V912VV5",
                    SerialNumber = "VV2000233"
                },
                Contact = new ContactData
                {
                    Email = "podg@t.t",
                    ContactPhone = "+ 375 29 399 05 33"
                },
                Address = new AddressData
                {
                    Country = "Somewhere",
                    State = "Somewhere",
                    City = "Somewhere",
                    Street = "Without name"
                }
            },
            InvoiceAmount = 100,
            Bonuses = 0,
            BillType = new AccountType
            {
                Name = ":)",
                BalanceCost = 100,
                ReplenishmentCost = 10
            }
        };
        #endregion

        #region Exceptions
        [Test]
        public void Create_NullEntity_ArgumentNullException()
            => Assert.Throws<ArgumentNullException>(() => _account.Create(null));

        [Test]
        public void Update_NullEntity_ArgumentNullException()
           => Assert.Throws<ArgumentNullException>(() => _account.Update(null));

        [Test]
        public void GetByValue_NullEntity_ArgumentNullException()
            => Assert.Throws<ArgumentNullException>(() => _account.GetByValue(null));

        [Test]
        public void Create_SameEntity_ArgumentException()
        {
            _account.Create(_accountData);
            Assert.Throws<ArgumentException>(() => _account.Create(_accountData));
        }
        #endregion
        
        [Test]
        public void Delete_PersonEntity_DeleteFromCollection()
        {
            _account.Create(_anotherAccountData);

            Account account = _account.GetByValue(_anotherAccountData.Number);
            _account.Delete(account.Id);

            Account actual = _account.GetById(account.Id);
            Assert.AreEqual(null, actual);
        }

        [Test]
        public void Update_PersonEntity_DeleteFromCollection()
        {
            _account.Create(_anotherAccountData);

            _anotherAccountData.InvoiceAmount = 120;
            Account account = _account.GetByValue(_anotherAccountData.Number);
            account.InvoiceAmount = _anotherAccountData.InvoiceAmount;
            _account.Update(account);

            Account actual = _account.GetById(account.Id);
            Assert.AreEqual(_anotherAccountData, actual);
        }
    }
}
