using BaiTapLon.ViewModels;
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
        public DateTime NgayLap { get; set; }
        public decimal TongTien { get; set; }
        public decimal VAT { get; set; }
        public decimal ThanhToan { get; set; }

        public List<ChiTietHoaDon> ChiTietHoaDons { get; set; } = new();
    }



}
