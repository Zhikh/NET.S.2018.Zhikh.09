using System.Web.Mvc;
using Task2.BLL.Interface.Services;
using Ninject;
using Task2.DependencyResolver;

namespace Task2.UI.MVC.Controllers
{
    public class BaseController : Controller
    {
        private readonly IKernel resolver;
        
        public IPersonService PersonService { get; set; }

        public IAccountService AccountService { get; set; }

        public BaseController()
        {
            resolver = new StandardKernel();
            resolver.ConfigurateResolver();

            PersonService = resolver.Get<IPersonService>();
            AccountService = resolver.Get<IAccountService>();
        }

        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}