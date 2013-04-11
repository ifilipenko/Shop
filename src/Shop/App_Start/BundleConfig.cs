using System.Web.Optimization;

namespace Shop
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            LibsBundles(bundles);

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/bootstrap-responsive.css"));

            bundles.Add(new StyleBundle("~/Content/Admin").Include(
                "~/Content/Admin.min.css"));

            bundles.Add(new StyleBundle("~/Content/tags-css").Include(
                "~/Content/tags.css"));
            bundles.Add(new StyleBundle("~/Content/tabs-css").Include(
                "~/Content/tabs.css"));
        }

        private static void LibsBundles(BundleCollection bundles)
        {
            /*bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));*/

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            /*bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                "~/Scripts/jquery-ui-{version}.js"));*/

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js"));
        }
    }
}