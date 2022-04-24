﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class HomeController : Controller
    {

    
        public ActionResult Index()
        {

            string iduserrrr = User.Identity.GetUserId();
            System.Diagnostics.Debug.WriteLine("****username :*****" + HttpContext.User.Identity.GetUserId());

            return View();
        }


        public ActionResult Events()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}