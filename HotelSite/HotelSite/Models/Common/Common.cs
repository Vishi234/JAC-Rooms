using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.ApplicationBlocks.Data;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace HotelSite.Models.Common
{

    public class Common
    {
        public Common()
        {

        }
    }
    #region Exception Handling
    /// <summary>
    /// Handle all the exception in Application
    /// </summary>
    public static class ExceptionHandling
    {
        public static void WriteException(Exception ex)
        {
            string filePath = System.Configuration.ConfigurationManager.AppSettings["ErrorFile"].ToString();
            if (File.Exists(filePath))
            {
                WriteIntoTxt(filePath, ex);
            }
            else
            {
                FileSecurity fSecurity = new FileSecurity();
                string a = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString();
                fSecurity.AddAccessRule(new FileSystemAccessRule(a, FileSystemRights.ReadData, AccessControlType.Allow));
                using (FileStream fs = File.Create(filePath, 1024, FileOptions.WriteThrough, fSecurity))
                {

                    WriteIntoTxt(filePath, ex);
                }
            }
        }
        public static void WriteIntoTxt(string filePath, Exception ex)
        {
            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                sw.WriteLine("===============================start==============================================");
                sw.WriteLine("Inner Exception's" + ex.InnerException);
                sw.WriteLine("Message" + ex.Message);
                sw.WriteLine("StackTrace" + ex.StackTrace);
                sw.WriteLine("TargetSite" + ex.TargetSite);
                sw.WriteLine("DateTime : \t" + DateTime.Now);
                sw.WriteLine("================================end=============================================");
            }
        }
    }

    #endregion
}