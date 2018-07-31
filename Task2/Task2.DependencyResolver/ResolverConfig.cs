using DAL.Task2.Repositories;
using Ninject;
using Task2.BLL;
using Task2.BLL.Interface.Services;
using Task2.DAL.Fake.Strategies;
using Task2.DAL.Interface.Repositories;
using Task2.DAL.Interface.Strategies;

namespace Task2.DependencyResolver
{
    public static class ResolverConfig
    {
        public static void ConfigurateResolver(this IKernel kernel)
        {
            kernel.Bind<IAccountService>().To<AccountService>();
            kernel.Bind<IAccountRepository>().To<FakeAccountRepository>();
            kernel.Bind<IPersonRepository>().To<FakePersonRepository>();
            kernel.Bind<IAccountNumberGenerator<int>>().To<FakeAccountNumberGenerator>();
        }
    }
}
