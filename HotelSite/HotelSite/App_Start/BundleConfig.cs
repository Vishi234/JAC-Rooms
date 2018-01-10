using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace HotelSite
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundle/jquery").Include("~/scripts/jquery-1.4.2.min.js",
                "~/scripts/jquery-1.8.5.min.js",
                "~/scripts/jquery-1.11.2.min.js",
                "~/scripts/jquery-1.9.1.min.js",
                "~/scripts/jquery-3.2.1.min.js"));



            bundles.Add(new StyleBundle("~/Content/css").IncludeDirectory("~/Content/", "*.css", true));


            BundleTable.EnableOptimizations = true;
        }
    }
}