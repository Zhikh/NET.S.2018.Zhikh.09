using System;
using NUnit.Framework;

namespace Logic.Task2.Tests
{
    [TestFixture]
    class AccountOperationTests
    {
        #region Test data
        private DataProvider _provider = DataProvider.Instance;

        private Account _account = new Account
        {
            Number = "100456789",
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
            InvoiceAmount = 1000,
            Bonuses = 0,
            BillType = new AccountType
            {
                Name = ":)",
                BalanceCost = 100,
                ReplenishmentCost = 100
            }
        };

        private Account _anotherAccount = new Account
        {
            Number = "100000709",
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
            InvoiceAmount = 1000,
            Bonuses = 0,
            BillType = new AccountType
            {
                Name = ":)",
                BalanceCost = 100,
                ReplenishmentCost = 100
            }
        };

        private Account _accountNullPerson = new Account
        {
            Number = "100456789",
            Owner = null,
            InvoiceAmount = 100,
            Bonuses = 0,
            BillType = new AccountType
            {
                Name = ":)",
                BalanceCost = 100,
                ReplenishmentCost = 10
            }
        };

        private decimal _invoiceAmount = 1000;
        private int _bonuses = 0;
        private decimal[] _sourceData = { 10, 20, 50, 100 };
        private decimal[] _debitResult = { 990, 970, 920, 820 };
        private decimal[] _debitBonuses= { 0, 0, 0, -1 };
        private decimal[] _creditResult = { 1010, 1030, 1080, 1180 };
        private decimal[] _creditBonuses = { 0, 0, 0, 1 };
        #endregion

        #region Exceptions
        [Test]
        public void Open_NullEntity_ArgumentNullException()
            => Assert.Throws<ArgumentNullException>(() => AccountOperation.Open(null));

        [Test]
        public void Open_EntityWithNullOwner_ArgumentNullException()
            => Assert.Throws<ArgumentException>(() => AccountOperation.Open(_accountNullPerson));

        [Test]
        public void Close_EmptyAccountNumber_ArgumentException()
            => Assert.Throws<ArgumentException>(() => AccountOperation.Close(""));

        [Test]
        public void Close_NullAccountNumber_ArgumentException()
            => Assert.Throws<ArgumentException>(() => AccountOperation.Close(null));

        [Test]
        public void Close_UnexistingAccountNumber_ArgumentException()
            => Assert.Throws<ArgumentException>(() => AccountOperation.Close("something for checking"));

        [Test]
        public void Debit_UnexistingAccountNumber_ArgumentException()
            => Assert.Throws<ArgumentException>(() => AccountOperation.Debit("something for checking", 0));

        [Test]
        public void Credit_UnexistingAccountNumber_ArgumentException()
            => Assert.Throws<ArgumentException>(() => AccountOperation.Credit("something for checking", 0));
        #endregion

        [Test]
        public void Open_AccountEntity_CorrectAdding()
        {
            AccountOperation.Open(_anotherAccount);

            var actual = _provider.Accounts.FindFirst(_account);

            Assert.AreEqual(_account, actual);
        }

        [Test]
        public void Debit_AccountData_CorrectResult()
        {
            Account account = _provider.Accounts.FindFirst(_account);
            if (account == null)
            {
                AccountOperation.Open(_account);
            }
            _account.InvoiceAmount = _invoiceAmount;
            _account.Bonuses = _bonuses;

            for (int i = 0; i < _sourceData.Length; i++)
            {
                AccountOperation.Debit(_account.Number, _sourceData[i]);

                Assert.AreEqual(_debitResult[i], _account.InvoiceAmount);
                Assert.AreEqual(_debitBonuses[i], _account.Bonuses);
            }
        }

        [Test]
        public void Credit_AccountData_CorrectResult()
        {
            Account account = _provider.Accounts.FindFirst(_account);
            if (account == null)
            {
                AccountOperation.Open(_account);
            }
            _account.InvoiceAmount = _invoiceAmount;
            _account.Bonuses = _bonuses;

            for (int i = 0; i < _sourceData.Length; i++)
            {
                AccountOperation.Credit(_account.Number, _sourceData[i]);

                Assert.AreEqual(_creditResult[i], _account.InvoiceAmount);
                Assert.AreEqual(_creditBonuses[i], _account.Bonuses);
            }
        }

        [Test]
        public void Close_AccountData_CorrectReult()
        {
            Account account = _provider.Accounts.FindFirst(_account);
            if (account == null)
            {
                AccountOperation.Open(_account);
            }

            AccountOperation.Close(_account.Number);
        }
    }
}
