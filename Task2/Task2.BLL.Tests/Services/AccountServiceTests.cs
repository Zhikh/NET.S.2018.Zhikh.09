using System;
using DAL.Task2.Repositories;
using NUnit.Framework;
using Task2.BLL.Interface.Entities;
using Task2.BLL.Interface.Services;
using Task2.BLL.Services;
using Task2.DAL.Fake.Strategies;
using Task2.DAL.Interface.Repositories;
using Task2.DAL.Interface.Strategies;

namespace Task2.BLL.Tests
{
    [TestFixture]
    public class AccountServiceTests
    {
        #region Test data
        private IAccountService _account;
        private Account _accountData;
        private Account _anotherAccountData;
        private IAccountNumberGenerator<int> _numberGenerator;

        [SetUp]
        public void InitData()
        {
            _numberGenerator = new FakeAccountNumberGenerator();
            _account = new AccountService(new FakeAccountRepository(), _numberGenerator, new PersonService(new FakePersonRepository()));

            _accountData = new Account(new FakeAccountNumberGenerator())
            {
                Owner = new Person
                {
                    LastName = "Smbd",
                    FirstName = "Smbd",
                    Email = "podg@test.com",
                    SerialNumber = "12345678FF"
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

            _anotherAccountData = new Account(new FakeAccountNumberGenerator())
            {
                Owner = new Person
                {
                    LastName = "Smbd",
                    FirstName = "Smbd",
                    Email = "podg@test.com",
                    SerialNumber = "12345678FA"
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
        }
        #endregion

        #region Exceptions
        [Test]
        public void Open_NullEntity_ArgumentNullException()
            => Assert.Throws<ArgumentNullException>(() => _account.Open(null));

        [Test]
        public void Deposit_NullNumber_ArgumentNullException()
           => Assert.Throws<ArgumentNullException>(() => _account.Deposit(null, 90));

        [Test]
        public void Withdrawal_NullNumber_ArgumentNullException()
            => Assert.Throws<ArgumentNullException>(() => _account.Withdraw(null, 40));

        // TODO: fix me 
        [Test]
        public void Open_SameEntity_ArgumentException()
        {
            _account.Open(_accountData);
            Assert.Throws<ArgumentException>(() => _account.Open(_accountData));
        }
        #endregion

        #region Create account
        [Test]
        public void Open_AccountBase_CorrectResult()
        {
            _account.Open(_accountData);

            Account accountBase = _account.GetAccount(_numberGenerator.GenerateNumber(_accountData.GetHashCode()));

            Assert.AreEqual(_accountData, accountBase);
        }
        #endregion

        #region Close
        [Test]
        public void Close_AccountBase_CorrectResult()
        {
            _account.Open(_accountData);
            _account.Close(_accountData.Number);
        }
        #endregion

        #region Deposit
        #endregion

        #region Withdraw
        #endregion

        #region GetAccount
        #endregion

        #region GetUserAccounts
        #endregion
        //[Test]
        //public void Delete_PersonEntity_DeleteFromCollection()
        //{
        //    _account.Create(_anotherAccountData);

        //    AccountBase account = _account.GetByValue(_anotherAccountData.Number);
        //    _account.Delete(account.Id);

        //    AccountBase actual = _account.GetById(account.Id);
        //    Assert.AreEqual(null, actual);
        //}

        //[Test]
        //public void Update_PersonEntity_DeleteFromCollection()
        //{
        //    _account.Create(_anotherAccountData);

        //    _anotherAccountData.InvoiceAmount = 120;
        //    AccountBase account = _account.GetByValue(_anotherAccountData.Number);
        //    account.InvoiceAmount = _anotherAccountData.InvoiceAmount;
        //    _account.Update(account);

        //    AccountBase actual = _account.GetById(account.Id);
        //    Assert.AreEqual(_anotherAccountData, actual);
        //}
    }
}
