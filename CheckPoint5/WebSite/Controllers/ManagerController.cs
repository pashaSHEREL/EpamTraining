using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bll;
using DAL;

namespace WebSite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ManagerController : Controller
    {
        private ManagerBll _managerBll = new ManagerBll();

        public ActionResult Index()
        {
            List<WebSite.Models.Manager> managers = new List<WebSite.Models.Manager>();

            foreach (var item in _managerBll.GetAll())
            {
                managers.Add(new WebSite.Models.Manager()
                {
                    ManagerId = item.ManagerId,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Address = item.Address,
                    PhoneNumber = item.PhoneNumber,
                    Email = item.Email
                });
            }
            return View(managers);
        }

        [HttpPost]
        public ActionResult Index(string searchString)
        {
            List<WebSite.Models.Manager> managers = new List<WebSite.Models.Manager>();

            foreach (var item in _managerBll.GetAll().Where((s) =>
            {
                if (s.FirstName != null)
                {
                    return s.FirstName.Contains(searchString);
                }
                else return false;
            }))
            {
                managers.Add(new WebSite.Models.Manager()
                {
                    ManagerId = item.ManagerId,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Address = item.Address,
                    PhoneNumber = item.PhoneNumber,
                    Email = item.Email
                });
            }
            return View(managers);
        }

        public ActionResult Edit()
        {
            int managerId = int.Parse(RouteData.Values["id"].ToString());
            var record = _managerBll.GetRecord(managerId);
            WebSite.Models.Manager manager = new WebSite.Models.Manager()
            {
                FirstName = record.FirstName,
                LastName = record.LastName,
                Address = record.Address,
                PhoneNumber = record.PhoneNumber,
                Email = record.Email
            };
            return View(manager);
        }

        [HttpPost]
        public ActionResult Edit(WebSite.Models.Manager manager)
        {
            if (ModelState.IsValid)
            {
                int managerId = int.Parse(RouteData.Values["id"].ToString());
                _managerBll.Update(new global::Models.Manager() {ManagerId = managerId},
                    new global::Models.Manager()
                    {
                        FirstName = manager.FirstName,
                        LastName = manager.LastName,
                        Address = manager.Address,
                        PhoneNumber = manager.PhoneNumber,
                        Email = manager.Email
                    });
                _managerBll.Save();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(WebSite.Models.Manager manager)
        {
            if (ModelState.IsValid)
            {
                _managerBll.Add(new global::Models.Manager()
                {
                    FirstName = manager.FirstName,
                    LastName = manager.LastName,
                    Address = manager.Address,
                    PhoneNumber = manager.PhoneNumber,
                    Email = manager.Email
                });
                _managerBll.Save();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
    }
}