﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    [ValidateInput(false)]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Fl*ricam";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
