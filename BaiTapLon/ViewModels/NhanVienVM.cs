using BaiTapLon.Models;
using BaiTapLon.View;
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
    public class NhanVienVM : INotifyPropertyChanged
    {
        public BindingList<NhanVien> NhanVienList = new BindingList<NhanVien>();
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyBanHang;Integrated Security=True";
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public NhanVien LayNhanVienTheoMa(string maNhanVien)
        {
            NhanVien nv = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
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
                            GioiTinh = reader["GioiTinh"].ToString(),
                            ChucVu = reader["ChucVu"].ToString(),
                            NgayVaoLam = DateTime.Parse(reader["NgayVaoLam"].ToString()),
                            Luong = decimal.Parse(reader["Luong"].ToString())
                        };
                    }
                }
            }
            return nv;
        }
        public BindingList<NhanVien> LayTatCaNhanVien()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM NhanVien";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    NhanVienList.Add(new NhanVien
                    {
                        MaNhanVien = reader["MaNhanVien"].ToString(),
                        TenNhanVien = reader["TenNhanVien"].ToString(),
                        SoDienThoai = reader["SoDienThoai"].ToString(),
                        GioiTinh = reader["GioiTinh"].ToString(),
                        ChucVu = reader["ChucVu"].ToString(),
                        NgayVaoLam = DateTime.Parse(reader["NgayVaoLam"].ToString()),
                        Luong = decimal.Parse(reader["Luong"].ToString())
                    });
                }
            }
            return NhanVienList;
        }
        public bool ThemNhanVien(NhanVien nv)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
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
                return rows > 0;
            }
        }
        public void XoaNhanVien(string maNhanVien)
        {
            frmQuanLyNhanVien form = new frmQuanLyNhanVien();
            if (string.IsNullOrEmpty(maNhanVien))
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên cần xóa.");
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
        public void SuaNhanVien(string maNhanVien, string tenNhanVien, string soDienThoai, string chucVu, DateTime ngayVaoLam, decimal luong)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = "UPDATE NhanVien SET TenNhanVien = @TenNhanVien, SoDienThoai = @SoDienThoai, ChucVu = @ChucVu, NgayVaoLam = @NgayVaoLam, Luong = @Luong WHERE MaNhanVien = @MaNhanVien";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaNhanVien", maNhanVien);
                    cmd.Parameters.AddWithValue("@TenNhanVien", tenNhanVien);
                    cmd.Parameters.AddWithValue("@SoDienThoai", soDienThoai);
                    cmd.Parameters.AddWithValue("@ChucVu", chucVu);
                    cmd.Parameters.AddWithValue("@NgayVaoLam", ngayVaoLam);
                    cmd.Parameters.AddWithValue("@Luong", luong);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Sửa nhân viên thành công.");

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
    }
}
