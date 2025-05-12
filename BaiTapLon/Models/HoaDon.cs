using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiTapLon.Models
{
    public class HoaDon
    {
        public string MaHoaDon { get; set; }
        public string MaKhachHang { get; set; }
        public string MaSanPham { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public DateTime NgayLap { get; set; }
        public int GiamGia { get; set; }
        public decimal ThanhTien => SoLuong * DonGia * (1 - GiamGia / 100m);
        public decimal TongTien { get; set; }
        public decimal VAT { get; set; }
        public decimal ThanhToan { get; set; }
    }


}
