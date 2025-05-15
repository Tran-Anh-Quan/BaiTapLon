using BaiTapLon.Models;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace BaiTapLon.ViewModels
{
    public class SanPhamVM : INotifyPropertyChanged
    {
        public BindingList<SanPham> SanphamList { get; private set; } = new BindingList<SanPham>();
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyBanHang;Integrated Security=True";
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        // Lấy sản phẩm theo mã
        public SanPham LaySanPhamTheoMa(string maSanPham)
        {
            SanPham sp = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
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
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tìm sản phẩm: " + ex.Message);
                }
            }
            return sp;
        }

        // Lấy tất cả sản phẩm
        public void LayTatCaSanPham()
        {
            SanphamList.Clear();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT * FROM SanPham";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
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
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải danh sách sản phẩm: " + ex.Message);
                }
            }
        }

        // Thêm sản phẩm
        public bool ThemSanPham(SanPham sp)
        {
            if (sp == null || string.IsNullOrWhiteSpace(sp.MaSanPham) || string.IsNullOrWhiteSpace(sp.TenSanPham))
            {
                MessageBox.Show("Thông tin sản phẩm không hợp lệ.");
                return false;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    string sql = @"INSERT INTO SanPham (MaSanPham, TenSanPham, DonGia, SoLuongTon)
                                   VALUES (@Ma, @Ten, @DonGia, @SoLuongTon)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Ma", sp.MaSanPham);
                    cmd.Parameters.AddWithValue("@Ten", sp.TenSanPham);
                    cmd.Parameters.AddWithValue("@DonGia", sp.DonGia);
                    cmd.Parameters.AddWithValue("@SoLuongTon", sp.SoLuongTon);

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        MessageBox.Show("Thêm sản phẩm thành công.");
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm sản phẩm: " + ex.Message);
                }
            }
            return false;
        }

        // Xóa sản phẩm
        public void XoaSanPham(string maSanPham)
        {
            if (string.IsNullOrEmpty(maSanPham))
            {
                MessageBox.Show("Vui lòng nhập mã sản phẩm để xóa.");
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
                        LayTatCaSanPham(); // Làm mới danh sách
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

        // Sửa sản phẩm
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
                    string sql = @"UPDATE SanPham SET TenSanPham = @TenSanPham, DonGia = @DonGia, SoLuongTon = @SoLuongTon 
                                   WHERE MaSanPham = @MaSanPham";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaSanPham", maSanPham);
                    cmd.Parameters.AddWithValue("@TenSanPham", tenSanPham);
                    cmd.Parameters.AddWithValue("@DonGia", donGia);
                    cmd.Parameters.AddWithValue("@SoLuongTon", soLuongTon);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Sửa sản phẩm thành công.");
                        LayTatCaSanPham(); // Làm mới danh sách
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy sản phẩm để sửa.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi sửa sản phẩm: " + ex.Message);
                }
            }
        }

        // Tìm kiếm sản phẩm
        public BindingList<SanPham> TimKiemSanPham(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                LayTatCaSanPham(); // Tải lại danh sách từ database nếu từ khóa rỗng
                if (SanphamList.Count == 0)
                {
                    MessageBox.Show("Danh sách sản phẩm trống. Vui lòng thêm sản phẩm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return SanphamList;
            }

            var result = new BindingList<SanPham>(SanphamList.Where(sp =>
                sp.MaSanPham.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0 ||
                sp.TenSanPham.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0).ToList());

            if (result.Count == 0)
            {
                MessageBox.Show("Không tìm thấy sản phẩm nào.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return result;

        }
    }
}