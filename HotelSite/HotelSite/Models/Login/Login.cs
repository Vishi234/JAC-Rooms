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
                DataSet ds = SqlHelper.ExecuteDataset(sqlconn, "sp_Login", lstsqlparam.ToArray());
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        HttpContext.Current.Session["UserID"] = ds.Tables[0].Rows[0]["UserID"].ToString();  
                        HttpContext.Current.Session["Name"] = ds.Tables[0].Rows[0]["FirstName"].ToString() + ds.Tables[0].Rows[0]["LastName"].ToString();
                        HttpContext.Current.Session["Email"] = ds.Tables[0].Rows[0]["Email"].ToString();
                        HttpContext.Current.Session["Mobile"] = ds.Tables[0].Rows[0]["Mobile"].ToString();

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
    }
}