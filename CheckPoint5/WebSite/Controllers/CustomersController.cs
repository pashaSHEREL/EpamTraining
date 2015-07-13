using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bll;
using Models;
using CustomerSummaryPresenterModel = WebSite.Models.CustomerSummaryPresenterModel;

namespace WebSite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CustomersController : Controller
    {
        private CustomerBll _customerBll = new CustomerBll();

        public ActionResult Index()
        {
            ViewBag.Title = "Customers";
            List<WebSite.Models.Customer> customers = new List<WebSite.Models.Customer>();

            foreach (var item in _customerBll.GetAll())
            {
                customers.Add(new WebSite.Models.Customer()
                {
                    CustomerId = item.CustomerId,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Address = item.Address,
                    PhoneNumber = item.PhoneNumber,
                    Email = item.Email
                });
            }
            return View(customers);
        }

        [HttpPost]
        public ActionResult Index(string searchString)
        {
            List<WebSite.Models.Customer> customers = new List<WebSite.Models.Customer>();

            foreach (var item in _customerBll.GetAll().Where((s) =>
            {
                if (s.FirstName != null)
                {
                    return s.FirstName.Contains(searchString);
                }
                else return false;
            }))
            {
                customers.Add(new WebSite.Models.Customer()
                {
                    CustomerId = item.CustomerId,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Address = item.Address,
                    PhoneNumber = item.PhoneNumber,
                    Email = item.Email
                });
            }
            return View(customers);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(WebSite.Models.Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerBll.Add(new global::Models.Customer()
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Address = customer.Address,
                    PhoneNumber = customer.PhoneNumber,
                    Email = customer.Email
                });
                _customerBll.Save();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Edit()
        {
            int customerId = int.Parse(RouteData.Values["id"].ToString());
            var record = _customerBll.GetRecord(customerId);
            WebSite.Models.Customer customer = new WebSite.Models.Customer()
            {
                FirstName = record.FirstName,
                LastName = record.LastName,
                Address = record.Address,
                PhoneNumber = record.PhoneNumber,
                Email = record.Email
            };
            return View(customer);
        }

        [HttpPost]
        public ActionResult Edit(WebSite.Models.Customer customer)
        {
            if (ModelState.IsValid)
            {
                int customerId = int.Parse(RouteData.Values["id"].ToString());
                _customerBll.Update(new global::Models.Customer() {CustomerId = customerId},
                    new global::Models.Customer()
                    {
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        Address = customer.Address,
                        PhoneNumber = customer.PhoneNumber,
                        Email = customer.Email
                    });
                _customerBll.Save();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public ActionResult CustomerSummary()
        {
            List<WebSite.Models.CustomerSummaryPresenterModel> customerSummary =
                new List<CustomerSummaryPresenterModel>();

            foreach (var item in _customerBll.GetAllSummary())
            {
                customerSummary.Add(new CustomerSummaryPresenterModel()
                {
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Address = item.Address,
                    OrderCount = item.OrderCount,
                    TotalSum = item.TotalSum
                });
            }
            return View(customerSummary);
        }

        [HttpPost]
        public ActionResult CustomerSummary(string searchString)
        {
            List<WebSite.Models.CustomerSummaryPresenterModel> customerSummary =
                new List<WebSite.Models.CustomerSummaryPresenterModel>();

            foreach (var item in _customerBll.GetAllSummary().Where((s) =>
            {
                if (s.FirstName != null)
                {
                    return s.FirstName.Contains(searchString);
                }
                else return false;
            }))
            {
                customerSummary.Add(new CustomerSummaryPresenterModel()
                {
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Address = item.Address,
                    OrderCount = item.OrderCount,
                    TotalSum = item.TotalSum
                });
            }
            return View(customerSummary);
        }
    }
}