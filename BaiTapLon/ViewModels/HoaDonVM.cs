
using BaiTapLon.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiTapLon.ViewModels
{
    public class HoaDonVM : INotifyPropertyChanged
    {
        public BindingList<HoaDon> HoaDonList = new BindingList<HoaDon>();
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyBanHang;Integrated Security=True";
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public decimal TongTien
        {
            get
            {
                decimal tong = 0;
                foreach (HoaDon hd in HoaDonList)
                {
                    tong += hd.ThanhTien;
                }
                return tong;
            }
        }
        public decimal VAT
        {
            get
            {
                decimal vat = TongTien * 0.1m;
                return Math.Round(vat, 2);
            }
        }
        public HoaDon TimHoaDonTheoMa(string maHoaDon)
        {
            HoaDon hoaDon = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM HoaDon WHERE MaHoaDon = @MaHoaDon";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaHoaDon", maHoaDon);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        hoaDon = new HoaDon
                        {
                            MaHoaDon = reader["MaHoaDon"].ToString(),
                            MaKhachHang = reader["MaKhachHang"].ToString(),
                            MaSanPham = reader["MaSanPham"].ToString(),
                            SoLuong = int.Parse(reader["SoLuong"].ToString()),
                            DonGia = decimal.Parse(reader["DonGia"].ToString()),
                            NgayLap = DateTime.Parse(reader["NgayLap"].ToString()),
                            GiamGia = int.Parse(reader["GiamGia"].ToString()),

                        };
                    }
                }

            }
            return hoaDon;
        }
        public HoaDon LayHoaDonTheoMa(string maHoaDon)
        {
            HoaDon hd = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM HoaDon WHERE MaHoaDon = @ma";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ma", maHoaDon);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        hd = new HoaDon
                        {
                            MaHoaDon = reader["MaHoaDon"].ToString(),
                            MaKhachHang = reader["MaKhachHang"].ToString(),
                            MaSanPham = reader["MaSanPham"].ToString(),
                            SoLuong = Convert.ToInt32(reader["SoLuong"]),
                            DonGia = Convert.ToDecimal(reader["DonGia"]),
                            NgayLap = Convert.ToDateTime(reader["NgayLap"]),
                            GiamGia = Convert.ToInt32(reader["GiamGia"]),
                            TongTien = Convert.ToDecimal(reader["TongTien"]),
                            VAT = Convert.ToDecimal(reader["VAT"]),
                            ThanhToan = Convert.ToDecimal(reader["ThanhToan"])
                        };
                    }
                }
            }
            return hd;
        }

        public BindingList<HoaDon> LayTatCaHoaDon()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM HoaDon";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();              
                    while (reader.Read())
                    {
                        HoaDon hd = new HoaDon
                        {
                            MaHoaDon = reader["MaHoaDon"].ToString(),
                            MaKhachHang = reader["MaKhachHang"].ToString(),
                            MaSanPham = reader["MaSanPham"].ToString(),
                            SoLuong = Convert.ToInt32(reader["SoLuong"]),
                            DonGia = Convert.ToDecimal(reader["DonGia"]),
                            NgayLap = Convert.ToDateTime(reader["NgayLap"]),
                            GiamGia = Convert.ToInt32(reader["GiamGia"]),
                            TongTien = Convert.ToDecimal(reader["TongTien"]),
                            VAT = Convert.ToDecimal(reader["VAT"]),
                            ThanhToan = Convert.ToDecimal(reader["ThanhToan"])
                        };
                        HoaDonList.Add(hd);
                    }
            }
            return HoaDonList;
        }
        public bool ThemHoaDon(HoaDon hd)
        {
            if (hd == null)
            {
                MessageBox.Show("Hóa đơn không hợp lệ.");
                return false;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = @"INSERT INTO HoaDon 
               (MaHoaDon, MaKhachHang, MaSanPham, SoLuong, DonGia, NgayLap, GiamGia, TongTien, VAT, ThanhToan)
               VALUES 
               (@MaHD, @MaKH, @MaSP, @SoLuong, @DonGia, @NgayLap, @GiamGia, @TongTien, @VAT, @ThanhToan)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.Add("@MaHD", SqlDbType.NVarChar).Value = hd.MaHoaDon;
                cmd.Parameters.Add("@MaKH", SqlDbType.NVarChar).Value = hd.MaKhachHang;
                cmd.Parameters.Add("@MaSP", SqlDbType.NVarChar).Value = hd.MaSanPham;
                cmd.Parameters.Add("@SoLuong", SqlDbType.Int).Value = hd.SoLuong;
                cmd.Parameters.Add("@DonGia", SqlDbType.Decimal).Value = hd.DonGia;
                cmd.Parameters.Add("@NgayLap", SqlDbType.DateTime).Value = hd.NgayLap;
                cmd.Parameters.Add("@GiamGia", SqlDbType.Int).Value = hd.GiamGia;
                cmd.Parameters.Add("@TongTien", SqlDbType.Decimal).Value = hd.TongTien;
                cmd.Parameters.Add("@VAT", SqlDbType.Decimal).Value = hd.VAT;
                cmd.Parameters.Add("@ThanhToan", SqlDbType.Decimal).Value = hd.ThanhToan;

                conn.Open();

                try
                {
                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm hóa đơn: " + ex.Message);
                    return false;
                }
            }
        }

        public void XoaHoaDon(string maHoaDon)
        {
            if (string.IsNullOrEmpty(maHoaDon))
            {
                MessageBox.Show("Vui lòng nhập mã hóa đơn cần xóa.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = "DELETE FROM HoaDon WHERE MaHoaDon = @MaHoaDon";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaHoaDon", maHoaDon);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Xóa hóa đơn thành công.");
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy hóa đơn để xóa.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa hóa đơn: " + ex.Message);
                }
            }
        }
        public void SuaHoaDon(string maHoaDon, string maKhachHang, string maSanPham, int soLuong, decimal donGia, DateTime ngayLap, int giamGia, decimal tongTien, decimal vat, decimal thanhToan)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = @"UPDATE HoaDon 
                           SET MaKhachHang = @MaKhachHang, 
                               MaSanPham = @MaSanPham, 
                               SoLuong = @SoLuong, 
                               DonGia = @DonGia, 
                               NgayLap = @NgayLap, 
                               GiamGia = @GiamGia, 
                               TongTien = @TongTien, 
                               VAT = @VAT, 
                               ThanhToan = @ThanhToan 
                           WHERE MaHoaDon = @MaHoaDon";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                    cmd.Parameters.AddWithValue("@MaKhachHang", maKhachHang);
                    cmd.Parameters.AddWithValue("@MaSanPham", maSanPham);
                    cmd.Parameters.AddWithValue("@SoLuong", soLuong);
                    cmd.Parameters.AddWithValue("@DonGia", donGia);
                    cmd.Parameters.AddWithValue("@NgayLap", ngayLap);
                    cmd.Parameters.AddWithValue("@GiamGia", giamGia);
                    cmd.Parameters.AddWithValue("@TongTien", tongTien);
                    cmd.Parameters.AddWithValue("@VAT", vat);
                    cmd.Parameters.AddWithValue("@ThanhToan", thanhToan);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Sửa hóa đơn thành công.");
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy hóa đơn để sửa.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi sửa hóa đơn: " + ex.Message);
                }
            }
        }


    }

}

