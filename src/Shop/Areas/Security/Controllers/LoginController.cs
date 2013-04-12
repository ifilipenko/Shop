using System.Web.Mvc;
using System.Web.Security;
using Shop.Areas.Security.Models;

namespace Shop.Areas.Security.Controllers
{
    public class LoginController : Controller
    {
        private const bool RememberMeByDefault = true;

        [HttpGet]
        public ActionResult Login()
        {
            return View(new LogOnModel
            {
                RememberMe = RememberMeByDefault
            });
        }

        [HttpGet]
        public ActionResult CompactLogin()
        {
            return View("CompactLogin");
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model)
        {
            FormsAuthentication.SetAuthCookie(model.Username, false);
            return RedirectToAction("Index", "Home", new {area = ""});
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}