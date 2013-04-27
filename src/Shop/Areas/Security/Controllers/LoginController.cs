using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Shop.Areas.Security.Models;

namespace Shop.Areas.Security.Controllers
{
    public class LoginController : Controller
    {
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
                FormsAuthentication.SetAuthCookie(model.Username, model.RememberMe);
                return RedirectToAction("Index", "Home", new {area = ""});
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
