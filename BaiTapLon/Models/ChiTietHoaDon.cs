using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiTapLon.Models
{
    public class ChiTietHoaDon
    {
        public string MaHoaDon { get; set; }      // FK đến HoaDon
        public string MaSanPham { get; set; }     // FK đến SanPham
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public int GiamGia { get; set; }

        public decimal ThanhTien => SoLuong * DonGia * (1 - GiamGia / 100m);
    }

}
