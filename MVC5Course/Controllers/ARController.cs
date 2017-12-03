using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class ARController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PartialViewTest()
        {
            return PartialView("Index");
            //return PartialView("../Home/About");
        }

        public ActionResult ContentTest()
        {
            //此用方避免，會降低維護
            return Content("<script>alert('新增成功!');location.href='/';</script>");            
        }

        public ActionResult ContentTestBetter()
        {
            return PartialView("Alert","新增成功");
        }

       
    }
}