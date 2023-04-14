using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webBanGiay.Models;

namespace webBanGiay.Controllers
{
    public class QuanLyDonHangController : Controller
    {
        // GET: QuanLyDonHang
        WebBanGiayDemoEntities db = new WebBanGiayDemoEntities();
        public ActionResult Index()
        {
            return View(db.DonHangs.ToList());
        }
        
        //xoa
        [HttpGet]
        public ActionResult XoaDH(int MaDH)
        {
            DonHang sdh = db.DonHangs.SingleOrDefault(n => n.MaDonHang == MaDH);
            if (sdh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sdh);

        }
        [HttpPost, ActionName("XoaDH")]
        public ActionResult XacNhanXoa(int MaDH)
        {
            DonHang sdh = db.DonHangs.SingleOrDefault(n => n.MaDonHang == MaDH);
            if (sdh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.DonHangs.Remove(sdh);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //sua
        [HttpGet]
        public ActionResult ChinhSuaDh(int MaDH)
        {
            DonHang sdh = db.DonHangs.SingleOrDefault(n => n.MaDonHang == MaDH);
            if (sdh == null)
            {
                Response.StatusCode = 404;
                return null;
            }


            return View(sdh);
        }
        [HttpPost]
        public ActionResult ChinhSuaDh(DonHang sdh)
        {


            if (ModelState.IsValid)
            {
                //cap nhật trong model nhanh
                db.Entry(sdh).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}