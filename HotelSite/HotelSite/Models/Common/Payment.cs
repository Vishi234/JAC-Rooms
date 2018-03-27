using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HotelSite.Models.Common
{
    public static class Payment
    {
        public static bool SavePaymentOut(string amount,string firstname,string email,string phone,string productinfo)
        {
            try
            {
                string sqlconn = ConfigurationManager.ConnectionStrings["DBCONN"].ConnectionString;
                SqlParameter[] sqlParameter = new SqlParameter[4];
                sqlParameter[0] = new SqlParameter("@Amount", amount);
                sqlParameter[1] = new SqlParameter("@Name", firstname);
                sqlParameter[2] = new SqlParameter("@Email", email);
                sqlParameter[3] = new SqlParameter("@Phone", phone);
                SqlHelper.ExecuteNonQuery(sqlconn, CommandType.StoredProcedure, "sp_Insert_Payment_Info", sqlParameter);
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandling.WriteException(ex);
                return false;
            }
            
        }
    }
}