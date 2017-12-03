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
        ProductRepository repo = RepositoryHelper.GetProductRepository();
        // GET: Test
        public ActionResult Index()
        {
            var data = repo.All();
            return View(data);
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
                repo.Add(product);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        public ActionResult Edit(int id)
        {
            if (ModelState.IsValid)
            {
                var data = repo.Find(id);
                return View(data);
            }
            return View("Index");
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                var data = repo.Find(product.ProductId);
                data.Active = product.Active;
                data.OrderLine = product.OrderLine;
                data.Price = product.Price;
                data.ProductName = product.ProductName;
                data.Stock = product.Stock;
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Details(int id)
        {
            return View(repo.Find(id));
        }

        
        public ActionResult Delete(int id)
        {
            //var olRepo = RepositoryHelper.GetOrderLineRepository(repo.UnitOfWork);
            //olRepo.Delete(olRepo.All().First(p => p.OrderId == 1));

            //var olRepo = new OrderLineRepository();
            //olRepo.UnitOfWork = repo.UnitOfWork;
            //olRepo.Delete(olRepo.All().First(p => p.OrderId == 1));
            var data = repo.Find(id);
            data.IsDeleted = true;
            repo.UnitOfWork.Commit();            
            return RedirectToAction("Index");
        }
    }
}