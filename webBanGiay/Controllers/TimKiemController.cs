using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webBanGiay.Models;
using PagedList.Mvc;
using PagedList;
namespace webBanGiay.Controllers
{
    public class TimKiemController : Controller
    {
        // GET: TimKiem
        //FormCollection 
        WebBanGiayDemoEntities db = new WebBanGiayDemoEntities();
        [HttpPost]
        //FormCollection lấy dlieu  từ html
        public ActionResult KetQuaTimKiem( FormCollection f, int? page)
        {
            string sTuKhoa = f["txtTimKiem"].ToString();
            ViewBag.TuKhoa = sTuKhoa;
            //Contains phuoc thức tìm kiem
            List<Giay> lstKQ = db.Giays.Where(n=>n.TenGiay.Contains(sTuKhoa)).ToList();
            //phan trang
            int pageNumber = (page ?? 1);
            int pageSize = 8;
            if(lstKQ.Count == 0)
            {
                ViewBag.ThongBao = "Không tìm thấy Giày";
                return View(db.Giays.OrderBy(n=>n.TenGiay).ToPagedList(pageNumber,pageSize));
            }
            ViewBag.ThongBao = "Đã tìm thấy " + lstKQ.Count + " kết quả";
            return View(lstKQ.OrderBy(n=>n.TenGiay).ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult KetQuaTimKiem(string sTuKhoa, int? page)
        {

            ViewBag.TuKhoa = sTuKhoa;
            //Contains phuoc thức tìm kiem
            List<Giay> lstKQ = db.Giays.Where(n => n.TenGiay.Contains(sTuKhoa)).ToList();
            //phan trang
            int pageNumber = (page ?? 1);
            int pageSize = 8;
            if (lstKQ.Count == 0)
            {
                ViewBag.ThongBao = "Không tìm thấy sản phẩm nào";
                
                return View(db.Giays.OrderBy(n => n.TenGiay).ToPagedList(pageNumber, pageSize));
            }
            ViewBag.ThongBao = "Đã tìm thấy " + lstKQ.Count + " kết quả";
            return View(lstKQ.OrderBy(n => n.TenGiay).ToPagedList(pageNumber, pageSize));
        }
    }
}