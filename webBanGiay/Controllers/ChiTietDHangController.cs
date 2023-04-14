using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webBanGiay.Models;

namespace webBanGiay.Controllers
{
    public class ChiTietDHangController : Controller
    {
        // GET: ChiTietDHang
        WebBanGiayDemoEntities db = new WebBanGiayDemoEntities();
        public ActionResult Index()
        {
            return View(db.ChiTietDonHangs.ToList());
        }
        //sua
        [HttpGet]
        public ActionResult ChinhSuaCTDH(int MaDH, int MaGiay)
        {
            ChiTietDonHang ctdh = db.ChiTietDonHangs.FirstOrDefault(n => n.MaDonHang == MaDH && n.MaGiay == MaGiay);
            if (ctdh == null)
            {
                Response.StatusCode = 404;
                return null;
            }


            return View(ctdh);
        }
        [HttpPost]
        public ActionResult ChinhSuaCTDH(ChiTietDonHang ctdh)
        {


            if (ModelState.IsValid)
            {
                //cap nhật trong model nhanh
                db.Entry(ctdh).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        //xoa
        [HttpGet]
        public ActionResult XoaCTDH(int MaDH,int MaGiay)
        {
            ChiTietDonHang ctdh = db.ChiTietDonHangs.FirstOrDefault(n => n.MaDonHang == MaDH && n.MaGiay==MaGiay);
            if (ctdh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(ctdh);

        }
        [HttpPost, ActionName("XoaCTDH")]
        public ActionResult XacNhanXoa(int MaDH, int MaGiay)
        {
            ChiTietDonHang ctdh = db.ChiTietDonHangs.SingleOrDefault(n => n.MaDonHang == MaDH && n.MaGiay == MaGiay);
            if (ctdh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.ChiTietDonHangs.Remove(ctdh);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}