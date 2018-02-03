using HotelSite.Models.Common;
using HotelSite.Models.Login;
using System;
using System.Web.Mvc;
using HotelSite.Models.Agent;

namespace HotelSite.Controllers
{
    [ApplicationController]
    public class AgentController : Controller
    {
       
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult login()
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

        [HttpPost]
        public ActionResult LoggingIN(Signin signin)
        {
            Register register = new Register();
            signin.IsAgent = true;
            if (register.SignIn(signin))
            {
                return View("Index");
            }
            else
            {
                return View("Login");
            }
        }
        [HttpPost]
        public string RegisterAgent(AgentDetail agentDetail)
        {
            try
            {
                string Email = agentDetail.EmailID;
                string Password = agentDetail.Password;
                string HotelType = agentDetail.HotelType;
                string FullName = agentDetail.FullName;
                Agent agent = new Agent();
                string Guid = agent.CreateAgent(Email, Password, HotelType, FullName);
                string body = "Hello " + FullName + ",";
                body += "<br /><br />Please click the following link to activate your account";
                body += "<br /><a href = '" + string.Format("{0}://{1}/Agent/AgentComfirmation/{2}", Request.Url.Scheme, Request.Url.Authority, Guid) + "'>Click here to activate your account.</a>";
                body += "<br /><br />Thanks";
                Common.SendEmail(body, "clickhere", Email);
                return "1";

            }
            catch (Exception ex)
            {
                ExceptionHandling.WriteException(ex);
                return "-1";
            }
        }
        public ViewResult AgentComfirmation()
        {
            Guid activationCode = new Guid(RouteData.Values["id"].ToString());
            Agent agent = new Agent();
            int rslt = agent.CheckAgent(activationCode);
            if (rslt == 1)
            {
                return View("Index");
            }
            else
            {
                ViewBag.Result = rslt;
                return View("UserValidation");
            }
        }

        [HttpPost]
        public int SaveHotelBasics(HotelBasics hotelBasics)
        {
            HotelInformation hotelInformation = new HotelInformation();
            return hotelInformation.AddHotel(hotelBasics);
        }

        public int SaveHotelContact(HotelContactInfo hotelContactInfo)
        {
            HotelInformation hotelInformation = new HotelInformation();
            return hotelInformation.AddHotelContactDetail(hotelContactInfo);
        }
    }
}