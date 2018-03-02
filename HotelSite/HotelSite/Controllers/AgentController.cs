using HotelSite.Models.Common;
using HotelSite.Models.Login;
using System;
using System.Web.Mvc;
using HotelSite.Models.Agent;
using HotelSite.Models.Listing;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Configuration;

namespace HotelSite.Controllers
{

    public class AgentController : Controller
    {
        [ApplicationController]
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
        [ApplicationController]
        public ActionResult property()
        {
            return View();
        }
        public ActionResult logout()
        {
            Session.Clear();
            Session.Abandon();
            return View("login");
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
                return View("login");
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
        public int SaveHotelRoom(HotelRoom hotelRoom)
        {
            HotelInformation hotelInformation = new HotelInformation();
            return hotelInformation.AddRoomDetails(hotelRoom);
        }
        public int DeleteRoom(string RoomID)
        {
            HotelInformation hotelInformation = new HotelInformation();
            return  hotelInformation.DeleteRoom(RoomID);
        }
        public JsonResult GetHotelList(string agentId)
        {
            HotelInformation hotelObj = new HotelInformation();
            List<HotelBasics> list = hotelObj.GetHotelList(agentId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetHotelDetails(string HotelID)
        {
            HotelInformation hotelDetails = new HotelInformation();

            return Json("", JsonRequestBehavior.AllowGet);
        }
        public ActionResult Inventory()
        {
            return View();
        }
        [HttpPost]
        public int UploadHomeReport(string id, string hotelName)
        {
            try
            {
                string drive = ConfigurationManager.AppSettings["PicDrive"];
                string folderName = ConfigurationManager.AppSettings["FolderName"];
                folderName = folderName + "/" + hotelName;
                string driveToSave = Path.Combine(drive, folderName);
                HotelInformation hInfo = new HotelInformation();
                RoomImages roomImg = new RoomImages();
                int result = 0;
                if (CheckDirectory(driveToSave))
                {
                    if (Request.Files.Count > 0)
                    {
                        foreach (string file in Request.Files)
                        {
                            HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                            hpf.SaveAs(Path.Combine(driveToSave, hpf.FileName));
                            roomImg.PicName = hpf.FileName;
                            roomImg.RoomID = Convert.ToInt32(id);
                            roomImg.flag = 1;
                            result = hInfo.SaveRoomImages(roomImg);
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandling.WriteException(ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return 0;
            }


        }
        private bool CheckDirectory(string directory)
        {
            try
            {
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                    return true;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.WriteException(ex);
                return false;
            }
        }


        public JsonResult GetRoomList(String HotelID)
        {
            HotelInformation hInfo = new HotelInformation();

            return Json(hInfo.GetRoomList(HotelID), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public int SaveRoomPlan(RoomPlan Details)
        {
            HotelInformation hotelInfo = new HotelInformation();
            return hotelInfo.SaveRoomPlan(Details);
        }
        [HttpPost]
        public JsonResult GetHotelData(string HotelID)
        {
            HotelInformation hInfo = new HotelInformation();
            Common objCommon = new Common();
            return Json(objCommon.DatasetToJson(hInfo.GetHotelData(HotelID)), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetRoomWisePLan(string RoomID)
        {
            HotelInformation hotelDetails = new HotelInformation();

            return Json(hotelDetails.GetRoomWisePlan(RoomID), JsonRequestBehavior.AllowGet);
        }
    }
}