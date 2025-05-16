using BaiTapLon.Models;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace BaiTapLon.ViewModels
{
    public class ChiTietHoaDonVM : INotifyPropertyChanged
    {
        public BindingList<HoaDon> HoaDonList { get; private set; } = new BindingList<HoaDon>();
        public BindingList<ChiTietHoaDon> ChiTietHoaDonList { get; private set; } = new BindingList<ChiTietHoaDon>();

        private readonly string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyBanHang4;Integrated Security=True";

        private decimal tongTien;
        public decimal TongTien
        {
            get => tongTien;
            private set
            {
                tongTien = value;
                OnPropertyChanged(nameof(TongTien));
            }
        }

        private decimal vat;
        public decimal VAT
        {
            get => vat;
            private set
            {
                vat = value;
                OnPropertyChanged(nameof(VAT));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        // Lấy tất cả hóa đơn
        public BindingList<ChiTietHoaDon> LayTatCaHoaDonCT()
        {
            ChiTietHoaDonList.Clear();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT * FROM ChiTietHoaDon";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ChiTietHoaDonList.Add(new ChiTietHoaDon
                            {
                                MaHoaDon = reader["MaHoaDon"].ToString(),
                                MaSanPham = reader["MaSanPham"].ToString(),
                                SoLuong = Convert.ToInt32(reader["SoLuong"]),
                                DonGia = Convert.ToDecimal(reader["DonGia"]),
                            });
                        }
                    }

                    // Tính toán tổng tiền và VAT
                    TinhTongTienVaVAT();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải danh sách hóa đơn: " + ex.Message);
                }
            }
            return ChiTietHoaDonList;
        }

        // Tính toán tổng tiền và VAT
        private void TinhTongTienVaVAT()
        {
            TongTien = HoaDonList.Sum(hd => hd.TongTien);
            VAT = Math.Round(TongTien * 0.1m, 2); // VAT 10%
        }

        // Thanh toán hóa đơn
        public void ThanhToanHoaDon(string maHoaDon)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = @"UPDATE HoaDon SET ThanhToan = TongTien + VAT WHERE MaHoaDon = @MaHoaDon";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaHoaDon", maHoaDon);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Thanh toán hóa đơn thành công.");
                        LayTatCaHoaDonCT(); // Làm mới danh sách hóa đơn
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy hóa đơn để thanh toán.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thanh toán hóa đơn: " + ex.Message);
                }
            }
        }
        public DataTable GetHoaDonReportData(string maHoaDon)
        {
            var selectedHoaDon = HoaDonList.FirstOrDefault(hd => hd.MaHoaDon == maHoaDon);
            var selectedChiTietHoaDon = ChiTietHoaDonList.FirstOrDefault(cthd => cthd.MaHoaDon == maHoaDon);

            if (selectedHoaDon == null)
            {
                MessageBox.Show("Không tìm thấy hóa đơn với mã này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }

            System.Data.DataSet ds = new System.Data.DataSet();
            DataTable dt = ds.Tables["HoaDonTable"];

            // Kiểm tra xem DataTable có tồn tại không
            if (dt == null)
            {
                MessageBox.Show("Không tìm thấy bảng 'HoaDonTable' trong HoaDonDataSet. Vui lòng kiểm tra thiết kế DataSet!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            try
            {
                DataRow row = dt.NewRow();
                row["MaHoaDon"] = selectedHoaDon.MaHoaDon ?? string.Empty;
                row["NgayLap"] = selectedHoaDon.NgayLap;
                row["MaKhachHang"] = selectedHoaDon.MaKhachHang ?? string.Empty;
                row["TenKhachHang"] = string.Empty; // Sẽ lấy từ form hoặc cơ sở dữ liệu sau
                row["MaSanPham"] = selectedChiTietHoaDon.MaSanPham ?? string.Empty;
                row["SoLuong"] = selectedChiTietHoaDon.SoLuong;
                row["DonGia"] = selectedChiTietHoaDon.DonGia;
                row["GiamGia"] = selectedChiTietHoaDon.GiamGia;
                row["ThanhTien"] = selectedChiTietHoaDon.ThanhTien;
                row["TongTien"] = selectedHoaDon.TongTien;
                row["VAT"] = selectedHoaDon.VAT;
                row["ThanhToan"] = selectedHoaDon.ThanhToan;
                dt.Rows.Add(row);

                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tạo dữ liệu báo cáo: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}