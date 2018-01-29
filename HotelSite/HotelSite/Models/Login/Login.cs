using System;
using System.Collections.Generic;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web;
using System.Linq;
using HotelSite.Models.Common;

namespace HotelSite.Models.Login
{
    public class Login
    {
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public class Signin
    {
        public string EmailID { get; set; }
        public string Password { get; set; }
        public bool? IsAgent { get; set; } = false;

    }
    public class Register
    {

        string sqlconn = ConfigurationManager.ConnectionStrings["DBCONN"].ConnectionString;
        public int CreateUser(Login lgn)
        {
            int result = 0;
            try
            {
                //sp_Register_User
                List<SqlParameter> lstsqlparam = new List<SqlParameter>();
                lstsqlparam.Add(new SqlParameter("@Email", lgn.Email));
                lstsqlparam.Add(new SqlParameter("@Mobile", lgn.Mobile));
                lstsqlparam.Add(new SqlParameter("@Password", lgn.Password));
                lstsqlparam.Add(new SqlParameter("@FirstName", lgn.FirstName));
                lstsqlparam.Add(new SqlParameter("@LastName", lgn.LastName));
                lstsqlparam.Add(new SqlParameter("@CreatedBy", "sreekumar"));
                lstsqlparam.Add(new SqlParameter("@UpdatedBy", "sreekumar"));
                int a = SqlHelper.ExecuteNonQuery(sqlconn, "sp_Register_User", lstsqlparam.ToArray());
                result = a;

            }
            catch (Exception ex)
            {
                ExceptionHandling.WriteException(ex);
            }
            finally
            {

            }
            return result;
        }
        public bool SignIn(Signin signin)
        {
            try
            {
                List<SqlParameter> lstsqlparam = new List<SqlParameter>();
                lstsqlparam.Add(new SqlParameter("@EmailID", signin.EmailID));
                lstsqlparam.Add(new SqlParameter("@Password", signin.Password));
                lstsqlparam.Add(new SqlParameter("@IsAgent", signin.IsAgent));
                DataSet ds = SqlHelper.ExecuteDataset(sqlconn, "sp_Login", lstsqlparam.ToArray());
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (signin.IsAgent == false)
                        {
                            HttpContext.Current.Session["UserID"] = ds.Tables[0].Rows[0]["UserID"].ToString();
                            HttpContext.Current.Session["Name"] = ds.Tables[0].Rows[0]["FirstName"].ToString() + ds.Tables[0].Rows[0]["LastName"].ToString();
                            HttpContext.Current.Session["Email"] = ds.Tables[0].Rows[0]["Email"].ToString();
                            HttpContext.Current.Session["Mobile"] = ds.Tables[0].Rows[0]["Mobile"].ToString();
                        }
                        else
                        {
                            HttpContext.Current.Session["UserID"] = ds.Tables[0].Rows[0]["AgentID"].ToString();
                            HttpContext.Current.Session["Name"] = ds.Tables[0].Rows[0]["AgentFullName"].ToString();
                            HttpContext.Current.Session["Email"] = ds.Tables[0].Rows[0]["AgentEmail"].ToString();
                        }

                        return true;
                    }
                    return false;

                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.WriteException(ex);
                return false;
            }
        }
        public bool checkEmailUser(string email,bool IsAgent)
        {
            bool result = false;
            try
            {
                string tblName = IsAgent ? "tbl_AgentLogin#AgentEmail" : "tbl_User_Detail#Email";
                string query = string.Format("select 1 from {0}  where lower({1}) = '" + email.ToLower() + "'", tblName.Split('#')[0], tblName.Split('#')[1]);
                result = (SqlHelper.ExecuteScalar(sqlconn, CommandType.Text, query) != null) ? true : false;
            }
            catch (Exception ex)
            {
                result = false;
                ExceptionHandling.WriteException(ex);
            }
            return result;
        }
    }
    public class Agent
    {
        string sqlconn = ConfigurationManager.ConnectionStrings["DBCONN"].ConnectionString;
        public string CreateAgent(string Email, string Password, string HotelType, string FullName)
        {
            Guid guid = Guid.Empty;
            try
            {
                List<SqlParameter> lstsqlparam = new List<SqlParameter>();
                lstsqlparam.Add(new SqlParameter("@AgentFullName", FullName));
                lstsqlparam.Add(new SqlParameter("@AgentType", HotelType));
                lstsqlparam.Add(new SqlParameter("@AgentEmail", Email));
                lstsqlparam.Add(new SqlParameter("@Password", Password));
                guid = (Guid)SqlHelper.ExecuteScalar(sqlconn, "sp_AgentLogin", lstsqlparam.ToArray());
            }
            catch (Exception ex)
            {
                ExceptionHandling.WriteException(ex);
            }
            finally
            {

            }
            return guid.ToString();
        }

        public int CheckAgent(Guid guid)
        {
            int result = 0;
            try
            {
                SqlParameter sqlParameter = new SqlParameter("@LinkId", guid);
                object obj = SqlHelper.ExecuteScalar(sqlconn, "sp_VerifyAgent", sqlParameter);
                result = Convert.ToInt32(obj);
            }
            catch (Exception ex)
            {
                result = 0;
                ExceptionHandling.WriteException(ex);
            }
            return result;
        }
    }
}