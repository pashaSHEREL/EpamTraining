using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bll;
using CheckPoint4;
using DAL;
using Models;
using WebSite.Models;
using Item = CheckPoint4.Item;

namespace WebSite.Controllers
{
    public class ItemController : Controller
    {
        private readonly ItemBll _itemBll = new ItemBll();

        public ActionResult Index()
        {
            ViewBag.Title = "Products";
            List<WebSite.Models.Item> products = new List<WebSite.Models.Item>();

            foreach (var item in _itemBll.GetAll())
            {
                products.Add(new WebSite.Models.Item()
                {
                    ItemId = item.ItemId,
                    Name = item.Name,
                    Cost = item.Cost,
                    Description = item.Description,
                    Number = item.Number
                });
            }
            return View(products);
        }

        [HttpPost]
        public ActionResult Index(string searchString)
        {
            List<WebSite.Models.Item> products = new List<WebSite.Models.Item>();

            foreach (var item in _itemBll.GetAll().Where((s) =>
            {
                if (s.Name != null)
                {
                    return s.Name.Contains(searchString);
                }
                else return false;
            }))

            {
                products.Add(new WebSite.Models.Item()
                {
                    ItemId = item.ItemId,
                    Name = item.Name,
                    Cost = item.Cost,
                    Description = item.Description,
                    Number = item.Number
                });
            }
            return View(products);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(WebSite.Models.Item item)
        {
            if (ModelState.IsValid)
            {
                _itemBll.Add(new global::Models.Item()
                {
                    Name = item.Name,
                    Cost = item.Cost,
                    Description = item.Description,
                    Number = item.Number
                });
                _itemBll.Save();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit()
        {
            int itemId = int.Parse(RouteData.Values["id"].ToString());
            var record = _itemBll.GetRecord(itemId);
            WebSite.Models.Item item = new WebSite.Models.Item()
            {
                Name = record.Name,
                Cost = record.Cost,
                Description = record.Description,
                Number = record.Number
            };
            return View(item);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(WebSite.Models.Item item)
        {
            if (ModelState.IsValid)
            {
                int itemId = int.Parse(RouteData.Values["id"].ToString());
                _itemBll.Update(new global::Models.Item() {ItemId = itemId},
                    new global::Models.Item()
                    {
                        Name = item.Name,
                        Cost = item.Cost,
                        Description = item.Description,
                        Number = item.Number
                    });
                _itemBll.Save();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Graphs()
        {
            if (Request.IsAjaxRequest())
            {
                return Json(new {result = 100});
            }
            return View();
        }

        public ActionResult RR()
        {  
            List<List<int>> kList=new List<List<int>>(){new List<int>(){1,2},new List<int>(){4,7}};
            return Json(new { result = kList }, JsonRequestBehavior.AllowGet);
        }
    }
}