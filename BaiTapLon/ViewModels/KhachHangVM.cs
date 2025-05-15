using BaiTapLon.Models;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace BaiTapLon.ViewModels
{
    public class KhachHangVM : INotifyPropertyChanged
    {
        public BindingList<KhachHang> KhachHangList { get; private set; } = new BindingList<KhachHang>();
        private readonly string connectionString = @"Data Source=LAPTOP-I4S7V50U\SQLEXPRESS;Initial Catalog=QuanLyBanHang;Integrated Security=True;TrustServerCertificate=True";

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        // Lấy danh sách tất cả khách hàng
        public void LayTatCaKhachHang()
        {
            KhachHangList.Clear();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT * FROM KhachHang";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            KhachHangList.Add(new KhachHang
                            {
                                MaKhachHang = reader["MaKhachHang"].ToString(),
                                TenKhachHang = reader["TenKhachHang"].ToString(),
                                SoDienThoai = reader["SoDienThoai"].ToString(),
                                DiaChi = reader["DiaChi"].ToString(),
                                Email = reader["Email"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải danh sách khách hàng: " + ex.Message);
                }
            }
        }

        // Lấy thông tin khách hàng theo mã
        public KhachHang LayKhachHangTheoMa(string maKhachHang)
        {
            KhachHang kh = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT * FROM KhachHang WHERE MaKhachHang = @MaKhachHang";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaKhachHang", maKhachHang);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            kh = new KhachHang
                            {
                                MaKhachHang = reader["MaKhachHang"].ToString(),
                                TenKhachHang = reader["TenKhachHang"].ToString(),
                                SoDienThoai = reader["SoDienThoai"].ToString(),
                                DiaChi = reader["DiaChi"].ToString(),
                                Email = reader["Email"].ToString()
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lấy khách hàng: " + ex.Message);
                }
            }
            return kh;
        }

        // Thêm khách hàng mới
        public bool ThemKhachHang(KhachHang kh)
        {
            if (kh == null || string.IsNullOrWhiteSpace(kh.MaKhachHang))
            {
                MessageBox.Show("Thông tin khách hàng không hợp lệ.");
                return false;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    string sql = @"INSERT INTO KhachHang (MaKhachHang, TenKhachHang, SoDienThoai, DiaChi, Email)
                                   VALUES (@Ma, @Ten, @SDT, @DiaChi, @Email)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Ma", kh.MaKhachHang);
                    cmd.Parameters.AddWithValue("@Ten", kh.TenKhachHang);
                    cmd.Parameters.AddWithValue("@SDT", kh.SoDienThoai);
                    cmd.Parameters.AddWithValue("@DiaChi", kh.DiaChi);
                    cmd.Parameters.AddWithValue("@Email", kh.Email);

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        MessageBox.Show("Thêm khách hàng thành công.");
                        LayTatCaKhachHang(); // Làm mới danh sách
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm khách hàng: " + ex.Message);
                }
            }
            return false;
        }

        // Xóa khách hàng
        public void XoaKhachHang(string maKhachHang)
        {
            if (string.IsNullOrEmpty(maKhachHang))
            {
                MessageBox.Show("Vui lòng nhập mã khách hàng để xóa.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = "DELETE FROM KhachHang WHERE MaKhachHang = @MaKhachHang";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaKhachHang", maKhachHang);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Xóa khách hàng thành công.");
                        LayTatCaKhachHang(); // Làm mới danh sách
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy khách hàng để xóa.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa khách hàng: " + ex.Message);
                }
            }
        }

        // Sửa thông tin khách hàng
        public void SuaKhachHang(string maKhachHang, string tenKhachHang, string soDienThoai, string diaChi, string email)
        {
            if (string.IsNullOrWhiteSpace(maKhachHang))
            {
                MessageBox.Show("Vui lòng nhập mã khách hàng để sửa.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = @"UPDATE KhachHang SET TenKhachHang = @TenKhachHang, 
                                   SoDienThoai = @SoDienThoai, DiaChi = @DiaChi, Email = @Email 
                                   WHERE MaKhachHang = @MaKhachHang";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaKhachHang", maKhachHang);
                    cmd.Parameters.AddWithValue("@TenKhachHang", tenKhachHang);
                    cmd.Parameters.AddWithValue("@SoDienThoai", soDienThoai);
                    cmd.Parameters.AddWithValue("@DiaChi", diaChi);
                    cmd.Parameters.AddWithValue("@Email", email);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Sửa thông tin khách hàng thành công.");
                        LayTatCaKhachHang(); // Làm mới danh sách
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy khách hàng để sửa.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi sửa khách hàng: " + ex.Message);
                }
            }
        }

        // Tìm kiếm khách hàng theo tên hoặc mã
        public BindingList<KhachHang> TimKiemKhachHang(string keyword)
        {
            var result = new BindingList<KhachHang>(KhachHangList.Where(kh =>
                kh.MaKhachHang.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0 ||
                kh.TenKhachHang.IndexOf(keyword, StringComparison.OrdinalIgnoreCase)>= 0) .ToList());

            if (result.Count == 0)
            {
                MessageBox.Show("Không tìm thấy khách hàng nào.");
            }
            return result;
        }
    }
}