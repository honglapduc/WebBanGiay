using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using webBanGiay.Models;

namespace webBanGiay.Controllers
{
    public class HomeController : Controller
    {
        WebBanGiayDemoEntities db = new WebBanGiayDemoEntities();
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult abc()
        {
            return View();
        }

    }
}