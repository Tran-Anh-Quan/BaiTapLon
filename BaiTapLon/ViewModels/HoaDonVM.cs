using BaiTapLon.Models;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace BaiTapLon.ViewModels
{
    public class HoaDonVM : INotifyPropertyChanged
    {
        public BindingList<HoaDon> HoaDonList { get; private set; } = new BindingList<HoaDon>();
        private readonly string connectionString = @"Data Source=LAPTOP-VTKAQD4V;Initial Catalog=QuanLyBanHang;Integrated Security=True";

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        // Lấy tất cả hóa đơn
        public void LayTatCaHoaDon()
        {
            HoaDonList.Clear();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT * FROM HoaDon";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            HoaDonList.Add(new HoaDon
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
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải danh sách hóa đơn: " + ex.Message);
                }
            }
        }

        // Lấy hóa đơn theo mã
        public HoaDon LayHoaDonTheoMa(string maHoaDon)
        {
            HoaDon hoaDon = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
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
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lấy hóa đơn: " + ex.Message);
                }
            }
            return hoaDon;
        }

        // Thêm hóa đơn mới
        public bool ThemHoaDon(HoaDon hoaDon)
        {
            if (hoaDon == null || string.IsNullOrWhiteSpace(hoaDon.MaHoaDon))
            {
                MessageBox.Show("Thông tin hóa đơn không hợp lệ.");
                return false;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    string sql = @"INSERT INTO HoaDon 
                                   (MaHoaDon, MaKhachHang, MaSanPham, SoLuong, DonGia, NgayLap, GiamGia, TongTien, VAT, ThanhToan)
                                   VALUES (@MaHD, @MaKH, @MaSP, @SoLuong, @DonGia, @NgayLap, @GiamGia, @TongTien, @VAT, @ThanhToan)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaHD", hoaDon.MaHoaDon);
                    cmd.Parameters.AddWithValue("@MaKH", hoaDon.MaKhachHang);
                    cmd.Parameters.AddWithValue("@MaSP", hoaDon.MaSanPham);
                    cmd.Parameters.AddWithValue("@SoLuong", hoaDon.SoLuong);
                    cmd.Parameters.AddWithValue("@DonGia", hoaDon.DonGia);
                    cmd.Parameters.AddWithValue("@NgayLap", hoaDon.NgayLap);
                    cmd.Parameters.AddWithValue("@GiamGia", hoaDon.GiamGia);
                    cmd.Parameters.AddWithValue("@TongTien", hoaDon.TongTien);
                    cmd.Parameters.AddWithValue("@VAT", hoaDon.VAT);
                    cmd.Parameters.AddWithValue("@ThanhToan", hoaDon.ThanhToan);

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        MessageBox.Show("Thêm hóa đơn thành công.");
                        LayTatCaHoaDon();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm hóa đơn: " + ex.Message);
                }
            }
            return false;
        }

        // Sửa hóa đơn
        public void SuaHoaDon(HoaDon hoaDon)
        {
            if (hoaDon == null || string.IsNullOrWhiteSpace(hoaDon.MaHoaDon))
            {
                MessageBox.Show("Thông tin hóa đơn không hợp lệ.");
                return;
            }

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
                    cmd.Parameters.AddWithValue("@MaHoaDon", hoaDon.MaHoaDon);
                    cmd.Parameters.AddWithValue("@MaKhachHang", hoaDon.MaKhachHang);
                    cmd.Parameters.AddWithValue("@MaSanPham", hoaDon.MaSanPham);
                    cmd.Parameters.AddWithValue("@SoLuong", hoaDon.SoLuong);
                    cmd.Parameters.AddWithValue("@DonGia", hoaDon.DonGia);
                    cmd.Parameters.AddWithValue("@NgayLap", hoaDon.NgayLap);
                    cmd.Parameters.AddWithValue("@GiamGia", hoaDon.GiamGia);
                    cmd.Parameters.AddWithValue("@TongTien", hoaDon.TongTien);
                    cmd.Parameters.AddWithValue("@VAT", hoaDon.VAT);
                    cmd.Parameters.AddWithValue("@ThanhToan", hoaDon.ThanhToan);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Sửa hóa đơn thành công.");
                        LayTatCaHoaDon();
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

        // Xóa hóa đơn
        public void XoaHoaDon(string maHoaDon)
        {
            if (string.IsNullOrEmpty(maHoaDon))
            {
                MessageBox.Show("Vui lòng nhập mã hóa đơn để xóa.");
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
                        LayTatCaHoaDon();
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

        // Tìm kiếm hóa đơn theo mã hoặc khách hàng
        public BindingList<HoaDon> TimKiemHoaDon(string keyword)
        {
            var result = new BindingList<HoaDon>(HoaDonList.Where(hd =>
               hd.MaHoaDon.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0 ||
               hd.MaKhachHang.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0).ToList());

            if (result.Count == 0)
            {
                MessageBox.Show("Không tìm thấy hóa đơn nào.");
            }
            return result;
        }
    }
}