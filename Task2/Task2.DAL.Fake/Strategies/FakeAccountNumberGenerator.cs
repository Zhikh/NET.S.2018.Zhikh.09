using System;
using Task2.DAL.Interface.Strategies;

namespace Task2.DAL.Fake.Strategies
{
    public sealed class FakeAccountNumberGenerator : IAccountNumberGenerator<int>
    {
        private readonly string _balance = "42605";
        private readonly string _filial = "0000";
        private readonly string _currency = "906";
        private readonly string _key = "1";
        private readonly int _accountNumberLength = 7;

        public string GenerateNumber(int value) 
            => _balance + _filial + _currency + _key + GetAccountNumber(value);

        private string GetAccountNumber(int value)
        {
            string result = value.ToString();
            int n = _accountNumberLength - result.Length;

            if ( n < 0)
            {
                throw new ArgumentException(nameof(value) + " isn't valid for generating of account number!");
            }
            result = result.Insert(0, new string('0', n));

            return result;
        }
    }
}
