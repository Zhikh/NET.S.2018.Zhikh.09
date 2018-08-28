using Ninject;
using Task2.BLL.Interface.Services;
using Task2.BLL.Services;
using Task2.DAL;
using Task2.DAL.Fake.Strategies;
using Task2.DAL.Interface;
using Task2.DAL.Interface.Repositories;
using Task2.DAL.Interface.Strategies;
using Task2.DAL.Repositories;
using Task2.ORM;

namespace Task2.DependencyResolver
{
    public static class ResolverConfig
    {
        public static void ConfigurateResolver(this IKernel kernel)
        {
            var context = new BankModel();
            // unit of work
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument("context", context); 

            // services
            kernel.Bind<IPersonService>().ToMethod(
                c => PersonService.GetInstance(kernel.Get<IUnitOfWork>(),
                kernel.Get<IPersonRepository>()));

            kernel.Bind<IAccountService>().ToMethod(
                c => AccountService.GetInstance(kernel.Get<IUnitOfWork>(),
                kernel.Get<IAccountRepository>(), kernel.Get<IPersonService>()));

            // repositories
            kernel.Bind<IAccountRepository>().To<AccountRepository>().WithConstructorArgument("context", context);
            kernel.Bind<IPersonRepository>().To<PersonRepository>().WithConstructorArgument("context", context);

            // strategy
            kernel.Bind<IAccountNumberGenerator<int>>().To<FakeAccountNumberGenerator>();
        }
    }
}
