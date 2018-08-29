namespace Task2.DAL.Interface.Strategies
{
    public interface IAccountNumberGenerator<in T>
    {
        /// <summary>
        /// Returns account number
        /// </summary>
        /// <param name="value"> Value for generation </param>
        /// <returns> Account number </returns>
        string GenerateNumber(T value);
    }
}
