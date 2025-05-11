using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiTapLon.Models
{
    public class NhanVien
    {
        public string MaNhanVien { get; set; }
        public string TenNhanVien { get; set; }
        public string ChucVu { get; set; }
        public DateTime NgayVaoLam { get; set; }
        public string SoDienThoai { get; set; }
        public decimal Luong { get; set; }
        public string GioiTinh { get; set; }
    }
}
