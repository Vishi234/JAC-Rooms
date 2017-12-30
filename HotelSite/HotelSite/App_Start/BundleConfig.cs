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
            // create an object of ScriptBundle and 
            // specify bundle name (as virtual path) as constructor parameter 
            var jsPath1 = "http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js";
            var jsPath2 = "http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.5/jquery-ui.min.js";
            var cssPath1 = "https://fonts.googleapis.com/css?family=Montserrat:100,100i,200,200i,300,300i,400,400i,500,500i,600,600i,700,700i,800";

            ScriptBundle javascript = new ScriptBundle("~/bundles/scripts");
            StyleBundle style = new StyleBundle("~/bundles/Content");


            //use Include() method to add all the script files with their paths 
            javascript.Include(
                                "~/scripts/jquery-3.2.1.min.js",
                                "~/scripts/bootstrap.min.js"
                              );

            style.Include(
                "~/Content/bootstrap.min.css",
                "~/Content/font-awesome.min.css",
                "~/Content/Style.css");

            bundles.Add(new StyleBundle("~/bundles/Content", cssPath1)
            .Include("~/Content/fontstyle.css"));

            bundles.Add(new ScriptBundle("~/bundles/scripts", jsPath1)
               .Include("~/scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts", jsPath2)
             .Include("~/scripts/jquery-{version}.js"));

            //Add the bundle into BundleCollection
            bundles.Add(javascript);
            bundles.Add(style);
            BundleTable.EnableOptimizations = true;
        }
    }
}