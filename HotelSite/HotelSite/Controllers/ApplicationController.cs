using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HotelSite.Controllers
{
    public class ApplicationController : AuthorizeAttribute
    {
        // GET: Application
        //public override void OnActionExecuting(FilterExecutingContext filterContext)
        //{
        //    HttpContext ctx = HttpContext.Current;

        //    // check if session is supported
        //    if (HttpContext.Current.Session["AgentId"] == null)
        //    {
        //        // check if a new session id was generated
        //        filterContext.Result = new RedirectResult("~/Agent/Login");
        //        return;
        //    }
        //    else
        //    {
        //        filterContext.Result = new RedirectResult("~/Agent/Dashboard");
        //    }

        //    base.OnActionExecuting(filterContext);

        //}
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            // check if session is supported  
            if (ctx.Session != null)
            {
                // check if a new session id was generated  
                if (ctx.Session["AgentId"] == null || ctx.Session.IsNewSession)
                {
                    //Check is Ajax request  
                    if (filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        filterContext.HttpContext.Response.ClearContent();
                        filterContext.HttpContext.Items["AjaxPermissionDenied"] = true;
                    }
                    // check if a new session id was generated  
                    else
                    {
                        filterContext.Result = new RedirectResult("~/Agent/Login");
                    }
                }
            }
            base.HandleUnauthorizedRequest(filterContext);
        }
    }
}