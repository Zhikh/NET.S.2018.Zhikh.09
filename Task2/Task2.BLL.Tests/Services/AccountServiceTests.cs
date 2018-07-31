using System;
using DAL.Task2.Repositories;
using NUnit.Framework;
using Task2.BLL.Interface.Entities;
using Task2.DAL.Fake.Strategies;

namespace Task2.BLL.Tests
{
    [TestFixture]
    public class AccountServiceTests
    {
        #region Test data
        private AccountService _account = new AccountService(new FakeAccountRepository(), new FakePersonRepository());
        //private DataProvider _dataProvider = DataProvider.Instance;

        private AccountBase _accountData = new AccountBase(new FakeAccountNumberGenerator())
        {
            Owner = new Person
            {
                LastName = "Smbd",
                FirstName = "Smbd",
                Email = "podg@test.com",
            },

            InvoiceAmount = 100,
            Bonuses = 0,
            AccountType = new AccountType
            {
                Name = ":)",
                BalanceCost = 100,
                ReplenishmentCost = 10
            }
        };
        private AccountBase _anotherAccountData = new AccountBase(new FakeAccountNumberGenerator())
        {
            Owner = new Person
            {
                LastName = "Smbd",
                FirstName = "Smbd",
                Email = "podg@test.com",
                   
            },
            InvoiceAmount = 100,
            Bonuses = 0,
            AccountType = new AccountType
            {
                Name = ":)",
                BalanceCost = 100,
                ReplenishmentCost = 10
            }
        };
        #endregion

        #region Exceptions
        [Test]
        public void Open_NullEntity_ArgumentNullException()
            => Assert.Throws<ArgumentNullException>(() => _account.Open(null));

        [Test]
        public void Deposit_NullEntity_ArgumentNullException()
           => Assert.Throws<ArgumentNullException>(() => _account.Deposit(null, 90));

        [Test]
        public void Withdrawal_NullEntity_ArgumentNullException()
            => Assert.Throws<ArgumentNullException>(() => _account.Withdraw(null, 40));

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

            AccountBase account = _account.GetByValue(_anotherAccountData.Number);
            _account.Delete(account.Id);

            AccountBase actual = _account.GetById(account.Id);
            Assert.AreEqual(null, actual);
        }

        [Test]
        public void Update_PersonEntity_DeleteFromCollection()
        {
            _account.Create(_anotherAccountData);

            _anotherAccountData.InvoiceAmount = 120;
            AccountBase account = _account.GetByValue(_anotherAccountData.Number);
            account.InvoiceAmount = _anotherAccountData.InvoiceAmount;
            _account.Update(account);

            AccountBase actual = _account.GetById(account.Id);
            Assert.AreEqual(_anotherAccountData, actual);
        }
    }
}
