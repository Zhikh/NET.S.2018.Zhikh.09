using Ninject;
using Task2.BLL.Interface.Services;
using Task2.BLL.Services;
using Task2.DAL.Fake.Strategies;
using Task2.DAL.Interface.Repositories;
using Task2.DAL.Interface.Strategies;
using Task2.DAL.Repositories;

namespace Task2.DependencyResolver
{
    public static class ResolverConfig
    {
        public static void ConfigurateResolver(this IKernel kernel)
        {
            // services
            kernel.Bind<IAccountService>().To<AccountService>();
            kernel.Bind<IPersonService>().To<PersonService>();

            // repositories
            kernel.Bind<IAccountRepository>().To<AccountRepository>();
            kernel.Bind<IPersonRepository>().To<PersonRepository>();

            // strategy
            kernel.Bind<IAccountNumberGenerator<int>>().To<FakeAccountNumberGenerator>();
        }
    }
}
