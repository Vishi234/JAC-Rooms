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
            bundles.Add(new ScriptBundle("~/bundle/jquery").Include("~/scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/Content/css").IncludeDirectory("~/Content/", "*.css", true));
            BundleTable.EnableOptimizations = true;
        }
    }
}