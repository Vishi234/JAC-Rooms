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