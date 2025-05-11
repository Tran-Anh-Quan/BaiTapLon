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
    public class SanPhamVM : INotifyPropertyChanged
    {
        public BindingList<SanPham> SanphamList = new BindingList<SanPham>();
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyBanHang;Integrated Security=True";
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public SanPham LaySanPhamTheoMa(string maSanPham)
        {
            SanPham sp = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM SanPham WHERE MaSanPham = @MaSanPham";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaSanPham", maSanPham);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        sp = new SanPham
                        {
                            MaSanPham = reader["MaSanPham"].ToString(),
                            TenSanPham = reader["TenSanPham"].ToString(),
                            DonGia = decimal.Parse(reader["DonGia"].ToString()),
                            SoLuongTon = int.Parse(reader["SoLuongTon"].ToString())       
                        };
                    }
                }
            }
            return sp;
        }
        public BindingList<SanPham> LayTatCaSanPham()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM SanPham";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    SanphamList.Add(new SanPham
                    {
                        MaSanPham = reader["MaSanPham"].ToString(),
                        TenSanPham = reader["TenSanPham"].ToString(),
                        DonGia = decimal.Parse(reader["DonGia"].ToString()),
                        SoLuongTon = int.Parse(reader["SoLuongTon"].ToString())
                    });
                }
            }
            return SanphamList;
        }
        public bool ThemSanPham(SanPham sp)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = @"INSERT INTO SanPham 
                       (MaSanPham, TenSanPham, DonGia, SoLuongTon)
                       VALUES (@Ma, @Ten, @DonGia, @SoLuongTon)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Ma", sp.MaSanPham);
                cmd.Parameters.AddWithValue("@Ten", sp.TenSanPham);
                cmd.Parameters.AddWithValue("@DonGia", sp.DonGia);
                cmd.Parameters.AddWithValue("@SoLuongTon", sp.SoLuongTon);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
        }
        public void XoaSanPham(string maSanPham)
        {
            if (string.IsNullOrEmpty(maSanPham))
            {
                MessageBox.Show("Vui lòng nhập mã sản phẩm cần xóa.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = "DELETE FROM SanPham WHERE MaSanPham = @MaSanPham";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaSanPham", maSanPham);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Xóa sản phẩm thành công.");
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy sản phẩm để xóa.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa sản phẩm: " + ex.Message);
                }
            }
        }
        public void SuaSanPham(string maSanPham, string tenSanPham, decimal donGia, int soLuongTon)
        {
            if (string.IsNullOrWhiteSpace(maSanPham))
            {
                MessageBox.Show("Vui lòng nhập mã sản phẩm để sửa.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = @"UPDATE SanPham SET TenSanPham = @TenSanPham,DonGia = @DonGia,SoLuongTon = @SoLuongTon WHERE MaSanPham = @MaSanPham";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaSanPham", maSanPham);
                        cmd.Parameters.AddWithValue("@TenSanPham", tenSanPham);
                        cmd.Parameters.AddWithValue("@DonGia", donGia);
                        cmd.Parameters.AddWithValue("@SoLuongTon", soLuongTon);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Sửa sản phẩm thành công.");

                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy sản phẩm để sửa.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi sửa sản phẩm: " + ex.Message);
                }
            }
        }


    }
}
