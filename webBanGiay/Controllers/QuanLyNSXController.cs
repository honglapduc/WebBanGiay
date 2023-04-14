using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webBanGiay.Models;

namespace webBanGiay.Controllers
{
    public class QuanLyNSXController : Controller
    {
        WebBanGiayDemoEntities db = new WebBanGiayDemoEntities();
        // GET: QuanLyNSX
        public ActionResult Index()
        {
            return View(db.NhaSanXuats.ToList());
        }
        [HttpGet]
        public ActionResult ThemMoiNSX()
        {

            return View();

        }
        [HttpPost]
        public ActionResult ThemMoiNSX(NhaSanXuat nhasx)
        {
            db.NhaSanXuats.Add(nhasx);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult ChinhSuaNSX(int MaNSX)
        {
            NhaSanXuat nsx = db.NhaSanXuats.SingleOrDefault(n => n.MaNSX == MaNSX);
            if (nsx == null)
            {
                Response.StatusCode = 404;
                return null;
            }


            return View(nsx);
        }
        [HttpPost]
        public ActionResult ChinhSuaNSX(NhaSanXuat nsx)
        {

            if (ModelState.IsValid)
            {
                //cap nhật trong model nhanh
                db.Entry(nsx).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult XoaNSX(int MaNSX)
        {
            NhaSanXuat nsx = db.NhaSanXuats.SingleOrDefault(n => n.MaNSX == MaNSX);
            if (nsx == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(nsx);

        }
        [HttpPost, ActionName("XoaNSX")]
        public ActionResult XacNhanXoa(int MaNSX)
        {
            NhaSanXuat nsx = db.NhaSanXuats.SingleOrDefault(n => n.MaNSX == MaNSX);
            if (nsx == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.NhaSanXuats.Remove(nsx);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}