using System.Web.Mvc;

namespace Task2.UI.MVC.Infrastructure.HtmlHelpers
{
    public static class MessageHelper
    {
        public static string DisplayMessage(this HtmlHelper html, string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return "";
            }

            return "@Html.Partial(\"_Message\")";
        }
    }
}