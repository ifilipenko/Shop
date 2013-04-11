using System.Web.Mvc;
using System.Web.Security;
using Shop.Areas.Security.Models;

namespace Shop.Areas.Security.Controllers
{
    public class LoginController : Controller
    {
        private const bool RememberMeByDefault = true;

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            return View(new LogOnModel
            {
                ReturnUrl  = returnUrl,
                RememberMe = RememberMeByDefault
            });
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