using BaiTapLon.Models;
using BaiTapLon.View;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BaiTapLon.ViewModels
{
    public class TaiKhoanVM : INotifyPropertyChanged
    {
        public BindingList<TaiKhoan> List = new BindingList<TaiKhoan>();
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyBanHang;Integrated Security=True";

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public bool DangNhapTaiKhoan(string tenDangNhap, string matKhau, Form loginForm)
        {
            if (string.IsNullOrWhiteSpace(tenDangNhap) || string.IsNullOrWhiteSpace(matKhau))
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không được để trống.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT MatKhau FROM TaiKhoan WHERE TenDangNhap = @user";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@user", tenDangNhap.Trim());
                        object result = cmd.ExecuteScalar();

                        if (result != null && result.ToString() == matKhau)
                        {
                            loginForm.Hide();
                            var mainForm = new frmQuanLyTaiKhoan();
                            mainForm.ShowDialog();
                            loginForm.Close();
                            return true;
                        }
                        else if (result == null)
                        {
                            MessageBox.Show("Tên đăng nhập không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                        else
                        {
                            MessageBox.Show("Mật khẩu không đúng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Lỗi kết nối database: {ex.Message}. Vui lòng kiểm tra cấu hình SQL Server.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi đăng nhập: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public void LoadTaiKhoanData()
        {
            List.Clear();
            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT * FROM TaiKhoan";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                List.Add(new TaiKhoan
                                {
                                    MaNhanVien = reader["MaNhanVien"].ToString(),
                                    TenDangNhap = reader["TenDangNhap"].ToString(),
                                    MatKhau = reader["MatKhau"].ToString()
                                });
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Lỗi kết nối database: {ex.Message}. Vui lòng kiểm tra cấu hình SQL Server.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi tải danh sách tài khoản: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public BindingList<TaiKhoan> TimKiemTaiKhoan(string tenDangNhap)
        {
            var result = new BindingList<TaiKhoan>();

            if (string.IsNullOrWhiteSpace(tenDangNhap))
            {
                LoadTaiKhoanData();
                return List;
            }

            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT * FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap.Trim());
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(new TaiKhoan
                                {
                                    MaNhanVien = reader["MaNhanVien"].ToString(),
                                    TenDangNhap = reader["TenDangNhap"].ToString(),
                                    MatKhau = reader["MatKhau"].ToString()
                                });
                            }
                        }
                    }

                    if (result.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy tài khoản với tên đăng nhập này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadTaiKhoanData();
                        return List;
                    }
                    return result;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Lỗi kết nối database: {ex.Message}. Vui lòng kiểm tra cấu hình SQL Server.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return List;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi tìm kiếm tài khoản: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return List;
                }
            }
        }

        public void SuaTaiKhoan(string tenDangNhap, string matKhau, string maNhanVien)
        {
            if (string.IsNullOrWhiteSpace(tenDangNhap) || string.IsNullOrWhiteSpace(matKhau) || string.IsNullOrWhiteSpace(maNhanVien))
            {
                MessageBox.Show("Thông tin không được để trống.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = "UPDATE TaiKhoan SET MatKhau = @MatKhau, MaNhanVien = @MaNhanVien WHERE TenDangNhap = @TenDangNhap";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                        cmd.Parameters.AddWithValue("@MatKhau", matKhau);
                        cmd.Parameters.AddWithValue("@MaNhanVien", maNhanVien);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Sửa tài khoản thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy tài khoản để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Lỗi kết nối database: {ex.Message}. Vui lòng kiểm tra cấu hình SQL Server.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi sửa tài khoản: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void XoaTaiKhoan(string tenDangNhap)
        {
            if (string.IsNullOrWhiteSpace(tenDangNhap))
            {
                MessageBox.Show("Tên đăng nhập không được để trống.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = "DELETE FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Xóa tài khoản thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy tài khoản để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Lỗi kết nối database: {ex.Message}. Vui lòng kiểm tra cấu hình SQL Server.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa tài khoản: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public bool ThemTaiKhoan(TaiKhoan tk)
        {
            if (tk == null || string.IsNullOrWhiteSpace(tk.TenDangNhap) || string.IsNullOrWhiteSpace(tk.MatKhau) || string.IsNullOrWhiteSpace(tk.MaNhanVien))
            {
                MessageBox.Show("Thông tin tài khoản không được để trống.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Check if MaNhanVien exists in NhanVien table
                    string checkSql = "SELECT COUNT(*) FROM NhanVien WHERE MaNhanVien = @MaNhanVien";
                    using (var checkCmd = new SqlCommand(checkSql, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@MaNhanVien", tk.MaNhanVien);
                        int count = (int)checkCmd.ExecuteScalar();
                        if (count == 0)
                        {
                            MessageBox.Show($"Mã nhân viên {tk.MaNhanVien} không tồn tại trong bảng NhanVien.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }

                    // Proceed with the insert if MaNhanVien exists
                    string sql = "INSERT INTO TaiKhoan (TenDangNhap, MatKhau, MaNhanVien) VALUES (@TenDangNhap, @MatKhau, @MaNhanVien)";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenDangNhap", tk.TenDangNhap);
                        cmd.Parameters.AddWithValue("@MatKhau", tk.MatKhau);
                        cmd.Parameters.AddWithValue("@MaNhanVien", tk.MaNhanVien);
                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Thêm tài khoản thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return true;
                        }
                        return false;
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Lỗi kết nối database: {ex.Message}. Vui lòng kiểm tra cấu hình SQL Server.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi thêm tài khoản: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }
    }
}