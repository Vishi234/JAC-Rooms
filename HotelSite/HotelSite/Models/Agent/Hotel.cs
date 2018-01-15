using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelSite.Models.Agent
{
    public class HotelBasics
    {
        public string HotelName { get; set; }
        public int HotelFloor { get; set; }
        public string HotelType { get; set; }
        public string DisplayName { get; set; }
        public int HotelStar { get; set; }
        public DateTime HotelBuiltYear { get; set; }
        public int HotelRooms { get; set; }
        public DateTime HotelCheckInTime { get; set; }
        public DateTime HotelCheckOutTime { get; set; }
        public string StreetAdress { get; set; }
        public string Locality { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
        public int AgentID { get; set; }

    }
}