using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Parser.SyntaxTree;
using webBanGiay.Models;
namespace webBanGiay.Controllers
{
    
    public class NguoiDungController : Controller
    {
        // GET: NguoiDung
        WebBanGiayDemoEntities db = new WebBanGiayDemoEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]

        public ActionResult Dangky()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Dangky(KhachHang kh)
        {
            if(ModelState.IsValid)
            { 
             //chèn dữ liệu vào bàng khách hàng
            db.KhachHangs.Add(kh);
            //lưu vào csdl
            db.SaveChanges();
            }

            return RedirectToAction("Dangnhap");
        }
        [HttpGet]
        public ActionResult Dangnhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangnhap(FormCollection f)
        {

            string staikhoan = f["txtTaiKhoan"].ToString();
            string smatkhau = f.Get("txtMatKhau").ToString();
            KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.TaiKhoan == staikhoan && n.MatKhau == smatkhau);
            if (kh != null)
            {
                ViewBag.thongbao = "Bạn đã đăng nhập thành công";
                Session["TaiKhoan"] = kh;
                ViewBag.thbao = ((KhachHang)Session["TaiKhoan"]).TaiKhoan;
                return RedirectToAction("Index");
                
            }
            
            ViewBag.thongbao = "Tên tài khoản hoặc mật khẩu không đúng !";
            
            return View();
        }
        public ActionResult DangXuat()
        {
            
            Session.Clear(); // Xóa tất cả các session
            Session.Abandon(); // Hủy tất cả các session
            return RedirectToAction("Index", "Home"); // Chuyển hướng đến trang chủ
        }
        public ActionResult TaiKhoanPartial()
        {
            ViewBag.thbao = ((KhachHang)Session["TaiKhoan"]).TaiKhoan;
            return PartialView();
        }
    }
}