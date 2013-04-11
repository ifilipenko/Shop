using System.Web.Mvc;
using System.Web.Security;
using Shop.Areas.Security.Models;

namespace Shop.Areas.Security.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Login()
        {
            return View("Login");
        }

        public ActionResult CompactLogin()
        {
            return View("CompactLogin");
        }

        public ActionResult LogOn(LogOnModel model)
        {
            FormsAuthentication.SetAuthCookie(model.Username, false);
            return RedirectToAction("Index", "Home", new {area = ""});
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}