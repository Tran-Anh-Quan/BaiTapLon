using BaiTapLon.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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



    }
}
