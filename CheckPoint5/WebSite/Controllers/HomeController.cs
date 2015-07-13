using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace WebSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Shop";
            return View();
        }

        public ActionResult ErrorAccess()
        {
            return View();
        }
    }
}