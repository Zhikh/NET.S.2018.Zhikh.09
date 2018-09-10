using System.Web.Mvc;

namespace Task2.UI.MVC.Infrastructure.ActionResults
{
    public class NotFoundResult : ActionResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.StatusCode = 404;
            new ViewResult { ViewName = "Error" }.ExecuteResult(context);
        }
    }
}