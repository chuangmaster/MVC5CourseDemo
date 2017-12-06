using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class MBController : BaseController
    {
        // GET: MV
        public ActionResult Index()
        {
            //強型別傳值
            var data = repo.GetTop10();
            ViewData.Model = data;  //等同於View(data);
            return View();
        }
    }
}