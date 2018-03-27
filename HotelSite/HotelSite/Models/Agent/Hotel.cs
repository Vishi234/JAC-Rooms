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

        public string flag { get; set; }


    }
    public class HotelContactInfo
    {
        public long HotelPhone { get; set; }
        public long HotelMobile { get; set; }
        public string HotelEmail { get; set; }
        public string HotelPhoneList { get; set; }
        public string HotelEmailList { get; set; }
        public string HotelWebsiteList { get; set; }
        public string HotelID { get; set; }
        public string flag { get; set; }

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
        public int ID { get; set; }
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
        public string PricePerNight { get; set; }
        public int HotelID { get; set; }
        public string flag { get; set; }

    }
    public class RoomPlan
    {
        public string IsRefundable { get; set; }
        public string MealPlan { get; set; }
        public string PaymentMode { get; set; }
        public string PlanName { get; set; }
        public string RoomID { get; set; }
        public string PlanID { get; set; }
        public string flag { get; set; }

    }

    public class SearchHotel
    {
        public string Keyword { get; set; }
        public string CheckIn { get; set; }
        public string CheckOut { get; set; }
        public string Person { get; set; }
        public string Room { get; set; }

    }
    public class RoomImages
    {
        public string RoomID { get; set; }
        public string PicName { get; set; }
        public int IsEnable { get; set; }
        public int HotelID { get; set; }
        public string Flag { get; set; }
    }
    public class RoomInventory
    {
        public int RoomID { get; set; }
        public string RoomType { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string DaysOfWeek { get; set; }
        public int Available { get; set; }
        public int Sold { get; set; }
        public int Block { get; set; }
        public int SoldOut { get; set; }
        public int HotelID { get; set; }
        public int AgentID { get; set; }

    }
    public class HotelInformation
    {
        string sqlconn = ConfigurationManager.ConnectionStrings["DBCONN"].ConnectionString;

        public int AddHotel(HotelBasics hotelBasics)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[20];
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
                sqlParameter[16] = new SqlParameter("@hotelID", hotelBasics.HotelID);
                sqlParameter[17] = new SqlParameter("@Flag", hotelBasics.flag);
                sqlParameter[18] = new SqlParameter("@Result", SqlDbType.Int);
                sqlParameter[18].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery(sqlconn, CommandType.StoredProcedure, "sp_InsertAgentHotel", sqlParameter);
                string message = sqlParameter[18].Value.ToString().Trim();
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
                SqlParameter[] sqlParameter = new SqlParameter[10];
                sqlParameter[0] = new SqlParameter("@HotelPhone", hotelContactInfo.HotelPhone);
                sqlParameter[1] = new SqlParameter("@HotelMobile", hotelContactInfo.HotelMobile);
                sqlParameter[2] = new SqlParameter("@HotelEmail", hotelContactInfo.HotelEmail);
                sqlParameter[3] = new SqlParameter("@HotelPhoneLlist", hotelContactInfo.HotelPhoneList);
                sqlParameter[4] = new SqlParameter("@HotelEmailList", hotelContactInfo.HotelEmailList);
                sqlParameter[5] = new SqlParameter("@HotelWebsiteList", hotelContactInfo.HotelWebsiteList);
                sqlParameter[6] = new SqlParameter("@AgentID", HttpContext.Current.Session["AgentId"].ToString());
                sqlParameter[7] = new SqlParameter("@HotelID", Convert.ToInt32(hotelContactInfo.HotelID));
                sqlParameter[8] = new SqlParameter("@flag", hotelContactInfo.flag);
                sqlParameter[9] = new SqlParameter("@Result", SqlDbType.Int);
                sqlParameter[9].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery(sqlconn, CommandType.StoredProcedure, "sp_InsertHotelContactDetail", sqlParameter);
                string message = sqlParameter[9].Value.ToString().Trim();
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
                SqlParameter[] sqlParameter = new SqlParameter[17];
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
                sqlParameter[13] = new SqlParameter("@PricePerNight", hotelRoom.PricePerNight);
                sqlParameter[14] = new SqlParameter("@HotelID", hotelRoom.HotelID);
                sqlParameter[15] = new SqlParameter("@Flag", hotelRoom.flag);
                sqlParameter[16] = new SqlParameter("@RoomID", hotelRoom.ID);
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
                string query = "select * from tbl_HotelRooms where HotelID=" + HotelID;
                SqlDataReader sqlDataReader = SqlHelper.ExecuteReader(sqlconn, CommandType.Text, query);
                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        lst.Add(new HotelRoom()
                        {
                            ID = Convert.ToInt32(sqlDataReader["ID"]),
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
                            PricePerNight = sqlDataReader["PricePerNight"].ToString(),
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

        public int SaveRoomImages(RoomImages RoomImages)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[5];
                sqlParameter[0] = new SqlParameter("@RoomID", RoomImages.RoomID);
                sqlParameter[1] = new SqlParameter("@PicName", RoomImages.PicName);
                sqlParameter[2] = new SqlParameter("@IsEnable", RoomImages.IsEnable);
                sqlParameter[3] = new SqlParameter("@HotelID", RoomImages.HotelID);
                sqlParameter[4] = new SqlParameter("@flag", RoomImages.Flag);
                SqlHelper.ExecuteNonQuery(sqlconn, CommandType.StoredProcedure, "sp_SaveHotelWisePic", sqlParameter);
                return 1;
            }
            catch (Exception ex)
            {
                ExceptionHandling.WriteException(ex);
                return 0;
            }
        }
        public DataSet GetHotelImages(string HotelID)
        {
            DataSet ds = new DataSet();
            try
            {

                string query = "select r.RoomID, r.PicName, r.PicID, r.IsEnable, r.HotelID, u.RoomDisplayName from tbl_HotelPics r LEFT JOIN tbl_HotelRooms u on r.RoomID = u.ID where r.HotelID=" + HotelID;
                ds = SqlHelper.ExecuteDataset(sqlconn, CommandType.Text, query);
            }
            catch (Exception ex)
            {
                ExceptionHandling.WriteException(ex);
            }
            return ds;
        }
        public int EnableImage(string ImageID, string Status)
        {
            try
            {
                string query = "update tbl_hotelpics set IsEnable=" + Convert.ToInt32(Status) + "where PicID=" + ImageID;
                SqlHelper.ExecuteNonQuery(sqlconn, CommandType.Text, query);
                return 1;
            }
            catch (Exception ex)
            {
                ExceptionHandling.WriteException(ex);
                return 0;
            }
        }
        public int RemoveImage(string ImageID)
        {
            try
            {
                string query = "delete from tbl_hotelpics where PicID=" + ImageID;
                SqlHelper.ExecuteScalar(sqlconn, CommandType.Text, query);
                return 1;
            }
            catch (Exception ex)
            {
                ExceptionHandling.WriteException(ex);
                return 0;
            }
        }
        public int SaveRoomPlan(RoomPlan RoomPlan)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[7];
                sqlParameter[0] = new SqlParameter("@PlanName", RoomPlan.PlanName);
                sqlParameter[1] = new SqlParameter("@MealPlan", RoomPlan.MealPlan);
                sqlParameter[2] = new SqlParameter("@PaymentMode", RoomPlan.PaymentMode);
                sqlParameter[3] = new SqlParameter("@IsRefundale", RoomPlan.IsRefundable);
                sqlParameter[4] = new SqlParameter("@RoomID", RoomPlan.RoomID);
                sqlParameter[5] = new SqlParameter("@PlanID", RoomPlan.PlanID);//Pla
                sqlParameter[6] = new SqlParameter("@Flag", RoomPlan.flag);//Pla
                SqlHelper.ExecuteNonQuery(sqlconn, CommandType.StoredProcedure, "sp_Add_RoomWise_Plan", sqlParameter);
                return 1;
            }
            catch (Exception ex)
            {
                ExceptionHandling.WriteException(ex);
                return 0;
            }
        }

        public DataSet GetHotelData(string HotelID)
        {
            DataSet ds = new DataSet();
            try
            {
                string query = "select * from tbl_Agent_Hotel where HotelID=" + HotelID + ";select * from tbl_Agent_Hotel_Contact where HotelID=" + HotelID + ";select * from tbl_HotelRooms where HotelID=" + HotelID;
                ds = SqlHelper.ExecuteDataset(sqlconn, CommandType.Text, query);
                ds.Tables[0].TableName = "BasicInfo";
                ds.Tables[1].TableName = "ContactInfo";
                ds.Tables[2].TableName = "RoomInfo";
            }
            catch
            {

            }
            return ds;
        }

        public DataSet GetSearchData(SearchHotel Search)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[3];
                sqlParameter[0] = new SqlParameter("@hotelname", Search.Keyword);
                sqlParameter[1] = new SqlParameter("@checkin", Search.CheckIn);
                sqlParameter[2] = new SqlParameter("@checkout", Search.CheckOut);
                ds = SqlHelper.ExecuteDataset(sqlconn, CommandType.StoredProcedure, "sp_Get_Hotel_Listing", sqlParameter);
                ds.Tables[0].TableName = "HotelName";
                ds.Tables[1].TableName = "HotelPics";
                ds.Tables[2].TableName = "HotelPrice";
            }
            catch(Exception ex)
            {
                ExceptionHandling.WriteException(ex);
            }
            return ds;
        }

        public List<RoomPlan> GetRoomWisePlan(string RoomID)
        {
            List<RoomPlan> lst = new List<RoomPlan>();
            try
            {
                string query = "select * from tbl_Add_RoomWisePlan where RoomID=" + RoomID;
                SqlDataReader sqlDataReader = SqlHelper.ExecuteReader(sqlconn, CommandType.Text, query);
                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        lst.Add(new RoomPlan()
                        {
                            PlanName = sqlDataReader["PlanName"].ToString(),
                            MealPlan = sqlDataReader["MealPlan"].ToString(),
                            PaymentMode = sqlDataReader["PaymentMode"].ToString(),
                            IsRefundable = sqlDataReader["IsRefundale"].ToString(),
                            PlanID = sqlDataReader["PlanID"].ToString(),
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

        public int DeleteRoom(string RoomID)
        {
            try
            {
                string query = "delete tbl_HotelRooms where ID=" + Convert.ToInt32(RoomID);
                SqlHelper.ExecuteScalar(sqlconn, CommandType.Text, query);
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public List<HotelRoom> GetRoomForInventory(string HotelID)
        {
            List<HotelRoom> lst = new List<HotelRoom>();
            try
            {
                string query = "select * from tbl_HotelRooms where HotelID=" + HotelID;
                SqlDataReader sqlDataReader = SqlHelper.ExecuteReader(sqlconn, CommandType.Text, query);
                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        lst.Add(new HotelRoom()
                        {
                            DisplayName = sqlDataReader["RoomDisplayName"].ToString(),
                            TotalRoom = sqlDataReader["TotalRoom"].ToString(),
                            PricePerNight = sqlDataReader["PricePerNight"].ToString(),
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

        public int SaveRoomInventory(RoomInventory roomInventory)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[8];
                sqlParameter[0] = new SqlParameter("@RoomID", roomInventory.RoomID);
                sqlParameter[1] = new SqlParameter("@RoomType", roomInventory.RoomType);
                sqlParameter[2] = new SqlParameter("@HotelID", roomInventory.HotelID);
                sqlParameter[3] = new SqlParameter("@AgentID", HttpContext.Current.Session["AgentId"].ToString());
                sqlParameter[4] = new SqlParameter("@StartDate", roomInventory.StartDate);
                sqlParameter[5] = new SqlParameter("@EndDate", roomInventory.EndDate);
                sqlParameter[6] = new SqlParameter("@DaysOfWeek", roomInventory.DaysOfWeek);
                sqlParameter[7] = new SqlParameter("@Available", roomInventory.Available);
                SqlHelper.ExecuteNonQuery(sqlconn, CommandType.StoredProcedure, "sp_ManageRoomInventory", sqlParameter);
                return 1;
            }
            catch (Exception ex)
            {
                ExceptionHandling.WriteException(ex);
                return 0;
            }
        }

        public List<RoomInventory> GetRoomInventory(string HotelID)
        {
            List<RoomInventory> lst = new List<RoomInventory>();
            try
            {
                string query = "select * from tbl_RoomInventory where HotelID=" + HotelID;
                SqlDataReader sqlDataReader = SqlHelper.ExecuteReader(sqlconn, CommandType.Text, query);
                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        lst.Add(new RoomInventory()
                        {
                            RoomID = Convert.ToInt32(sqlDataReader["RoomID"]),
                            RoomType = sqlDataReader["RoomType"].ToString(),
                            StartDate = sqlDataReader["StartDate"].ToString(),
                            EndDate = sqlDataReader["EndDate"].ToString(),
                            Available = Convert.ToInt32(sqlDataReader["Available"]),
                            Sold = Convert.ToInt32(sqlDataReader["Sold"]),
                            Block = Convert.ToInt32(sqlDataReader["Block"]),
                            SoldOut = Convert.ToInt32(sqlDataReader["SoldOut"]),
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

        public int UpdateInventory(string inventDate,int RoomID,int Available)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[3];
                string query = "update tbl_RoomInventory set Available=@avail where StartDate=@sDate and RoomID=@RID";
                sqlParameter[0] = new SqlParameter("@sDate", inventDate);
                sqlParameter[1] = new SqlParameter("@RID", RoomID);
                sqlParameter[2] = new SqlParameter("@avail", Available);
                SqlHelper.ExecuteScalar(sqlconn, CommandType.Text, query,sqlParameter);
                return 1;
            }
            catch (Exception ex)
            {
                ExceptionHandling.WriteException(ex);
                return 0;
            }
        }
        public long HotelID { get; set; }
        public string HotelName { get; set; }
        public string HotelDisplayName { get; set; }
      

        public List<HotelBasics> GetHotelDetail(string Key)
        {
            List<HotelBasics> lst = new List<HotelBasics>();
            try
            {
                List<SqlParameter> lstsqlparam = new List<SqlParameter>();
                lstsqlparam.Add(new SqlParameter("@HotelName", Key));
                SqlDataReader sqlDataReader = SqlHelper.ExecuteReader(sqlconn, "sp_User_Search", lstsqlparam.ToArray());
                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        lst.Add(new HotelBasics()
                        {
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

        public DataSet GetHotelsForCache()
        {
            DataSet ds = new DataSet();
            try
            {
                string query = "select HotelName,HotelDisplayName from tbl_Agent_Hotel";
                ds = SqlHelper.ExecuteDataset(sqlconn, CommandType.Text, query);
            }
            catch (Exception ex)
            {
                ExceptionHandling.WriteException(ex);
            }
            return ds;
        }
        public DataSet GetIndividualHotelData(string HotelID)
        {
            DataSet ds = new DataSet();
            try
            {
                string query = "select * from tbl_Agent_Hotel where HotelID=" + HotelID + ";select * from tbl_HotelPics where IsEnable=1;select * from tbl_HotelRooms where HotelID=" + HotelID + ";select * from tbl_Add_RoomWisePlan";
                ds = SqlHelper.ExecuteDataset(sqlconn, CommandType.Text, query);
                ds.Tables[0].TableName = "BasicInfo";
                ds.Tables[1].TableName = "HotelPics";
                ds.Tables[2].TableName = "RoomInfo";
                ds.Tables[3].TableName = "RoomPlan";
            }
            catch
            {

            }
            return ds;
        }
    }


}