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
        public static int SavePaymentOut(string amount,string firstname,string email,string phone,string productinfo)
        {
            try
            {
                string sqlconn = ConfigurationManager.ConnectionStrings["DBCONN"].ConnectionString;
                SqlParameter[] sqlParameter = new SqlParameter[5];
                sqlParameter[0] = new SqlParameter("@Amount", amount);
                sqlParameter[1] = new SqlParameter("@Name", firstname);
                sqlParameter[2] = new SqlParameter("@Email", email);
                sqlParameter[3] = new SqlParameter("@Phone", phone);
                sqlParameter[4] = new SqlParameter("@ID",SqlDbType.Int);
                sqlParameter[4].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery(sqlconn, CommandType.StoredProcedure, "sp_Insert_Payment_Info", sqlParameter);
                int identity = Convert.ToInt32(sqlParameter[4].Value);
                return identity;
            }
            catch (Exception ex)
            {
                ExceptionHandling.WriteException(ex);
                return -1;
            }
            
        }
        public static void UpdatePaymentStatus(string TranID,string ErrorDesc,String status,String payuMoneyID,string mode,string ID)
        {
            try
            {
                string sqlconn = ConfigurationManager.ConnectionStrings["DBCONN"].ConnectionString;                
                string query = "update tbl_Payment_info set TranId = '" + TranID + "', payUMoneyId = '" + payuMoneyID + "', Status = '" + status + "', Mode = '" + mode + "', ErrorMsg = '" + ErrorDesc + "' where ID = '" + ID + "'";
                SqlHelper.ExecuteNonQuery(sqlconn, CommandType.Text, query);                
            }
            catch (Exception ex)
            {
                ExceptionHandling.WriteException(ex);
                throw;
            }
        }
    }
}