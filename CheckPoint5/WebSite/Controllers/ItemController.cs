using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.Models;

namespace WebSite.Controllers
{
   
    public class ItemController : Controller
    {
        private readonly ItemRepository _itemRepository = new ItemRepository();

        public ActionResult Index()
        {
            ViewBag.Title = "Products";
            return View(_itemRepository.GetAll());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(DAL.Models.Item item)
        {
            if (ModelState.IsValid)
            {
                _itemRepository.Add(item);
                _itemRepository.Save();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Item item)
        {
            if (ModelState.IsValid)
            {
                int itemId = int.Parse(RouteData.Values["id"].ToString());
                _itemRepository.Update(new Item() {ItemId = itemId}, item);
                _itemRepository.Save();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Delete()
        {
            int itemId = int.Parse(RouteData.Values["id"].ToString());
            _itemRepository.Delete(new Item() {ItemId = itemId});
            _itemRepository.Save();
            return RedirectToAction("Index");
        }
    }
}