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
    public class TaiKhoanVM : INotifyPropertyChanged
    {
        public BindingList<TaiKhoan> List = new BindingList<TaiKhoan>();
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyBanHang;Integrated Security=True";
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public bool DangNhapTaiKhoan(string tenDangNhap, string matKhau)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM TaiKhoan WHERE TenDangNhap = @user AND MatKhau = @pass";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add("@user", SqlDbType.NVarChar).Value = tenDangNhap.Trim();
                    cmd.Parameters.Add("@pass", SqlDbType.NVarChar).Value = matKhau.Trim();

                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();

                    return count > 0;
                }
            }
        }
        public TaiKhoan LayTaiKhoanTheoMaNhanVien(string maNhanVien)
        {
            TaiKhoan tk = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM TaiKhoan WHERE MaNhanVien = @MaNhanVien";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaNhanVien", maNhanVien);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        tk = new TaiKhoan
                        {
                            TenDangNhap = reader["TenDangNhap"].ToString(),
                            MatKhau = reader["MatKhau"].ToString(),
                            MaNhanVien = reader["MaNhanVien"].ToString()
                        };
                    }
                }
            }
            return tk;
        }
        public BindingList<TaiKhoan> LayTatCaTaiKhoan()
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM TaiKhoan";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    List.Add(new TaiKhoan
                    {
                        TenDangNhap = reader["TenDangNhap"].ToString(),
                        MatKhau = reader["MatKhau"].ToString(),
                        MaNhanVien = reader["MaNhanVien"].ToString()
                    });
                }
            }

            return List;
        }
        public void SuaTaiKhoan(string maNhanVien, string tenDangNhap, string matKhau)
        {
            if (string.IsNullOrWhiteSpace(maNhanVien))
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên để sửa tài khoản.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = @"UPDATE TaiKhoan SET TenDangNhap = @TenDangNhap, MatKhau = @MatKhau 
                           WHERE MaNhanVien = @MaNhanVien";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaNhanVien", maNhanVien);
                    cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                    cmd.Parameters.AddWithValue("@MatKhau", matKhau);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        MessageBox.Show("Sửa tài khoản thành công.");
                    else
                        MessageBox.Show("Không tìm thấy tài khoản để sửa.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi sửa tài khoản: " + ex.Message);
                }
            }
        }

        public void XoaTaiKhoan(string maNhanVien)
        {
            if (string.IsNullOrEmpty(maNhanVien))
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên để xóa tài khoản.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = "DELETE FROM TaiKhoan WHERE MaNhanVien = @MaNhanVien";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaNhanVien", maNhanVien);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        MessageBox.Show("Xóa tài khoản thành công.");
                    else
                        MessageBox.Show("Không tìm thấy tài khoản để xóa.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa tài khoản: " + ex.Message);
                }
            }
        }

        public bool ThemTaiKhoan(TaiKhoan tk)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = @"INSERT INTO TaiKhoan (TenDangNhap, MatKhau, MaNhanVien)
                       VALUES (@TenDangNhap, @MatKhau, @MaNhanVien)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@TenDangNhap", tk.TenDangNhap);
                cmd.Parameters.AddWithValue("@MatKhau", tk.MatKhau);
                cmd.Parameters.AddWithValue("@MaNhanVien", tk.MaNhanVien);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
        }


    }
}
