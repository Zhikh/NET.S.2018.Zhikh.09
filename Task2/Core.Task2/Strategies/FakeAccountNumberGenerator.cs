namespace Core.Task2.Strategies
{
    public sealed class FakeAccountNumberGenerator : IAccountNumberGenerator
    {
        private readonly string _balance = "42605";
        private readonly string _filial = "0000";
        private readonly string _currency = "906";
        private readonly string _key = "1";
        private readonly int _accountNumberLength = 7;

        public string GenerateNumber(int id) 
            => _balance + _filial + _currency + _key + GetAccountNumber(id);

        private string GetAccountNumber(int id)
        {
            string result = id.ToString();
            int n = _accountNumberLength - result.Length;
            if ( n < 0)
            {
            }
            result = result.Insert(0, new string('0', n));

            return result;
        }
    }
}
