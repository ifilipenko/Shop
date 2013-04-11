using System.Web.Mvc;

namespace Shop.Areas.Security.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult LogOn()
        {
            return RedirectToAction("Index", "Home", new {area = ""});
        }
    }
}