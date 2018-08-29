using System;
using NUnit.Framework;
using Task2.BLL.Interface.Entities;
using Task2.BLL.Interface.Services;
using Task2.BLL.Services;
using Task2.DAL.Interface.Strategies;
using Moq;
using Task2.DAL.Interface;
using Task2.DAL.Interface.Repositories;
using Task2.BLL.Mappers;

namespace Task2.BLL.Tests
{
    [TestFixture]
    public class AccountServiceTests
    {
        #region Test data
        private IAccountService _account;
        private IAccountNumberGenerator<int> _numberGenerator;
        private IUnitOfWork _unitOfWork;
        private IPersonService _person;
        private Account _accountData;
        private Account _anotherAccountData;

        private int _number;

        [SetUp]
        public void InitData()
        {
            var repo = new MockRepository(MockBehavior.Default);
            var accountRepositoryMock = repo.Create<IAccountRepository>();

            accountRepositoryMock.Setup(c => c.Create(_accountData.ToDalAccount()));
            accountRepositoryMock.Setup(c => c.Delete(_accountData.ToDalAccount()));

            _numberGenerator = Mock.Of<IAccountNumberGenerator<int>>(d => d.GenerateNumber(It.IsAny<int>()) == $"{_number.ToString()}");
            _unitOfWork = Mock.Of<IUnitOfWork>();

            var personRepositoryMock = repo.Create<IPersonRepository>();
            _person = PersonService.GetInstance(_unitOfWork, personRepositoryMock.Object);

            _account = AccountService.GetInstance(_unitOfWork, accountRepositoryMock.Object, _person);

            _accountData = new Account(_numberGenerator)
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
            _number++;

            _anotherAccountData = new Account(_numberGenerator)
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
            _number++;
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
