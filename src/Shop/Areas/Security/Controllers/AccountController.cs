using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            return RedirectToAction("Index", "Home", new {area = ""});
        }
    }
}
