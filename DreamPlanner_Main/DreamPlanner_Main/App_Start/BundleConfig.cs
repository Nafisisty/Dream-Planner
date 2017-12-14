using System.Web;
using System.Web.Optimization;

namespace DreamPlanner_Main
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/assets/js/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/assets/js/modernizr.custom.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/assets/js/bootstrap.min.js",
                      "~/assets/js/scripts.js",
                      "~/assets/js/jquery.isotope.js",
                      "~/assets/js/jquery.slicknav.js",
                      "~/assets/js/jquery.visible.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/assets/css/bootstrap.css",
                      "~/assets/css/animate.css",
                      "~/assets/css/font-awesome.css",
                      "~/assets/css/nexus.css",
                      "~/assets/css/responsive.css",
                      "~/assets/css/custom.css"));
        }
    }
}
