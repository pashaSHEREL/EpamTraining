using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.Models;

namespace WebSite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CustomersController : Controller
    {
        private CustomerRepository _customerRepository = new CustomerRepository();

        public ActionResult Index()
        {
            ViewBag.Title = "Customers";
            return View(_customerRepository.GetAll());
        }
    }
}