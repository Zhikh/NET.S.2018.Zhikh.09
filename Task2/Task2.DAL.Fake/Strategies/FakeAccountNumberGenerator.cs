using System;
using Task2.DAL.Interface.Strategies;

namespace Task2.DAL.Fake.Strategies
{
    public sealed class FakeAccountNumberGenerator : IAccountNumberGenerator<int>
    {
        private const string _balance = "42605";
        private const string _filial = "0000";
        private const string _currency = "906";
        private const string _key = "1";

        public string GenerateNumber(int value) 
            => _balance + _filial + _currency + _key + value.ToString();
    }
}
