using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Data;
using HotelSite.Models.Agent;
using System.Web.Caching;

namespace HotelSite.App_Code.CL
{
    [System.ComponentModel.DataObject]
    public class StaticCache
    {
        private static DataTable HotelData = null;
        public static void LoadStaticCache()
        {
            HotelInformation Hinfo = new HotelInformation();
            HttpRuntime.Cache.Insert(
             /* key */                "HotelData",
             /* value */              Hinfo.GetHotelsForCache(),
             /* dependencies */       null,
             /* absoluteExpiration */ Cache.NoAbsoluteExpiration,
             /* slidingExpiration */  Cache.NoSlidingExpiration,
             /* priority */           CacheItemPriority.NotRemovable,
             /* onRemoveCallback */   null);
        }
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataSet GetHotels()
        {
            return HttpRuntime.Cache["HotelData"] as DataSet;
        }
    }

}