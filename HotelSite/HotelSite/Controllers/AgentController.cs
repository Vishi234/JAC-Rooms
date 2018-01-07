using HotelSite.Models.Common;
using HotelSite.Models.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelSite.Controllers
{
    public class AgentController : Controller
    {
        // GET: Agent
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult register()
        {
            return View();
        }
        public ActionResult property()
        {
            return View();
        }
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterAgent(FormCollection formCollection)
        {
            string Email = formCollection["EmailID"].ToString();
            string Password = formCollection["Password"].ToString();
            string HotelType = formCollection["HotelType"].ToString();
            string FullName = formCollection["FullName"].ToString();
            Agent agent = new Agent();
            string Guid = agent.CreateAgent(Email, Password, HotelType, FullName);
            string body = "Hello " + FullName + ",";
            body += "<br /><br />Please click the following link to activate your account";
            body += "<br /><a href = '" + string.Format("{0}://{1}/Agent/AgentComfirmation/{2}", Request.Url.Scheme, Request.Url.Authority, Guid) + "'>Click here to activate your account.</a>";
            body += "<br /><br />Thanks";
            Common.SendEmail(body, "clickhere");
            return View("UserValidation");
        }

        public ViewResult AgentComfirmation()
        {
            Guid activationCode = new Guid(RouteData.Values["id"].ToString());
            Agent agent = new Agent();
            int rslt = agent.CheckAgent(activationCode);
            if (rslt == 1) return View("Index");
            else
            {
                ViewBag.Result = rslt;
                return View("UserValidation");
            }
        }
    }
}