using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class ARController : BaseController
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
            return PartialView("Alert", "新增成功");
        }
        public ActionResult FileTest(string dl)
        {
            //ContentType:請查閱 http://www.freeformatter.com/mime-types-list.html
            if (string.IsNullOrEmpty(dl))
            {
                return File(Server.MapPath("~/App_Data/switch.jpg"), "image/jpeg");
            }
            else
            {
                return File(Server.MapPath("~/App_Data/switch.jpg"), "image/jpeg", "Switch.jpg");

            }
        }

        public ActionResult JsonTest()
        {
            var data = from p in repo.GetTop10()
                       select new
                       {
                           p.ProductId,
                           p.ProductName,
                           p.Price
                       };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RedirectTest()
        {
            return RedirectToAction("FileTest", new
            {
                dl = 1
            });
        }


        public ActionResult RedirectTest2()
        {
            return RedirectToRoute("FileTest", new
            {
                controller = "Home",
                action = "About",
                id = 123
            });
        }
    }
}