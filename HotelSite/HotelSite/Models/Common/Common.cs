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
using System.Net.Mail;
using System.Net;

namespace HotelSite.Models.Common
{

    public class Common
    {
        public Common()
        {

        }

        #region Sendmail
        public static void SendEmail(string mailBodyHtml, string mailSubject)
        {
            var msg = new MailMessage(ConfigurationManager.AppSettings["FromMail"], ConfigurationManager.AppSettings["ToMail"], mailSubject, mailBodyHtml);
            msg.To.Add("vishalsingh9407@gmail.com");
            msg.IsBodyHtml = true;
            var smtpClient = new SmtpClient("smtp.gmail.com", 587); //if your from email address is "from@hotmail.com" then host should be "smtp.hotmail.com"**
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = new NetworkCredential("", "");
            smtpClient.EnableSsl = false;
            smtpClient.Send(msg);
        }
        #endregion

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
                FileStream fs = File.Create(filePath);
                WriteIntoTxt(filePath, ex);
            }
        }
        private static void WriteIntoTxt(string filePath, Exception ex)
        {
            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                sw.WriteLine("===============================start==============================================");
                sw.WriteLine("Inner Exception's" + ex.InnerException);
                sw.WriteLine("Message" + ex.Message);
                sw.WriteLine("StackTrace" + ex.StackTrace);
                sw.WriteLine("TargetSite" + ex.TargetSite);
                sw.WriteLine("DateTime : \t" + DateTime.Now);
                sw.WriteLine("================================end============================================= \n");
            }
        }
    }

    #endregion
}