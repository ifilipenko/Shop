using System;
using System.Security.Authentication;
using System.Web.Mvc;
using System.Web.Security;
using Shop.Areas.Security.Models;
using Shop.Services.Security;

namespace Shop.Areas.Security.Controllers
{
    public class LoginController : Controller
    {
        private const bool RememberMeByDefault = true;
        private readonly IAuthenticationService _authenticationService;

        public LoginController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

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
            return View("_CompactLogin");
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _authenticationService.SignIn(model.Username, model.Password, model.RememberMe);
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
                catch (AuthenticationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
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