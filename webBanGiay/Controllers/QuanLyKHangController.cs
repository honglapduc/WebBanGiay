using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webBanGiay.Models;

namespace webBanGiay.Controllers
{
    public class QuanLyKHangController : Controller
    {
        // GET: QuanLyKHang
        WebBanGiayDemoEntities db = new WebBanGiayDemoEntities();
        public ActionResult Index()
        {
            return View(db.KhachHangs.ToList());
        }
        [HttpGet]
        public ActionResult ThemMoi()
        {
            
            return View();

        }
        [HttpPost]
        public ActionResult ThemMoi(KhachHang kh)
        {
            db.KhachHangs.Add(kh);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult ChinhSua(int MaKh)
        {
            KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.MaKH == MaKh);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }


            return View(kh);
        }
        [HttpPost]
        public ActionResult ChinhSua(KhachHang kh)
        {

            if (ModelState.IsValid)
            {
                //cap nhật trong model nhanh
                db.Entry(kh).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult XoaKh(int MaKh)
        {
            KhachHang kh = db.KhachHangs.SingleOrDefault(n=>n.MaKH== MaKh);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(kh);

        }
        [HttpPost, ActionName("XoaKh")]
        public ActionResult XacNhanXoa(int MaKh)
        {
            KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.MaKH == MaKh);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.KhachHangs.Remove(kh);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult HienThi(int MaKh)
        {
            KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.MaKH == MaKh);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }


            return View(kh);
        }

    }
}