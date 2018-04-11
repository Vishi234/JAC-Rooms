using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Data;
using HotelSite.Models.Agent;
using System.Web.Caching;
using HotelSite.Models.Common;
using System.IO;

namespace HotelSite.App_Code.CL
{
    [System.ComponentModel.DataObject]
    public class StaticCache
    {
        //private static DataTable HotelData = null;
        public static void LoadStaticCache()
        {
            HotelInformation Hinfo = new HotelInformation();
            Common objCommon = new Common();
            // Declare the File Information   
            FileInfo file = new FileInfo(HttpContext.Current.Server.MapPath("~/Location Data/GlobalData.json"));
            // write the line using streamwriter   
            using (StreamWriter sw = file.AppendText())
            {
                sw.WriteLine(objCommon.DatasetToJson(Hinfo.GetHotelsForCache()));
            }
            // Read the content using Streamreader from text file   
            //using (StreamReader sr = file.OpenText())
            //{
            //    string s = "";
            //    while ((s = sr.ReadLine()) != null)
            //    {
            //        listBox1.Text = s; // Display the text file content to the list box  
            //    }
            //    sr.Close();
            //}
            string jsondata = objCommon.DatasetToJson(Hinfo.GetHotelsForCache());
            string path = HttpContext.Current.Server.MapPath("~/Location Data/");
            //File.Delete(path + "GlobalData.json");
            //File.Create(path + "GlobalData.json");
            File.WriteAllText(path + "GlobalData.json", jsondata);
        }
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataSet GetHotels()
        {
            return HttpRuntime.Cache["HotelData"] as DataSet;
        }
    }

}