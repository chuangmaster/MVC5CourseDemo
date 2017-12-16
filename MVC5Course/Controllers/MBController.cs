using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class MBController : BaseController
    {
        //GET: MV
        public ActionResult Index()
        {
            //強型別傳值
            var data = repo.GetTop10();
            ViewData.Model = data;  //等同於View(data);
            return View();
        }

        public class MB_ProductParameter
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public Nullable<decimal> Price { get; set; }
            public Nullable<decimal> Stock { get; set; }
            public Nullable<bool> Active { get; set; }
        }

        [HttpPost]
        public ActionResult Index(List<MB_ProductParameter> batch)
        {
            if (ModelState.IsValid)
            {
                if (batch != null)
                {
                    foreach (var item in batch)
                    {
                        var product = repo.Find(item.ProductId);
                        product.ProductName = item.ProductName;
                        product.Stock = item.Stock;
                        product.Price = item.Price;
                        product.ProductName = item.ProductName;
                        product.Active = item.Active;
                    }
                    try
                    {
                        repo.UnitOfWork.Commit();
                    }
                    catch (DbEntityValidationException ex)
                    {
                        throw;
                    }
                }
            }

            return View(repo.GetTop10());
        }
    }
}