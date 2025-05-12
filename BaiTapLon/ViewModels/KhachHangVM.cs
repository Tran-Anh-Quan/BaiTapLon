using BaiTapLon.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiTapLon.ViewModels
{
    public class KhachHangVM
    {
        public BindingList<KhachHang> KhachHangList = new BindingList<KhachHang>();
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyBanHang;Integrated Security=True";
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public KhachHang LayKhachHangTheoMa(string maKhachHang)
        {
            KhachHang kh = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
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
            return kh;
        }
        public BindingList<KhachHang> LayTatCaKhachHang()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM KhachHang";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

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
            return KhachHangList;
        }
        public void XoaKhachHang(string maKhachHang)
        {
            if (string.IsNullOrEmpty(maKhachHang))
            {
                MessageBox.Show("Vui lòng nhập mã khách hàng cần xóa.");
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


        public void SuaKhachHang(string maKhachHang, string tenKhachHang, string soDienThoai, string diaChi, string email)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = "UPDATE KhachHang SET TenKhachHang = @TenKhachHang, SoDienThoai = @SoDienThoai, DiaChi = @DiaChi, Email = @Email WHERE MaKhachHang = @MaKhachHang";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaKhachHang", maKhachHang);
                    cmd.Parameters.AddWithValue("@TenKhachHang", tenKhachHang);
                    cmd.Parameters.AddWithValue("@SoDienThoai", soDienThoai);
                    cmd.Parameters.AddWithValue("@DiaChi", diaChi);
                    cmd.Parameters.AddWithValue("@Email", email);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Sửa khách hàng thành công.");
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
        public bool ThemKhachHang(KhachHang kh)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = @"INSERT INTO KhachHang 
                    (MaKhachHang, TenKhachHang, SoDienThoai, DiaChi, Email)
                    VALUES (@Ma, @Ten, @SDT, @DiaChi, @Email)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Ma", kh.MaKhachHang);
                cmd.Parameters.AddWithValue("@Ten", kh.TenKhachHang);
                cmd.Parameters.AddWithValue("@SDT", kh.SoDienThoai);
                cmd.Parameters.AddWithValue("@DiaChi", kh.DiaChi);
                cmd.Parameters.AddWithValue("@Email", kh.Email);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
        }

    }

}
