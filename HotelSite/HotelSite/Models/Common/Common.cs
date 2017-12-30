using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.ApplicationBlocks.Data;
using System.IO;

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
            string filePath = @"C:\Users\Lenovo\Documents\Visual Studio 2017\Projects\Exception.txt";
            if (File.Exists(filePath))
            {
                WriteIntoTxt(filePath, ex);
            }
            else
            {
                File.Create(filePath);
                //DirectoryInfo directoryInfo = Directory.CreateDirectory(filePath);
                //directoryInfo.CreateSubdirectory("Exception.txt");
                WriteIntoTxt(filePath, ex);
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