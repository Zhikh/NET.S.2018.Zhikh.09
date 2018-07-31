namespace Task2.DAL.Interface.Strategies
{
    public interface IAccountNumberGenerator<in T>
    {
        string GenerateNumber(T ivalued);
    }
}
