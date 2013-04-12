using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Shop.Areas.Security.Models;

namespace Shop.Areas.Security.Controllers
{
    public class AccountController : Controller
    {
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
                    Membership.CreateUser(model.UserName, model.Password);
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
