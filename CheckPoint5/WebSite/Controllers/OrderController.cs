using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bll;


namespace WebSite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private OrderBll _orderBll = new OrderBll();

        public ActionResult Index()
        {
            List<WebSite.Models.OrderSummary> orders = new List<WebSite.Models.OrderSummary>();

            foreach (var item in _orderBll.GetAll())
            {
                orders.Add(new WebSite.Models.OrderSummary()
                {
                    OrderId = item.OrderId,
                    Date = item.Date,
                    CustomerId = item.CustomerId,
                    Time = item.Time,
                    ManagerId = item.ManagerId,
                    NumberOfItems = item.NumberOfItems,
                    TotalCost = item.TotalCost
                });
            }
            return View(orders);
        }

        [HttpPost]
        public ActionResult Index(string searchString, string searchString2)
        {
            List<WebSite.Models.OrderSummary> orders = new List<WebSite.Models.OrderSummary>();
            if (!String.IsNullOrEmpty(searchString))
            {
                foreach (var item in _orderBll.GetAll().Where((s) =>
                {
                    if (s.Date != null)
                    {
                        return s.Date.ToString().Contains(searchString);
                    }
                    else return false;
                }))
                {
                    orders.Add(new WebSite.Models.OrderSummary()
                    {
                        OrderId = item.OrderId,
                        Date = item.Date,
                        CustomerId = item.CustomerId,
                        Time = item.Time,
                        ManagerId = item.ManagerId,
                        NumberOfItems = item.NumberOfItems,
                        TotalCost = item.TotalCost
                    });
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(searchString2))
                {
                    foreach (var item in _orderBll.GetAll().Where((s) =>
                    {
                        if (s.ManagerId != null)
                        {
                            return s.ManagerId.ToString().Contains(searchString2);
                        }
                        else return false;
                    }))
                    {
                        orders.Add(new WebSite.Models.OrderSummary()
                        {
                            OrderId = item.OrderId,
                            Date = item.Date,
                            CustomerId = item.CustomerId,
                            Time = item.Time,
                            ManagerId = item.ManagerId,
                            NumberOfItems = item.NumberOfItems,
                            TotalCost = item.TotalCost
                        });
                    }
                }
            }
            return View(orders);
        }

        public ActionResult Edit()
        {
            int orderId = int.Parse(RouteData.Values["id"].ToString());
            var record = _orderBll.GetRecord(orderId);
            WebSite.Models.OrderSummary order = new WebSite.Models.OrderSummary()
            {
                Date = record.Date,
                Time = record.Time,
                CustomerId = record.CustomerId,
                ManagerId = record.ManagerId
            };
            return View(order);
        }

        [HttpPost]
        public ActionResult Edit(WebSite.Models.OrderSummary order)
        {
            if (ModelState.IsValid)
            {
                int orderId = int.Parse(RouteData.Values["id"].ToString());
                _orderBll.Update(new global::Models.Order() {OrderId = orderId},
                    new global::Models.Order()
                    {
                        Date = order.Date,
                        Time = order.Time,
                        CustomerId = order.CustomerId,
                        ManagerId = order.ManagerId
                    });
                _orderBll.Save();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Graphs()
        {
            return View();
        }

        public ActionResult GetDateAndCost()
        {
            List<int?[]> p = new List<int?[]>();

            foreach (var item in _orderBll.GetAll().GroupBy(x => x.Date.Value.Month))
            {
                int? totalSum = item.Sum(x => x.TotalCost);
                p.Add(new[] { item.Key, item.First().Date.Value.Year, totalSum });
            }
            
            return Json(new { result = p }, JsonRequestBehavior.AllowGet);
        }
    }
}