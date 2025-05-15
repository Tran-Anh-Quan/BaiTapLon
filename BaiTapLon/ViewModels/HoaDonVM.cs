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
        public BindingList<ChiTietHoaDon> ChiTietHoaDonList { get; private set; } = new BindingList<ChiTietHoaDon>();

        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyBanHang;Integrated Security=True";

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        // Lấy danh sách tất cả hóa đơn
        public void LayTatCaHoaDon()
        {
             HoaDonList.Clear();
            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT * FROM HoaDon";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                HoaDonList.Add(new HoaDon
                                {
                                    MaHoaDon = reader["MaHoaDon"].ToString(),
                                    MaKhachHang = reader["MaKhachHang"].ToString(),
                                    NgayLap = DateTime.Parse(reader["NgayLap"].ToString()),
                                    TongTien = decimal.Parse(reader["TongTien"].ToString()),
                                    VAT = decimal.Parse(reader["VAT"].ToString()),
                                    ThanhToan = decimal.Parse(reader["ThanhToan"].ToString())
                                });
                            }
                        }
                    }
                    if (HoaDonList.Count == 0)
                    {
                        MessageBox.Show("Danh sách hóa đơn trống. Vui lòng thêm hóa đơn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Lỗi kết nối database: {ex.Message}. Vui lòng kiểm tra cấu hình SQL Server.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi tải danh sách hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Thêm hóa đơn
        public bool ThemHoaDon(HoaDon hd)
        {
            if (hd == null || string.IsNullOrWhiteSpace(hd.MaHoaDon))
            {
                MessageBox.Show("Thông tin hóa đơn không hợp lệ.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    string sql = @"INSERT INTO HoaDon (MaHoaDon,MaKhachHang, NgayLap,  TongTien, VAT, ThanhToan)
                                   VALUES (@MaHoaDon, @MaKhachHang,@NgayLap, @TongTien, @VAT, @ThanhToan)";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaHoaDon", hd.MaHoaDon);
                        cmd.Parameters.AddWithValue("@MaKhachHang", hd.MaKhachHang);
                        cmd.Parameters.AddWithValue("@NgayLap", hd.NgayLap);
                        cmd.Parameters.AddWithValue("@TongTien", hd.TongTien);
                        cmd.Parameters.AddWithValue("@VAT", hd.VAT);
                        cmd.Parameters.AddWithValue("@ThanhToan", hd.ThanhToan);

                        conn.Open();
                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Thêm hóa đơn thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LayTatCaHoaDon(); // Làm mới danh sách
                            return true;
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Lỗi kết nối database: {ex.Message}. Vui lòng kiểm tra cấu hình SQL Server.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi thêm hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return false;
        }
        public bool ThemHoaDonCT(ChiTietHoaDon cthd)
        {
            if (cthd == null || string.IsNullOrWhiteSpace(cthd.MaHoaDon))
            {
                MessageBox.Show("Thông tin hóa đơn chi tiết không hợp lệ.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    string sql = @"INSERT INTO ChiTietHoaDon (MaHoaDon, MaSanPham, SoLuong, DonGia, GiamGia)
                           VALUES (@MaHoaDon, @MaSanPham, @SoLuong, @DonGia, @GiamGia)";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaHoaDon", cthd.MaHoaDon);
                        cmd.Parameters.AddWithValue("@MaSanPham", cthd.MaSanPham);
                        cmd.Parameters.AddWithValue("@SoLuong", cthd.SoLuong);
                        cmd.Parameters.AddWithValue("@DonGia", cthd.DonGia);
                        cmd.Parameters.AddWithValue("@GiamGia", cthd.GiamGia);

                        conn.Open();
                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Thêm chi tiết hóa đơn thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("Không thêm được chi tiết hóa đơn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Lỗi kết nối database: {ex.Message}. Vui lòng kiểm tra cấu hình SQL Server.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi thêm chi tiết hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return false;
        }

        // Sửa hóa đơn
        public void SuaHoaDon(HoaDon hd)
        {
            if (string.IsNullOrWhiteSpace(hd.MaHoaDon))
            {
                MessageBox.Show("Vui lòng nhập mã hóa đơn để sửa.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = @"
                            UPDATE HoaDon 
                            SET MaKhachHang = @MaKhachHang, NgayLap = @NgayLap, 
                                TongTien = @TongTien, VAT = @VAT, ThanhToan = @ThanhToan 
                            WHERE MaHoaDon = @MaHoaDon";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaHoaDon", hd.MaHoaDon);
                        cmd.Parameters.AddWithValue("@MaKhachHang", hd.MaKhachHang);
                        cmd.Parameters.AddWithValue("@NgayLap", hd.NgayLap);
                        cmd.Parameters.AddWithValue("@TongTien", hd.TongTien);
                        cmd.Parameters.AddWithValue("@VAT", hd.VAT);
                        cmd.Parameters.AddWithValue("@ThanhToan", hd.ThanhToan);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Sửa hóa đơn thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LayTatCaHoaDon(); // Làm mới danh sách
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy hóa đơn để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Lỗi kết nối database: {ex.Message}. Vui lòng kiểm tra cấu hình SQL Server.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi sửa hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        // Sửa hóa đơn
        public void SuaHoaDonCT(ChiTietHoaDon cthd)
        {
            if (string.IsNullOrWhiteSpace(cthd.MaHoaDon))
            {
                MessageBox.Show("Vui lòng nhập mã hóa đơn để sửa.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = @"                       

                            UPDATE ChiTietHoaDon 
                            SET MaSanPham = @MaSanPham, GiamGia = @GiamGia, SoLuong = @SoLuong, DonGia = @DonGia 
                            WHERE MaHoaDon = @MaHoaDon AND MaSanPham = @OldMaSanPham;
                            ";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaHoaDon", cthd.MaHoaDon);
                        cmd.Parameters.AddWithValue("@MaSanPham", cthd.MaSanPham);
                        cmd.Parameters.AddWithValue("@SoLuong", cthd.SoLuong);
                        cmd.Parameters.AddWithValue("@DonGia", cthd.DonGia);
                        cmd.Parameters.AddWithValue("@GiamGia", cthd.GiamGia);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Sửa hóa đơn thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LayTatCaHoaDon(); // Làm mới danh sách
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy hóa đơn để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Lỗi kết nối database: {ex.Message}. Vui lòng kiểm tra cấu hình SQL Server.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi sửa hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        // Xóa hóa đơn
        public void XoaHoaDon(string maHoaDon)
        {
            if (string.IsNullOrEmpty(maHoaDon))
            {
                MessageBox.Show("Vui lòng nhập mã hóa đơn để xóa.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = "DELETE FROM ChiTietHoaDon WHERE MaHoaDon = @MaHoaDon";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Xóa hóa đơn thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LayTatCaHoaDon(); // Làm mới danh sách
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy hóa đơn để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Lỗi kết nối database: {ex.Message}. Vui lòng kiểm tra cấu hình SQL Server.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Tìm kiếm hóa đơn
        public BindingList<HoaDon> TimKiemHoaDon(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                LayTatCaHoaDon(); // Tải lại danh sách từ database nếu từ khóa rỗng
                return HoaDonList;
            }

            var result = new BindingList<HoaDon>(HoaDonList.Where(hd =>
                hd.MaHoaDon.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0 ||
                hd.MaKhachHang.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0).ToList());

            if (result.Count == 0)
            {
                MessageBox.Show("Không tìm thấy hóa đơn nào.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return result;
        }
    }
}