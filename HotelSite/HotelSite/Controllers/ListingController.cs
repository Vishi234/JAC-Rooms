using HotelSite.Models.Agent;
using HotelSite.Models.Common;
using HotelSite.Models.Listing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelSite.Controllers
{
    public class ListingController : Controller
    {
        // GET: Listing
        public ActionResult Index()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult search(SearchHotel data)
        {
            string person = data.Person.Split(' ')[0];
            string room = data.Person.Split(' ')[2];
            data.Person = person;
            data.Room = room;
            return RedirectToAction("Index", "Listing", data);
        }
        [HttpPost]
        public JsonResult GetSearchData()
        {
            HotelInformation hInfo = new HotelInformation();
            Common objCommon = new Common();
            return Json(objCommon.DatasetToJson(hInfo.GetSearchData("")), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetHotelData(string Searchkey)
        {
            try
            {
                var hotel = getHotel(Searchkey);
                return Json(hotel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string rslt = "Not Found";
                ExceptionHandling.WriteException(ex);
                return Json(rslt, JsonRequestBehavior.AllowGet);
            }
        }
        public List<Hotel> getHotel(string key)
        {
            Hotel listing = new Hotel();
            List<Hotel> list = listing.GetHotelDetail(key);
            return list;
        }
    }
}