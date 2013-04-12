using System.Web.Optimization;

namespace Shop
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            LibsBundles(bundles);
            ControllersBundles(bundles);

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/Site.css",
                "~/Content/bootstrap-responsive.css"));

            bundles.Add(new StyleBundle("~/Content/Admin").Include(
                "~/Content/Admin.min.css"));

            bundles.Add(new StyleBundle("~/Content/tags-css").Include(
                "~/Content/tags.css"));
            bundles.Add(new StyleBundle("~/Content/tabs-css").Include(
                "~/Content/tabs.css"));
        }

        private static void ControllersBundles(BundleCollection bundles)
        {
        }

        private static void LibsBundles(BundleCollection bundles)
        {
            /*bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));*/

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.unobtrusive*",
                "~/Scripts/jquery.validate*"));

            /*bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                "~/Scripts/jquery-ui-{version}.js"));*/

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js"));
        }
    }
}