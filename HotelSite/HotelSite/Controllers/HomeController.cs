using HotelSite.Models.Agent;
using HotelSite.Models.Common;
using HotelSite.Models.Listing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelSite.App_Code.CL;
using System.Data;
using System.Text;
using System.Security.Cryptography;
using System.Configuration;

namespace HotelSite.Controllers
{
    public class HomeController : Controller
    {
        public string action1 = string.Empty;
        public string hash1 = string.Empty;
        public string txnid1 = string.Empty;
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Search(SearchHotel data)
        {
            string person = data.Person.Split(' ')[0];
            string room = data.Person.Split(' ')[2];
            data.Person = person;
            data.Room = room;
            ViewBag.SearchData = data;
            return View("Listing", data);
        }
        public ActionResult Listing()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetHotelData(string Searchkey)
        {
            try
            {

                return Json(new Common().DatasetToJson(StaticCache.GetHotels()), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string rslt = "Not Found";
                ExceptionHandling.WriteException(ex);
                return Json(rslt, JsonRequestBehavior.AllowGet);
            }
        }
        public List<Hotel> getHotel(string key)
        {
            Hotel listing = new Hotel();
            List<Hotel> list = listing.GetHotelDetail(key);
            return list;
        }

        public JsonResult GetSearchData(SearchHotel Search)
        {
            HotelInformation Hinfo = new HotelInformation();
            Common objCommon = new Common();
            return Json(objCommon.DatasetToJson(Hinfo.GetSearchData(Search)), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetHotelDetailData(string HotelId)
        {
            HotelInformation Hinfo = new HotelInformation();
            Common objCommon = new Common();
            return Json(objCommon.DatasetToJson(Hinfo.GetIndividualHotelData(HotelId)), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetHotelName(string SearchKey)
        {
            HotelInformation Hinfo = new HotelInformation();
            Common objCommon = new Common();
            return Json(Hinfo.GetHotelDetail(SearchKey), JsonRequestBehavior.AllowGet);
        }
        public ActionResult HotelDescription()
        {

            return View();
        }
        public ViewResult getbookingPage()
        {
            return View("PaymentPage");
        }

        public ViewResult GetPaymentResult(FormCollection formCollection)
        {
            string a = formCollection["status"];
            string b = formCollection["txnid"];
            string c = formCollection["payuMoneyId"];
            string d = formCollection["mode"];
            string e = formCollection["error"];
            string f = formCollection["error_Message"];
            if (a == "failure")
            {
                ViewBag.ErrorMessage = f;
                return View("PaymentPage");
            }
            else
            {
                ViewBag.ErrorMessage = f;
                return View("SuccessfullPayment");
            }

        }
        public void PaymentGateway(FormCollection formCollection)
        {

            try
            {

                string[] hashVarsSeq;
                string hash_string = string.Empty;


                if (string.IsNullOrEmpty(Request.Form["txnid"])) // generating txnid
                {
                    Random rnd = new Random();
                    string strHash = Generatehash512(rnd.ToString() + DateTime.Now);
                    txnid1 = strHash.ToString().Substring(0, 20);

                }
                else
                {
                    txnid1 = Request.Form["txnid"];
                }
                if (string.IsNullOrEmpty(Request.Form["hash"])) // generating hash value
                {
                    if (
                        string.IsNullOrEmpty(ConfigurationManager.AppSettings["MERCHANT_KEY"]) ||
                        string.IsNullOrEmpty(txnid1) ||
                        string.IsNullOrEmpty(Request.Form["amount"]) ||
                        string.IsNullOrEmpty(Request.Form["firstname"]) ||
                        string.IsNullOrEmpty(Request.Form["email"]) ||
                        string.IsNullOrEmpty(Request.Form["phone"]) ||
                        string.IsNullOrEmpty(Request.Form["productinfo"]) ||
                        string.IsNullOrEmpty(Request.Form["surl"]) ||
                        string.IsNullOrEmpty(Request.Form["furl"]) ||
                        string.IsNullOrEmpty(Request.Form["service_provider"])
                        )
                    {
                        //error

                        //frmError.Visible = true;
                        Response.Write("");
                    }

                    else
                    {
                        //frmError.Visible = false;
                        hashVarsSeq = ConfigurationManager.AppSettings["hashSequence"].Split('|'); // spliting hash sequence from config
                        hash_string = "";
                        foreach (string hash_var in hashVarsSeq)
                        {
                            if (hash_var == "key")
                            {
                                hash_string = hash_string + ConfigurationManager.AppSettings["MERCHANT_KEY"];
                                hash_string = hash_string + '|';
                            }
                            else if (hash_var == "txnid")
                            {
                                hash_string = hash_string + txnid1;
                                hash_string = hash_string + '|';
                            }
                            else if (hash_var == "amount")
                            {
                                hash_string = hash_string + Convert.ToDecimal(Request.Form[hash_var]).ToString("g29");
                                hash_string = hash_string + '|';
                            }
                            else
                            {

                                hash_string = hash_string + (Request.Form[hash_var] != null ? Request.Form[hash_var] : "");// isset if else
                                hash_string = hash_string + '|';
                            }
                        }

                        hash_string += ConfigurationManager.AppSettings["SALT"];// appending SALT

                        hash1 = Generatehash512(hash_string).ToLower();         //generating hash
                        action1 = ConfigurationManager.AppSettings["PAYU_BASE_URL"] + "/_payment";// setting URL

                    }


                }

                else if (!string.IsNullOrEmpty(Request.Form["hash"]))
                {
                    hash1 = Request.Form["hash"];
                    action1 = ConfigurationManager.AppSettings["PAYU_BASE_URL"] + "/_payment";

                }




                if (!string.IsNullOrEmpty(hash1))
                {
                    TempData["hasd"] = hash1;
                    TempData["txnid"] = txnid1;
                    TempData["key"] = ConfigurationManager.AppSettings["MERCHANT_KEY"];
                    System.Collections.Hashtable data = new System.Collections.Hashtable(); // adding values in gash table for data post
                    data.Add("hash", TempData["hasd"]);
                    data.Add("txnid", TempData["txnid"]);
                    data.Add("key", TempData["key"]);
                    string AmountForm = Convert.ToDecimal(formCollection["amount"]).ToString("g29");// eliminating trailing zeros
                    //amount.Text = AmountForm;
                    data.Add("amount", AmountForm);
                    data.Add("firstname", formCollection["firstname"].Trim());
                    data.Add("email", formCollection["email"].Trim());
                    data.Add("phone", formCollection["phone"].Trim());
                    data.Add("productinfo", formCollection["productinfo"].Trim());
                    data.Add("surl", formCollection["surl"].Trim());
                    data.Add("furl", formCollection["furl"].Trim());

                    //data.Add("lastname", lastname.Text.Trim());
                    //data.Add("curl", curl.Text.Trim());
                    //data.Add("address1", address1.Text.Trim());
                    //data.Add("address2", address2.Text.Trim());
                    //data.Add("city", city.Text.Trim());
                    //data.Add("state", state.Text.Trim());
                    //data.Add("country", country.Text.Trim());
                    //data.Add("zipcode", zipcode.Text.Trim());
                    //data.Add("udf1", udf1.Text.Trim());
                    //data.Add("udf2", udf2.Text.Trim());
                    //data.Add("udf3", udf3.Text.Trim());
                    //data.Add("udf4", udf4.Text.Trim());
                    //data.Add("udf5", udf5.Text.Trim());
                    //data.Add("pg", pg.Text.Trim());
                    data.Add("service_provider", "payu_paisa");//formCollection["service_provider"].Trim()


                    string strForm = PreparePOSTForm(action1, data);

                    #region SavePaymentintoDB

                    Payment.SavePaymentOut(AmountForm, formCollection["firstname"].Trim(), formCollection["email"], formCollection["phone"], formCollection["productinfo"]);

                    #endregion


                    //Page.Controls.Add(new LiteralControl(strForm));
                    Response.Write(strForm);

                }

                else
                {
                    //no hash

                }

            }

            catch (Exception ex)

            {
                Response.Write("<span style='color:red'>" + ex.Message + "</span>");

            }
            //var a = "[{asd:'asdja'}]";
            //return Json(a);
        }

        private string PreparePOSTForm(string url, System.Collections.Hashtable data)      // post form
        {
            //Set a name for the form
            string formID = "PostForm";
            //Build the form using the specified data to be posted.
            StringBuilder strForm = new StringBuilder();
            strForm.Append("<form id=\"" + formID + "\" name=\"" +
                           formID + "\" action=\"" + url +
                           "\" method=\"POST\">");

            foreach (System.Collections.DictionaryEntry key in data)
            {

                strForm.Append("<input type=\"hidden\" name=\"" + key.Key +
                               "\" value=\"" + key.Value + "\">");
            }


            strForm.Append("</form>");
            //Build the JavaScript which will do the Posting operation.
            StringBuilder strScript = new StringBuilder();
            strScript.Append("<script language='javascript'>");
            strScript.Append("var v" + formID + " = document." +
                             formID + ";");
            strScript.Append("v" + formID + ".submit();");
            strScript.Append("</script>");
            //Return the form and the script concatenated.
            //(The order is important, Form then JavaScript)
            return strForm.ToString() + strScript.ToString();
        }
        private string Generatehash512(string text)
        {

            byte[] message = Encoding.UTF8.GetBytes(text);

            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] hashValue;
            SHA512Managed hashString = new SHA512Managed();
            string hex = "";
            hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;

        }
        public ActionResult ViewBookingDetails()
        {
            return View();
        }
    }
}