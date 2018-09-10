using System.Web.Mvc;
using Task2.BLL.Interface.Services;
using Ninject;

namespace Task2.UI.MVC.Controllers
{
    public class BaseController : Controller
    {
        [Inject]
        public IPersonService PersonService { get; set; }

        [Inject]
        public IAccountService AccountService { get; set; }

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