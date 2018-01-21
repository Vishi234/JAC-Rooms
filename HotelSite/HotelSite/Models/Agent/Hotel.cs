using HotelSite.Models.Common;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
        public string HotelBuiltYear { get; set; }
        public int HotelRooms { get; set; }
        public string HotelCheckInTime { get; set; }
        public string HotelCheckOutTime { get; set; }
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
                SqlParameter[] sqlParameter = new SqlParameter[17];
                sqlParameter[0] = new SqlParameter("@AgentID", HttpContext.Current.Session["UserID"].ToString());
                sqlParameter[1] = new SqlParameter("@HotelName", hotelBasics.HotelName);
                sqlParameter[2] = new SqlParameter("@HotelDisplay", hotelBasics.DisplayName);
                sqlParameter[3] = new SqlParameter("@HotelStar", hotelBasics.HotelStar);
                sqlParameter[4] = new SqlParameter("@HotelFloor", hotelBasics.HotelFloor);
                sqlParameter[5] = new SqlParameter("@HotelBuildYear", hotelBasics.HotelBuiltYear);
                sqlParameter[6] = new SqlParameter("@HotelRooms", hotelBasics.HotelRooms);
                sqlParameter[7] = new SqlParameter("@CheckInTime", hotelBasics.HotelCheckInTime);
                sqlParameter[8] = new SqlParameter("@CheckOutTime", hotelBasics.HotelCheckOutTime);
                sqlParameter[9] = new SqlParameter("@HotelType", hotelBasics.HotelType);
                sqlParameter[10] = new SqlParameter("@StreetAddress", hotelBasics.StreetAdress);
                sqlParameter[11] = new SqlParameter("@Country", hotelBasics.Country);
                sqlParameter[12] = new SqlParameter("@State", hotelBasics.State);
                sqlParameter[13] = new SqlParameter("@City", hotelBasics.City);
                sqlParameter[14] = new SqlParameter("@Locality", hotelBasics.Locality);
                sqlParameter[15] = new SqlParameter("@ZipCode", hotelBasics.ZipCode);
                sqlParameter[16] = new SqlParameter("@Result", SqlDbType.Int);
                sqlParameter[16].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery(sqlconn, CommandType.StoredProcedure, "sp_InsertAgentHotel", sqlParameter);
                string message = sqlParameter[16].Value.ToString().Trim();
                if(message == "1")
                {
                    return 2;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.WriteException(ex);
                return 0;
            }
        }
    }
}