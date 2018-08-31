namespace Task2.BLL.Interface.Services
{
    public interface IAccountNumberCreateService<in TSource>
    {
        string Create(TSource source);
    }
}
