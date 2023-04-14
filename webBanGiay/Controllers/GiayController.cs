using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webBanGiay.Models;

namespace webBanGiay.Controllers
{
    public class GiayController : Controller
    {
        WebBanGiayDemoEntities db = new WebBanGiayDemoEntities();
        // GET: Giay
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult GiaymoiPartial()
        {
            var listgiaymoi = db.Giays.Where(m => m.Moi ==1).ToList();
            return PartialView(listgiaymoi);

        }
        public PartialViewResult SanphamPartial()
        {
            var listgiaymoi = db.Giays.Where(m => m.Moi != 1).ToList();
            return PartialView(listgiaymoi);

        }
        public PartialViewResult LoaiGiayPartial()
        {
            var listloaigiay = db.Loais.ToList();
            return PartialView(listloaigiay);

        }
        
        public ActionResult GiayTheoLoai(int MaLoai = 0)
        {
            //kiễm tra loại giày có tồn tại ko
            Loai sloai = db.Loais.SingleOrDefault(n => n.MaLoai == MaLoai);
            if (sloai == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<Giay> listgiay = db.Giays.Where(n => n.MaLoai == MaLoai).OrderBy(n => n.GiaBan).ToList();
            if (listgiay.Count == 0)
            {
                ViewBag.Giay = "Không còn giày nào thuộc loại này";
            }
            return View(listgiay);
        }
        //xem chi tiec
        public ActionResult XemChiTiet(int MaGiay=0)
        {
            Giay giay = db.Giays.SingleOrDefault(n=>n.MaGiay == MaGiay);
            if(giay == null)
            {
                //trả về  trang báo lỗi 
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.NhaSX = db.NhaSanXuats.Single(n=>n.MaNSX== giay.MaNSX).TenNSX;
            ViewBag.Loaigiay = db.Loais.Single(n => n.MaLoai == giay.MaLoai).TenLoai;
            return View(giay);
        }

    }
}