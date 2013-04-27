using System.Web.Mvc;
using System.Web.Security;
using Shop.Areas.Security.Models;
using Shop.Services.Infrastructure;

namespace Shop.Areas.Security.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public AccountController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _authenticationService.CreateUser(model.Username, model.Password);
                    return RedirectToAction("Index", "Home", new {area = ""});
                }
                catch (MembershipCreateUserException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(model);
        }
    }
}
