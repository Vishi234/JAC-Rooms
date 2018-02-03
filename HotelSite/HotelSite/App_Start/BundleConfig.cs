using System.Web.Optimization;

namespace HotelSite
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundle/jquery").Include
                (
                "~/scripts/jquery-3.2.1.min.js",
                "~/scripts/bootstrap.min.js",
                "~/scripts/jquery.min.js",
                "~/scripts/jquery-ui.min.js",
                "~/scripts/jquery-1.11.2.min.js",
                "~/scripts/bootstrap3.3.2.min.js",
                "~/scripts/jquery-1.4.2.min.js",
                "~/scripts/jquery-1.8.5.min.js",
                "~/scripts/jquery-1.9.1.min.js",
                "~/scripts/filter-tags.js",
                "~/scripts/common.js",
                "~/scripts/CommonValidator/CommonValidator.js",
                "~/scripts/jquery.js",
                "~/scripts/jquery.nice-select.js"));

            bundles.Add(new ScriptBundle("~/bundle/jquery2").Include
                (
                "~/scripts/jquery.js",
                "~/scripts/jquery.nice-select.js"));


            bundles.Add(new StyleBundle("~/Content/css").IncludeDirectory("~/Content/", "*.css", true));


            BundleTable.EnableOptimizations = true;
        }
    }
}