using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webBanGiay.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;
using System.Web.UI.WebControls;

namespace webBanGiay.Controllers
{
    public class QuanLySPController : Controller
    {
        WebBanGiayDemoEntities db = new WebBanGiayDemoEntities();
        // GET: QuanLySP
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 9;
            return View(db.Giays.ToList().OrderBy(n=>n.MaGiay).ToPagedList(pageNumber, pageSize));
        }
        // thêm 
        [HttpGet]
        public ActionResult ThemMoi()
        {
            ViewBag.MaLoai = new SelectList( db.Loais.ToList().OrderBy(n=>n.TenLoai), "MaLoai","TenLoai");
            ViewBag.MaNSX =new SelectList( db.NhaSanXuats.ToList().OrderBy(n=>n.TenNSX), "MaNSX", "TenNSX");
            return View();

        }
        [HttpPost]
        public ActionResult ThemMoi(Giay giay,HttpPostedFileBase fileUpload)
        {
           
            // dưa dữ liệu dropdown list
            ViewBag.MaLoai = new SelectList(db.Loais.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats.ToList().OrderBy(n => n.TenNSX), "MaNSX", "TenNSX");
            //ktra anh giay
            if (fileUpload == null)
            {
                ViewBag.ThongBao = "Chọn hình ảnh";
                return View();
            }
            if (giay.SoLuongTon < 0)
            {
                ViewBag.ThongBaoSL = "Số lượng sản phẩm không hợp lệ";
                return View();
            }
            // thêm vào csdl, ModelState.IsValid nếu thảo mảng tất cả các dk 
            if (ModelState.IsValid)
            {
                //luu tên file
                var fileName = Path.GetFileName(fileUpload.FileName);
                //luu đường dẫn
                var path = Path.Combine(Server.MapPath("~/AnhGiay"), fileName);
                //ktra hinh anh ton tai chua
                if (System.IO.File.Exists(path))
                {
                    ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                }
                else
                {
                    fileUpload.SaveAs(path);
                }
                giay.AnhGiay = fileUpload.FileName;
                db.Giays.Add(giay);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        //chỉnh sua sp
        [HttpGet]
        public ActionResult ChinhSua( int MaGiay, HttpPostedFileBase fileUpload)
        {
            //lấy đối tượng giày theo mã
            Giay giay = db.Giays.SingleOrDefault(n=> n.MaGiay == MaGiay);
            if (giay == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaLoai = new SelectList(db.Loais.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai",giay.MaLoai);
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats.ToList().OrderBy(n => n.TenNSX), "MaNSX", "TenNSX",giay.MaNSX);

            return View(giay);
        }
        [HttpPost]
        public ActionResult ChinhSua(Giay giay)
        {
           
            
           
            // thêm vào csdl, ModelState.IsValid nếu thảo mảng tất cả các dk 
            if (ModelState.IsValid)
            {
                //cap nhật trong model
                db.Entry(giay).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            // dưa dữ liệu dropdown list
            ViewBag.MaLoai = new SelectList(db.Loais.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai", giay.MaLoai);
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats.ToList().OrderBy(n => n.TenNSX), "MaNSX", "TenNSX", giay.MaNSX);
            return RedirectToAction("Index");
        }
        public ActionResult HienThi(int MaGiay)
        {
            Giay giay = db.Giays.SingleOrDefault(n => n.MaGiay == MaGiay);
            if (giay == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            

            return View(giay);
        }
        [HttpGet]
        public ActionResult XoaSp(int MaGiay)
        {
            Giay giay = db.Giays.SingleOrDefault(n => n.MaGiay == MaGiay);
            if (giay == null)
            {
                Response.StatusCode = 404;
                return null;
            }


            return View(giay);  
        }
        [HttpPost,ActionName("XoaSp")]
        public ActionResult XacNhanXoa(int MaGiay)
        {
            Giay giay = db.Giays.SingleOrDefault(n=>n.MaGiay== MaGiay);
            if(giay == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.Giays.Remove(giay);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}