using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class TestController : Controller
    {
        FabricsEntities db = new FabricsEntities();
        // GET: Test
        public ActionResult Index()
        {
            var result = from i in db.Product
                         where i.IsDeleted == false
                         orderby i.ProductId descending
                         select i;
            return View(result.Take(100));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Product.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        public ActionResult Edit(int id)
        {
            if (ModelState.IsValid)
            {
                var data = db.Product.Find(id);
                return View(data);
            }
            return View("Index");
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                var data = db.Product.Find(product.ProductId);
                data.Active = product.Active;
                data.OrderLine = product.OrderLine;
                data.Price = product.Price;
                data.ProductName = product.ProductName;
                data.Stock = product.Stock;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Details(int id)
        {
            return View(db.Product.Find(id));
        }

        public ActionResult Delete(int id)
        {
            var productData = db.Product.Find(id);
            //db.OrderLine.RemoveRange(productData.OrderLine.ToList());
            //db.Product.Remove(productData);
            productData.IsDeleted = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}