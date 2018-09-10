using System.Web.Mvc;
using Task2.UI.MVC.Infrastructure.ActionResults;

namespace Task2.UI.MVC.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult NotFound()
        {
            return new NotFoundResult();
        }
    }
}