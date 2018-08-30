using System;
using NUnit.Framework;
using Moq;
using Task2.BLL.Interface.Entities;
using Task2.BLL.Interface.Services;
using Task2.BLL.Services;
using Task2.DAL.Interface;
using Task2.DAL.Interface.Repositories;
using System.Collections.Generic;
using Task2.BLL.Mappers;
using System.Linq;
using Task2.DAL.Interfaces.DTO;

namespace Task2.BLL.Tests
{
    [TestFixture]
    public class AccountServiceTests
    {
        #region Test data
        private IList<Account> _accounts = new List<Account>
        {
            new Account
            {
                Owner = new Person
                {
                    LastName = "Smbd",
                    FirstName = "Smbd",
                    Email = "podg@test.com",
                    SerialNumber = "12345678FF"
                },
                Number = "1",
                InvoiceAmount = 100,
                Bonuses = 0,
                AccountType = new AccountType
                {
                    Name = ":)",
                    DepositCost = 100,
                    WithdrawCost = 10
                }
            },
            new Account
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
                    DepositCost = 100,
                    WithdrawCost = 10
                }
            }
        };

        private AccountType _accountType = new AccountType
        {
            Name = "test",
            DepositCost = 10,
            WithdrawCost = 10
        };

        private int _startBonuses = 0;
        private decimal _startBalance = 1000;
        private decimal[] _sourceData = { 10, 20, 50, 100 };
        private decimal[] _withdrawResult = { 990, 970, 920, 820 };
        private decimal[] _withdrawBonuses = { 0, 0, 0, -1 };
        private decimal[] _depositResult = { 1010, 1030, 1080, 1180 };
        private decimal[] _depositBonuses = { 0, 0, 0, 1 };
        #endregion

        private IAccountService _accountService;
        private List<DalAccount> _dalAccounts;
        [SetUp]
        public void Init()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockPersonService = new Mock<IPersonService>();
            _dalAccounts = _accounts.ToDalAccount().ToList();
            _accountService = AccountService.GetInstance(mockUnitOfWork.Object, CreateRepository(_dalAccounts),
                mockPersonService.Object);
        }

        #region Exceptions
        [Test]
        public void Open_NullEntity_ArgumentNullException()
            => Assert.Throws<ArgumentNullException>(() => _accountService.Open(null));

        [Test]
        public void Deposit_NullNumber_ArgumentNullException()
           => Assert.Throws<ArgumentNullException>(() => _accountService.Deposit(null, 90));

        [Test]
        public void Withdrawal_NullNumber_ArgumentNullException()
            => Assert.Throws<ArgumentNullException>(() => _accountService.Withdraw(null, 40));
        
        [Test]
        public void Open_SameEntity_ArgumentException()
        {
            _accountService.Open(_accounts[0]);
            Assert.Throws<ArgumentException>(() => _accountService.Open(_accounts[0]));
        }
        #endregion

        #region Create account
        [Test]
        public void Open_Account_CorrectResult()
        {
            foreach (var account in _accounts)
            {
                _accountService.Open(account);
            }

            foreach (var account in _accounts)
            {
                Account accountBase = _accountService.GetAccount(account.Number);
                Assert.AreEqual(account, accountBase);
            }
        }
        #endregion

        #region Close
        [Test]
        public void Close_Account_CorrectResult()
        {
            _accountService.Open(_accounts[0]);
            _accountService.Close(_accounts[0].Number);

            var account = _accountService.GetAccount(_accounts[0].Number);

            Assert.AreEqual(false, account.IsOpen);
        }
        #endregion

        #region Deposit
        [Test]
        public void Deposit_Account_CorrectResult()
        {
            AssertOperation(_accountService.Deposit, "testDepositNumber", _depositResult, _depositBonuses);
        }
        #endregion

        #region Withdraw
        [Test]
        public void Withdraw_Account_CorrectResult()
        {
            AssertOperation(_accountService.Withdraw, "tesWithdrawNumber", _withdrawResult, _withdrawBonuses);
        }
        #endregion

        #region GetUserAccounts
        #endregion

        #region Additional methods
        private void AssertOperation(Action<string, decimal> action, string number,
            decimal[] expectedInvoiceAmount, decimal[] expectedBonuses)
        {
            Account account = _accounts[0];
            account.Number = number;
            account.InvoiceAmount = _startBalance;
            account.Bonuses = _startBonuses;

            _accountService.Open(account);

            for (int i = 0; i < _sourceData.Length; i++)
            {
                action(number, _sourceData[i]);

                var temp = _accountService.GetAccount(number);

                Assert.AreEqual(expectedInvoiceAmount[i], temp.InvoiceAmount);
                Assert.AreEqual(expectedBonuses[i], temp.Bonuses);
            }
        }

        private IAccountRepository CreateRepository(List<DalAccount> data)
        {
            var mock = new Mock<IAccountRepository>();

            mock.Setup(x => x.Create(It.IsAny<DalAccount>()))
                .Callback(new Action<DalAccount>(x =>
                {
                    x.Id = data.Count;
                    data.Add(x);
                }));

            mock.Setup(x => x.Update(It.IsAny<DalAccount>()))
                .Callback(new Action<DalAccount>(x =>
                {
                    var i = data.FindIndex(q => q.Id.Equals(x.Id));
                    data[i] = x;
                }));
            
            mock.Setup(x => x.Delete(It.IsAny<DalAccount>()))
                .Callback(new Action<DalAccount>(x => 
                {
                    var account = data.Find(q => q.Id.Equals(x.Id));
                    account.IsOpen = false;
                }));

            mock.Setup(x => x.GetAll()).Returns(data);

            mock.Setup(x => x.GetById( It.IsAny<int>()))
                .Returns((int i) => data.Where(
                x => x.Id == i).Single());

            mock.Setup(x => x.GetByPredicate(It.IsAny<Func<DalAccount, bool>>()))
                .Returns((Func<DalAccount, bool> expr) => data.First(expr));

            return mock.Object;
        }
        #endregion
    }
}
