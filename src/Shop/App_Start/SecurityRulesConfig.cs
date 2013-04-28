using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FluentSecurity;
using Shop.Areas.Security.Controllers;
using Shop.Controllers;

namespace Shop.App_Start
{
    public class SecurityRulesConfig
    {
        public static void Initialize(GlobalFilterCollection filters)
        {
            SecurityConfigurator.Configure(x =>
                {
                    x.GetAuthenticationStatusFrom(() => HttpContext.Current.User.Identity.IsAuthenticated);
                    x.GetRolesFrom(() =>
                        {
                            if (HttpContext.Current.User == null)
                                return Enumerable.Empty<object>();
                            var username = HttpContext.Current.User.Identity.Name;
                            return Roles.GetRolesForUser(username);
                        });

                    x.ForAllControllers().DenyAnonymousAccess();
                    //x.For<ProductController>().RequireRole("Администратор");
                    x.For<HomeController>().Ignore();
                    x.For<AccountController>().Ignore();
                    x.For<LoginController>().Ignore();
                    x.For<LoginController>(c => c.LogOff()).DenyAnonymousAccess();
                });

            filters.Add(new HandleSecurityAttribute(), 0);
        }
    }
}