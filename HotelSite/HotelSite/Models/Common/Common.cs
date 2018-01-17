using System;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Web;

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
            msg.To.Add("esreekumar@outlook.com");
            msg.IsBodyHtml = true;
            var smtpClient = new SmtpClient("smtp.gmail.com", 587); //if your from email address is "from@hotmail.com" then host should be "smtp.hotmail.com"**
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = new NetworkCredential("devil.terex@gmail.com", "Terex@2018");
            smtpClient.EnableSsl = true;
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
            string ExceptionPath = Path.Combine(HttpContext.Current.Server.MapPath("~"), ConfigurationManager.AppSettings["ErrorFileFolder"].ToString());
            string filePath = ExceptionPath;
            string Exfile = ConfigurationManager.AppSettings["ErrorFileName"].ToString();

            if (Directory.Exists(filePath))
            {
                if (File.Exists(Path.Combine(ExceptionPath, Exfile)))
                    WriteIntoTxt(filePath + "\\" + Exfile, ex);
                else
                    WriteIntoTxt(filePath + "\\" + Exfile, ex);
            }
            else
            {
                Directory.CreateDirectory(filePath);
                WriteIntoTxt(filePath + "\\" + Exfile, ex);
            }
        }
        private static void WriteIntoTxt(string filePath, Exception ex)
        {
            using (StreamWriter sw = File.CreateText(filePath)) //new StreamWriter(filePath, true))
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