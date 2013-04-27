using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Shop.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/bootstrap").Include(
                "~/Scripts/bootstrap.js"));
            bundles.Add(new ScriptBundle("~/Scripts/jquery").Include(
                "~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/Scripts/jqueryval").Include(
                "~/Scripts/jquery.validate.js",
                "~/Scripts/jquery.validate.unobtrusive.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/Site.css",
                "~/Content/bootstrap-responsive.css"));

            /*bundles.Add(new StyleBundle("~/Content/Admin").Include(
                "~/Content/Admin.min.css"));

            bundles.Add(new StyleBundle("~/Content/tags-css").Include(
                "~/Content/tags.css"));
            bundles.Add(new StyleBundle("~/Content/tabs-css").Include(
                "~/Content/tabs.css"));*/
        }
    }
}