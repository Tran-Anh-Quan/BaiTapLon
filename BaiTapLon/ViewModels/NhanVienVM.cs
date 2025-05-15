using BaiTapLon.Models;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace BaiTapLon.ViewModels
{
    public class NhanVienVM : INotifyPropertyChanged
    {
        public BindingList<NhanVien> NhanVienList { get; private set; } = new BindingList<NhanVien>();
        private string connectionString = @"Data Source=LAPTOP-VTKAQD4V;Initial Catalog=QuanLyBanHang;Integrated Security=True";

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        // Lấy danh sách tất cả nhân viên
        public void LayTatCaNhanVien()
        {
            NhanVienList.Clear();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT * FROM NhanVien";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            NhanVienList.Add(new NhanVien
                            {
                                MaNhanVien = reader["MaNhanVien"].ToString(),
                                TenNhanVien = reader["TenNhanVien"].ToString(),
                                SoDienThoai = reader["SoDienThoai"].ToString(),
                                ChucVu = reader["ChucVu"].ToString(),
                                NgayVaoLam = DateTime.Parse(reader["NgayVaoLam"].ToString()),
                                Luong = decimal.Parse(reader["Luong"].ToString()),
                                GioiTinh = reader["GioiTinh"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải danh sách nhân viên: " + ex.Message);
                }
            }
        }

        // Lấy thông tin nhân viên theo mã
        public NhanVien LayNhanVienTheoMa(string maNhanVien)
        {
            NhanVien nv = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT * FROM NhanVien WHERE MaNhanVien = @MaNhanVien";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaNhanVien", maNhanVien);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            nv = new NhanVien
                            {
                                MaNhanVien = reader["MaNhanVien"].ToString(),
                                TenNhanVien = reader["TenNhanVien"].ToString(),
                                SoDienThoai = reader["SoDienThoai"].ToString(),
                                ChucVu = reader["ChucVu"].ToString(),
                                NgayVaoLam = DateTime.Parse(reader["NgayVaoLam"].ToString()),
                                Luong = decimal.Parse(reader["Luong"].ToString()),
                                GioiTinh = reader["GioiTinh"].ToString()
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lấy nhân viên: " + ex.Message);
                }
            }
            return nv;
        }

        // Thêm nhân viên mới
        public bool ThemNhanVien(NhanVien nv)
        {
            if (nv == null || string.IsNullOrWhiteSpace(nv.MaNhanVien))
            {
                MessageBox.Show("Thông tin nhân viên không hợp lệ.");
                return false;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    string sql = @"INSERT INTO NhanVien 
                                   (MaNhanVien, TenNhanVien, SoDienThoai, GioiTinh, ChucVu, NgayVaoLam, Luong)
                                   VALUES (@Ma, @Ten, @SDT, @GioiTinh, @ChucVu, @NgayVaoLam, @Luong)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Ma", nv.MaNhanVien);
                    cmd.Parameters.AddWithValue("@Ten", nv.TenNhanVien);
                    cmd.Parameters.AddWithValue("@SDT", nv.SoDienThoai);
                    cmd.Parameters.AddWithValue("@GioiTinh", nv.GioiTinh);
                    cmd.Parameters.AddWithValue("@ChucVu", nv.ChucVu);
                    cmd.Parameters.AddWithValue("@NgayVaoLam", nv.NgayVaoLam);
                    cmd.Parameters.AddWithValue("@Luong", nv.Luong);

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        MessageBox.Show("Thêm nhân viên thành công.");
                        LayTatCaNhanVien(); // Làm mới danh sách
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm nhân viên: " + ex.Message);
                }
            }
            return false;
        }

        // Xóa nhân viên
        public void XoaNhanVien(string maNhanVien)
        {
            if (string.IsNullOrEmpty(maNhanVien))
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên để xóa.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = "DELETE FROM NhanVien WHERE MaNhanVien = @MaNhanVien";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaNhanVien", maNhanVien);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Xóa nhân viên thành công.");
                        LayTatCaNhanVien(); // Làm mới danh sách
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy nhân viên để xóa.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa nhân viên: " + ex.Message);
                }
            }
        }

        // Sửa thông tin nhân viên
        public void SuaNhanVien(string maNhanVien, string tenNhanVien, string soDienThoai, string chucVu, DateTime ngayVaoLam, decimal luong, string gioiTinh)
        {
            if (string.IsNullOrWhiteSpace(maNhanVien))
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên để sửa.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = @"UPDATE NhanVien SET TenNhanVien = @TenNhanVien, SoDienThoai = @SoDienThoai, 
                                   ChucVu = @ChucVu, NgayVaoLam = @NgayVaoLam, Luong = @Luong, GioiTinh = @GioiTinh 
                                   WHERE MaNhanVien = @MaNhanVien";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaNhanVien", maNhanVien);
                    cmd.Parameters.AddWithValue("@TenNhanVien", tenNhanVien);
                    cmd.Parameters.AddWithValue("@SoDienThoai", soDienThoai);
                    cmd.Parameters.AddWithValue("@ChucVu", chucVu);
                    cmd.Parameters.AddWithValue("@NgayVaoLam", ngayVaoLam);
                    cmd.Parameters.AddWithValue("@Luong", luong);
                    cmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Sửa thông tin nhân viên thành công.");
                        LayTatCaNhanVien(); // Làm mới danh sách
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy nhân viên để sửa.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi sửa nhân viên: " + ex.Message);
                }
            }
        }

        // Tìm kiếm nhân viên theo tên hoặc mã

        public BindingList<NhanVien> TimKiemNhanVien(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                LayTatCaNhanVien(); // Tải lại danh sách từ database nếu từ khóa rỗng
                return NhanVienList;
            }

            var result = new BindingList<NhanVien>(NhanVienList.Where(nv =>
                nv.MaNhanVien.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0 ||
                nv.TenNhanVien.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0).ToList());

            if (result.Count == 0)
            {
                MessageBox.Show("Không tìm thấy nhân viên nào.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return result;
        }
    }
}