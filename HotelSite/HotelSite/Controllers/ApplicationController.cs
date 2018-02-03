using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelSite.Controllers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class ApplicationController : ActionFilterAttribute
    {
        // GET: Application
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;

            // check if session is supported
            if (HttpContext.Current.Session["AgentId"] == null)
            {
                // check if a new session id was generated
                filterContext.Result = new RedirectResult("~/Agent/Login");
                return;
            }
            else
            {
                filterContext.Result = new RedirectResult("~/Agent/Dashboard");
            }

            base.OnActionExecuting(filterContext);

        }
    }
}