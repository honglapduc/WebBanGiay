using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webBanGiay.Models
{
    public class GioHang
    {
        WebBanGiayDemoEntities db = new WebBanGiayDemoEntities();
        public int iMaGiay { get; set; }
        public string sTenGiay { get; set; }
        public string sHinhAnh { get; set; }
        public double dDonGia { get; set; }
        public int iSoLuong { get; set; }
        public double ThanhTien
        {
            get { return iSoLuong * dDonGia; }
        }
        //tạo giỏ hàng
        public GioHang(int MaGiay)
        {
            iMaGiay = MaGiay;
            Giay giay = db.Giays.Single(n => n.MaGiay == iMaGiay);
            sTenGiay = giay.TenGiay;
            sHinhAnh = giay.AnhGiay;
            dDonGia =double.Parse(giay.GiaBan.ToString());
            iSoLuong = 1;

        }
    }
}