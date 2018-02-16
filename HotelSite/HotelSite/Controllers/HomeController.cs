using HotelSite.Models.Agent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelSite.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult search(SearchHotel data)
        {
            string person = data.Person.Split(' ')[0];
            string room= data.Person.Split(' ')[2];
            data.Person = person;
            data.Room = room;
            return RedirectToAction("Index", "Listing", data);
        }
    }
}