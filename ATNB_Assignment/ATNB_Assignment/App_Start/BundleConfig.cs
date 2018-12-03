using System.Web;
using System.Web.Optimization;

namespace ATNB_Assignment
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}js",
            //            "~/Scripts/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/fontawesome").Include(
                        "~/Scripts/fontawesome.js",
                        "~/Scripts/fontawesome.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/sidebar").Include(
                        "~/Scripts/sidebarmenu.js"));

            bundles.Add(new ScriptBundle("~/bundles/wave").Include(
                        "~/Scripts/waves.js"));

            bundles.Add(new ScriptBundle("~/bundles/kit").Include(
                        "~/Scripts/sticky-kit.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/frontjs").Include(
                        "~/Scripts/jquery.min.js",
                         "~/Scripts/popper.min.js",
                         "~/Scripts/fontawesome.js",
                         "~/Scripts/bootstrap.min.js",
                        "~/Scripts/fontawesome.min.js",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/custom").Include(
                        "~/Scripts/popper.min.js",
                        "~/Scripts/custom.min.js",
                        "~/Scripts/dashboard1.js",
                        "~/Scripts/jquery.sparkline.min.js",
                        "~/Scripts/jquery.slimscroll.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      //"~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Content/frontcss").Include(
                      "~/Scripts/popper.min.js",
                      "~/Content/fontawesome.css",
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap.min.css",
                      "~/Content/style.css",
                      "~/Content/bootstrap.min.css"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/fontawesome.css",
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap.min.css",
                      "~/Content/green-dark.css",
                      "~/Content/style.css",
                      "~/Content/themify-icons.css"));
        }
    }
}
