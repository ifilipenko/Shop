using System.Security.Authentication;
using System.Web.Mvc;
using System.Web.Security;
using Shop.Areas.Security.Models;
using Shop.Services.Infrastructure;

namespace Shop.Areas.Security.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public LoginController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet]
        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _authenticationService.Authenticate(model.Username, model.Password, model.RememberMe);
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
                catch (AuthenticationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home", new {area = ""});
        }
    }
}
