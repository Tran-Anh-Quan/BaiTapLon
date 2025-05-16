using BaiTapLon.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiTapLon.ViewModels
{
    public class HoaDonChiTietVM
    {
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyBanHang4;Integrated Security=True";

        public List<HoaDonChiTietDTO> LayTatCaHoaDonGopChiTiet()
        {
            var danhSach = new List<HoaDonChiTietDTO>();

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT h.MaHoaDon, h.MaKhachHang, h.NgayLap, h.TongTien, h.VAT, h.ThanhToan,
                                  ct.MaSanPham, ct.SoLuong, ct.DonGia, ct.GiamGia
                           FROM HoaDon h
                           INNER JOIN ChiTietHoaDon ct ON h.MaHoaDon = ct.MaHoaDon";

                using (var cmd = new SqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        danhSach.Add(new HoaDonChiTietDTO
                        {
                            MaHoaDon = reader["MaHoaDon"].ToString(),
                            MaKhachHang = reader["MaKhachHang"].ToString(),
                            NgayLap = DateTime.Parse(reader["NgayLap"].ToString()),
                            TongTien = decimal.Parse(reader["TongTien"].ToString()),
                            VAT = decimal.Parse(reader["VAT"].ToString()),
                            ThanhToan = decimal.Parse(reader["ThanhToan"].ToString()),
                            MaSanPham = reader["MaSanPham"].ToString(),
                            SoLuong = int.Parse(reader["SoLuong"].ToString()),
                            DonGia = decimal.Parse(reader["DonGia"].ToString()),
                            GiamGia = decimal.Parse(reader["GiamGia"].ToString())
                        });
                    }
                }
            }

            return danhSach;
        }
    }

}



