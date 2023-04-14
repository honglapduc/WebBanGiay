using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webBanGiay.Models;
namespace webBanGiay.Controllers
{
    public class GioHangController : Controller
    {
        //lấy giỏ hàng
        WebBanGiayDemoEntities db = new WebBanGiayDemoEntities();
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if(lstGioHang == null)
            {
                // nếu chưa tồn tại thì tạo gio hang
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
        //thêm gio hang
        public ActionResult ThemGioHang(int iMaGiay,string strUrl)
        {
            Giay giay = db.Giays.SingleOrDefault(n=>n.MaGiay==iMaGiay);
            if(giay == null)
            {
                Response.StatusCode = 404;
                return null; 
            }
            List<GioHang> lstGioHang = LayGioHang();
            //kiễm tra giày này đã tồn tại trong session[goihang]
            GioHang sanpham = lstGioHang.Find(n=>n.iMaGiay==iMaGiay);
            if(sanpham == null)
            {
                sanpham = new GioHang(iMaGiay);
                lstGioHang.Add(sanpham);
                return Redirect(strUrl);
            }
            else
            {
                sanpham.iSoLuong++;
                return Redirect(strUrl);
            }
        }
        //sũa gio hang
        public ActionResult CapNhatGioHang(int iMaSP,FormCollection f)
        {
            /*//kiem tra sanpam
            Giay giay = db.Giays.SingleOrDefault(n=>n.MaGiay ==iMaSP);
            if (giay == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //lấy giỏ hàng từ session
            List<GioHang> lstGioHang = LayGioHang();
            //ktra sp có tồn tại trong session[giohang]
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMaGiay == iMaSP);
            if (sanpham != null)
            {
                //neu ton tai thì cho sua soluong
                sanpham.iSoLuong = int.Parse(f["txtSoLuong"].ToString());

            }

            return RedirectToAction("GioHang");*/
            // Lấy sản phẩm từ database
            Giay giay = db.Giays.SingleOrDefault(n => n.MaGiay == iMaSP);
            if (giay == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            // Lấy giỏ hàng từ session
            List<GioHang> lstGioHang = LayGioHang();

            // Kiểm tra sản phẩm đã tồn tại trong giỏ hàng chưa
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMaGiay == iMaSP);
            if (sanpham != null)
            {
                // Lấy số lượng tồn của sản phẩm từ bảng 'Giay'
                

                // Lấy số lượng cập nhật từ FormCollection
                int soLuongCapNhat = int.Parse(f["txtSoLuong"].ToString());

                // So sánh số lượng cập nhật với số lượng tồn
                if (soLuongCapNhat > 0 && soLuongCapNhat <= giay.SoLuongTon)
                {
                    sanpham.iSoLuong = soLuongCapNhat;
                }
                else
                {
                    // Xử lý khi số lượng cập nhật không hợp lệ
                    



                }
            }

            return RedirectToAction("GioHang");

        }
        //Xóa gio hang
        public ActionResult XoaGioHang(int iMaSP)
        {
            Giay giay = db.Giays.SingleOrDefault(n => n.MaGiay == iMaSP);
            if (giay == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> lstGioHang = LayGioHang();
            //ktra sp có tồn tại trong session[giohang]
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMaGiay == iMaSP);
            if (sanpham != null)
            {
                
                lstGioHang.RemoveAll(n => n.iMaGiay == iMaSP);
                
            }
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index","Home");
            }
            return RedirectToAction("GioHang");
        }
        //xay dung trang gio hang
        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lstGioHang = LayGioHang();

            return View(lstGioHang);
        }
        //tinh tong soluong va tong tien
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if(lstGioHang !=null)
            {
                iTongSoLuong = lstGioHang.Sum(n => n.iSoLuong);
            }
            return iTongSoLuong;
        }
        private double TongTien()
        {
            double dTongTien = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                dTongTien = lstGioHang.Sum(n => n.ThanhTien);
            }
            return dTongTien;
        }
        public ActionResult GioHangPartial()
        {
            if(TongSoLuong() == 0)
            {
                return PartialView();
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }
        //nguoi dùng chỉnh sữa gio hàng
        public ActionResult SuaGiohang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            List<GioHang> lstGioHang = LayGioHang();
            
            return View(lstGioHang);
        }

        //dat hang
        [HttpPost]
        public ActionResult DatHang()
        {
            //kiem tra dang nhap
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("Dangnhap", "NguoiDung");
            }
            //kiem tra gio hang
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            
            //them don hang
            DonHang ddh = new DonHang();
            KhachHang kh =(KhachHang) Session["TaiKhoan"];
            List<GioHang> gh = LayGioHang();
            ddh.MaKH = kh.MaKH;
            ddh.NgayDat = DateTime.Now;
            db.DonHangs.Add(ddh);
            db.SaveChanges();
            // them  chi tiet don hang
            foreach(var item in gh)
            {
                ChiTietDonHang ctDonghang = new ChiTietDonHang();
                ctDonghang.MaDonHang = ddh.MaDonHang;
                ctDonghang.MaGiay = item.iMaGiay;
                ctDonghang.SoLuong = item.iSoLuong;
                ctDonghang.DonGia = item.dDonGia.ToString();
                db.ChiTietDonHangs.Add(ctDonghang);


            }
            db.SaveChanges();
            /*if (TongSoLuong() == 0)
            {
                ViewBag.TongSoLuong = TongSoLuong();
            }*/
            




            return RedirectToAction("Index","Home");
        }
    }
}