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
        public string HotelID { get; set; }
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
    public class HotelContactInfo
    {
        public long HotelPhone { get; set; }
        public long HotelMobile { get; set; }
        public string HotelEmail { get; set; }
        public string HotelPhoneList { get; set; }
        public string HotelEmailList { get; set; }
        public string HotelWebsiteList { get; set; }

    }

    public class AgentDetail
    {
        public string EmailID { get; set; }
        public string Password { get; set; }
        public string HotelType { get; set; }
        public string FullName { get; set; }
    }
    public class HotelRoom
    {
        public string RoomsDesc { get; set; }
        public string RoomType { get; set; }
        public string DisplayName { get; set; }
        public string TotalRoom { get; set; }
        public string BedType { get; set; }
        public string ExtraBedType { get; set; }
        public string RoomView { get; set; }
        public string MinAdult { get; set; }
        public string MaxAdult { get; set; }
        public string MinChild { get; set; }
        public string MaxChild { get; set; }
        public string MaxInfant { get; set; }
        public string MaxGuest { get; set; }
        public int IsActive { get; set; }

    }
    public class RoomPlan
    {
        public string IsRefundable { get; set; }
        public string MealPlan { get; set; }
        public string PaymentMode { get; set; }
        public string PlanName { get; set; }
        public string RoomID { get; set; }

    }

    public class HotelInformation
    {
        string sqlconn = ConfigurationManager.ConnectionStrings["DBCONN"].ConnectionString;
        public int AddHotel(HotelBasics hotelBasics)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[17];
                sqlParameter[0] = new SqlParameter("@AgentID", HttpContext.Current.Session["AgentId"].ToString());
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
                if (message == "1")
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

        public int AddHotelContactDetail(HotelContactInfo hotelContactInfo)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[8];
                sqlParameter[0] = new SqlParameter("@HotelPhone", hotelContactInfo.HotelPhone);
                sqlParameter[1] = new SqlParameter("@HotelMobile", hotelContactInfo.HotelMobile);
                sqlParameter[2] = new SqlParameter("@HotelEmail", hotelContactInfo.HotelEmail);
                sqlParameter[3] = new SqlParameter("@HotelPhoneLlist", hotelContactInfo.HotelPhoneList);
                sqlParameter[4] = new SqlParameter("@HotelEmailList", hotelContactInfo.HotelEmailList);
                sqlParameter[5] = new SqlParameter("@HotelWebsiteList", hotelContactInfo.HotelWebsiteList);
                sqlParameter[6] = new SqlParameter("@AgentID", HttpContext.Current.Session["AgentId"].ToString());
                sqlParameter[7] = new SqlParameter("@Result", SqlDbType.Int);
                sqlParameter[7].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery(sqlconn, CommandType.StoredProcedure, "sp_InsertHotelContactDetail", sqlParameter);
                string message = sqlParameter[7].Value.ToString().Trim();
                if (message == "2")
                {
                    return 2;
                }
                else
                {
                    return 1;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.WriteException(ex);
                return 0;
            }
        }

        public List<HotelBasics> GetHotelList(string AgentId)
        {

            List<HotelBasics> lst = new List<HotelBasics>();
            try
            {
                string query = "select * from tbl_Agent_Hotel where AgentID=" + AgentId;
                SqlDataReader sqlDataReader = SqlHelper.ExecuteReader(sqlconn, CommandType.Text, query);
                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        lst.Add(new HotelBasics()
                        {
                            HotelID = sqlDataReader["HotelID"].ToString(),
                            DisplayName = sqlDataReader["HotelDisplayName"].ToString(),
                        });
                    }
                }
                sqlDataReader.Close();

            }
            catch (Exception ex)
            {
                ExceptionHandling.WriteException(ex);
            }
            return lst;
        }

        public int AddRoomDetails(HotelRoom hotelRoom)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[14];
                sqlParameter[0] = new SqlParameter("@RoomsDesc", hotelRoom.RoomsDesc);
                sqlParameter[1] = new SqlParameter("@RoomType", hotelRoom.RoomType);
                sqlParameter[2] = new SqlParameter("@DisplayName", hotelRoom.DisplayName);
                sqlParameter[3] = new SqlParameter("@TotalRoom", hotelRoom.TotalRoom);
                sqlParameter[4] = new SqlParameter("@BedType", hotelRoom.BedType);
                sqlParameter[5] = new SqlParameter("@RoomView", hotelRoom.RoomView);
                sqlParameter[6] = new SqlParameter("@ExtraBedType", hotelRoom.ExtraBedType);
                sqlParameter[7] = new SqlParameter("@MinAdult", hotelRoom.MinAdult);
                sqlParameter[8] = new SqlParameter("@MinChild", hotelRoom.MinChild);
                sqlParameter[9] = new SqlParameter("@MaxAdult", hotelRoom.MaxAdult);
                sqlParameter[10] = new SqlParameter("@MaxChild", hotelRoom.MaxChild);
                sqlParameter[11] = new SqlParameter("@MaxInfant", hotelRoom.MaxInfant);
                sqlParameter[12] = new SqlParameter("@MaxGuest", hotelRoom.MaxGuest);
                sqlParameter[13] = new SqlParameter("@HotelID", "1000");
                SqlHelper.ExecuteNonQuery(sqlconn, CommandType.StoredProcedure, "sp_Add_HotelRoom", sqlParameter);
                return 1;
            }
            catch (Exception ex)
            {
                ExceptionHandling.WriteException(ex);
                return 0;
            }
        }

        public List<HotelRoom> GetRoomList(string HotelID)
        {
            List<HotelRoom> lst = new List<HotelRoom>();
            try
            {
                string query = "select * from tbl_HotelRooms";
                SqlDataReader sqlDataReader = SqlHelper.ExecuteReader(sqlconn, CommandType.Text, query);
                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        lst.Add(new HotelRoom()
                        {
                            RoomsDesc = sqlDataReader["RoomsDesc"].ToString(),
                            RoomType = sqlDataReader["RoomType"].ToString(),
                            DisplayName = sqlDataReader["RoomDisplayName"].ToString(),
                            TotalRoom = sqlDataReader["TotalRoom"].ToString(),
                            BedType = sqlDataReader["BedType"].ToString(),
                            ExtraBedType = sqlDataReader["ExtraBedType"].ToString(),
                            RoomView = sqlDataReader["RoomView"].ToString(),
                            MaxAdult = sqlDataReader["MaxAdult"].ToString(),
                            MaxChild = sqlDataReader["MaxChild"].ToString(),
                            MaxGuest = sqlDataReader["MaxGuest"].ToString(),
                            MaxInfant = sqlDataReader["MaxInfant"].ToString(),
                            MinAdult = sqlDataReader["MinAdult"].ToString(),
                            MinChild = sqlDataReader["MinChild"].ToString(),
                            IsActive = Convert.ToInt32(sqlDataReader["IsActive"]),
                        });
                    }
                }
                sqlDataReader.Close();

            }
            catch (Exception ex)
            {
                ExceptionHandling.WriteException(ex);
            }
            return lst;
        }

        public int SaveRoomPlan(RoomPlan RoomPlan)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[6];
                sqlParameter[0] = new SqlParameter("@PlanName", RoomPlan.PlanName);
                sqlParameter[1] = new SqlParameter("@MealPlan", RoomPlan.MealPlan);
                sqlParameter[2] = new SqlParameter("@PaymentMode", RoomPlan.PaymentMode);
                sqlParameter[3] = new SqlParameter("@IsRefundable", RoomPlan.IsRefundable);
                sqlParameter[4] = new SqlParameter("@RoomID", RoomPlan.RoomID);
                sqlParameter[5].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery(sqlconn, CommandType.StoredProcedure, "sp_Add_RoomWise_Plan", sqlParameter);
                return 1;
            }
            catch (Exception ex)
            {
                ExceptionHandling.WriteException(ex);
                return 0;
            }
        }
    }


}