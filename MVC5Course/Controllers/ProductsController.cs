﻿using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using MVC5Course.Models.ViewModels;

namespace MVC5Course.Controllers
{
    public class ProductsController : Controller
    {
        private FabricsEntities db = new FabricsEntities();

        // GET: Products
        public ActionResult Index(int pageNo = 1)
        {
            var ordered = db.Product.ToList().OrderBy(x => x.ProductId);
            var data = ordered.ToPagedList(pageNo, 10);
            return View(data);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderLines = product.OrderLine.ToList();
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            var items = new List<SelectListItem>();
            items.Add(new SelectListItem() { Text = "0", Value = "0" });
            items.Add(new SelectListItem() { Text = "10", Value = "10" });
            items.Add(new SelectListItem() { Text = "20", Value = "20" });
            items.Add(new SelectListItem() { Text = "30", Value = "30" });
            var priceList = new SelectList(items, "Text", "Value");
            ViewBag.Price = items;
            return View();
        }

        // POST: Products/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Product.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var items = new List<SelectListItem>();
            items.Add(new SelectListItem() { Text = "0", Value = "0" });
            items.Add(new SelectListItem() { Text = "10", Value = "10" });
            items.Add(new SelectListItem() { Text = "20", Value = "20" });
            items.Add(new SelectListItem() { Text = "30", Value = "30" });
            var priceList = new SelectList(items, "Text", "Value");
            ViewBag.Price = items;
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            //從DB取得所有的Price當作SelectList
            var priceList = (from item in db.Product select new { text = item.Price, value = item.Price }).Distinct().OrderBy(x => x.value);
            ViewBag.Price = new SelectList(priceList, "text", "value");
            return View(product);
        }

        // POST: Products/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            int id
            //[Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product
            )
        {
            var product = db.Product.Find(id);
            if (TryUpdateModel(product, new string[] { "ProductId", "Price", "Active", "Stock" }))
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Product.Find(id);
            db.Product.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult List()
        {
            var model = from p in db.Product
                        select new ProductListViewModel()
                        {
                            ProductId = p.ProductId,
                            Price = p.Price,
                            ProductName = p.ProductName,
                            Stock = p.Stock
                        };
            return View(model);
        }

#if !DEBUG
        [NonAction]
#endif
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
