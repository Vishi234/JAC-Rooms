﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace HotelSite
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            BundleTable.EnableOptimizations = true;
        }
        protected void Application_EndRequest()
        {
            if (Context.Items["AjaxPermissionDenied"] is bool)
            {
                Context.Response.StatusCode = 401;
                Context.Response.End();
            }
        }
    }
}
