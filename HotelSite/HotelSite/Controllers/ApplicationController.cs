using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HotelSite.Controllers
{
    public class ApplicationController : ActionFilterAttribute
    {
        // GET: Application
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;

            // check if session is supported
            if (HttpContext.Current.Session["AgentId"] == null)
            {
                // check if a new session id was generated
                filterContext.Result = new RedirectResult("~/Agent/Login");
                return;
            }
            
            base.OnActionExecuted(filterContext);

        }


    }
}