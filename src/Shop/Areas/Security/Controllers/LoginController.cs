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
            if (ModelState.IsValid)
            {
                FormsAuthentication.SetAuthCookie(model.Username, false);
                return RedirectToAction("Index", "Home", new {area = ""});
            }

            return View("Login", model);
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}