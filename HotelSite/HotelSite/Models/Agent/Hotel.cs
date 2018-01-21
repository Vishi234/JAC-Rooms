using HotelSite.Models.Common;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
        public TimeSpan HotelCheckInTime { get; set; } = new TimeSpan();
        public TimeSpan HotelCheckOutTime { get; set; }
        public string StreetAdress { get; set; }
        public string Locality { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
        public int AgentID { get; set; }

    }

    public class HotelInformation
    {
        public int AddHotel(HotelBasics hotelBasics)
        {
            string sqlconn = ConfigurationManager.ConnectionStrings["DBCONN"].ConnectionString;
            try
            {
                SqlParameter sqlParameter = new SqlParameter();
                sqlParameter.Direction = System.Data.ParameterDirection.Output;
                sqlParameter.DbType = System.Data.DbType.Int16;
                sqlParameter.ParameterName = "@Result";


                List<SqlParameter> lstsqlparam = new List<SqlParameter>();
                lstsqlparam.Add(new SqlParameter("@AgentID", HttpContext.Current.Session["UserID"].ToString()));
                lstsqlparam.Add(new SqlParameter("@HotelName", hotelBasics.HotelName));
                lstsqlparam.Add(new SqlParameter("@HotelDisplay", hotelBasics.DisplayName));
                lstsqlparam.Add(new SqlParameter("@HotelStar", hotelBasics.HotelStar));
                lstsqlparam.Add(new SqlParameter("@HotelFloor", hotelBasics.HotelFloor));
                lstsqlparam.Add(new SqlParameter("@HotelBuildYear", hotelBasics.HotelBuiltYear));
                lstsqlparam.Add(new SqlParameter("@HotelRooms", hotelBasics.HotelRooms));
                lstsqlparam.Add(new SqlParameter("@CheckInTime", hotelBasics.HotelCheckInTime));
                lstsqlparam.Add(new SqlParameter("@CheckOutTime", hotelBasics.HotelCheckOutTime));
                lstsqlparam.Add(new SqlParameter("@HotelType", hotelBasics.HotelType));
                lstsqlparam.Add(new SqlParameter("@StreetAddress", hotelBasics.StreetAdress));
                lstsqlparam.Add(new SqlParameter("@Country", hotelBasics.Country));
                lstsqlparam.Add(new SqlParameter("@State", hotelBasics.State));
                lstsqlparam.Add(new SqlParameter("@City", hotelBasics.City));
                lstsqlparam.Add(new SqlParameter("@Locality", hotelBasics.Locality));
                lstsqlparam.Add(new SqlParameter("@ZipCode", hotelBasics.ZipCode));
                lstsqlparam.Add(sqlParameter);
                SqlHelper.ExecuteNonQuery(sqlconn, "sp_InsertAgentHotel", lstsqlparam.ToArray());
                int a = (int)lstsqlparam[16].Value;
                return a;
            }
            catch (Exception ex)
            {
                ExceptionHandling.WriteException(ex);
                return 0;
            }
        }
    }
}