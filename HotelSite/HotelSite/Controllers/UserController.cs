﻿using HotelSite.Models.Common;
using HotelSite.Models.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelSite.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [HttpGet]
        public ActionResult Register()
        {
            try

            {
                throw new NotImplementedException();
                return View();
            }
            catch (Exception ex)
            {
                ExceptionHandling.WriteException(ex);
                return View();
            }
        }
        [ActionName("Register")]
        [HttpPost]
        public ActionResult RegisterUser(Login lgnDetail)
        {
            if (ModelState.IsValid)
            {
                UpdateModel(lgnDetail);
                Register register = new Register();
                int rslt = register.CreateUser(lgnDetail);
                if (rslt > 1) return View("Login");
                else return View();
            }
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoggingIN(Signin signin)
        {
            Register register = new Register();
            if (register.SignIn(signin))
            {
                return View("Dashboard");
            }
            else
            {
                return View("Login");
            }
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ForgotPassword(string email)
        {
            return Json("asd", JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEmail(string email)
        {
            Register register = new Register();
            bool EmailValid=register.checkEmailUser(email);
            return Json(new { res = EmailValid }, JsonRequestBehavior.AllowGet);
        }
    }
}