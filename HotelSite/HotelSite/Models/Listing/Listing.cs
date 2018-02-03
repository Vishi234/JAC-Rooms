using HotelSite.Models.Common;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HotelSite.Models.Listing
{
    public class Listing
    {
        public long HotelID { get; set; }
        public string HotelName { get; set; }
        public string HotelDisplayName { get; set; }
        string sqlconn = System.Configuration.ConfigurationManager.ConnectionStrings["DBCONN"].ConnectionString;
        public List<Listing> GetHotelDetail(string Key)
        {
            List<Listing> lst = new List<Listing>();
            try
            {
                List<SqlParameter> lstsqlparam = new List<SqlParameter>();
                lstsqlparam.Add(new SqlParameter("@HotelName", Key));
                SqlDataReader sqlDataReader = SqlHelper.ExecuteReader(sqlconn, "sp_User_Search", lstsqlparam.ToArray());
                if(sqlDataReader.HasRows)
                {
                    while(sqlDataReader.Read())
                    {
                        lst.Add(new Listing()
                        {
                            HotelDisplayName = sqlDataReader["HotelDisplayName"].ToString(),
                            HotelID = (int)sqlDataReader["HotelID"],
                            HotelName = sqlDataReader["HotelName"].ToString()
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
    }
}