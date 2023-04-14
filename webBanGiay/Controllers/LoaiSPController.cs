using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webBanGiay.Models;

namespace webBanGiay.Controllers
{
    public class LoaiSPController : Controller
    {
        // GET: LoaiSP
        WebBanGiayDemoEntities db = new WebBanGiayDemoEntities();
        public ActionResult Index()
        {
            return View(db.Loais.ToList());
        }
        [HttpGet]
        public ActionResult ThemLoaiGiay()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemLoaiGiay(Loai sloai)
        {
            db.Loais.Add(sloai);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult XoaLoaiGiay(int Maloai)
        {
            Loai ssloai = db.Loais.SingleOrDefault(n => n.MaLoai == Maloai);
            if (ssloai == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(ssloai);

        }
        [HttpPost, ActionName("XoaLoaiGiay")]
        public ActionResult XacNhanXoa(int Maloai)
        {
            Loai ssloai = db.Loais.SingleOrDefault(n => n.MaLoai == Maloai);
            if (ssloai == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.Loais.Remove(ssloai);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}