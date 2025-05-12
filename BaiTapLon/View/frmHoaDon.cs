using BaiTapLon.Models;
using BaiTapLon.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiTapLon.View
{
    public partial class frmHoaDon : Form
    {
        HoaDonVM HDVM = new HoaDonVM();
        BindingSource bindingSource = new BindingSource();
        public frmHoaDon()
        {
            InitializeComponent();
        }
        private void HienThiHoaDon()
        {
            bindingSource.Clear();
            BindingList<HoaDon> KhachHangList = HDVM.LayTatCaHoaDon();
            bindingSource.DataSource = KhachHangList;
        }

        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView1.DataSource = bindingSource;
            HienThiHoaDon();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string maHoaDon = txtMaHoaDon.Text.Trim();
            if (string.IsNullOrEmpty(maHoaDon))
            {
                MessageBox.Show("Vui lòng nhập mã hóa đơn cần tìm kiếm.");
                return;
            }

            HoaDon hd = HDVM.LayHoaDonTheoMa(maHoaDon);
            if (hd == null)
            {
                MessageBox.Show("Không tìm thấy hóa đơn.");
                return;
            }

            txtMaKhachHang.Text = hd.MaKhachHang;
            txtMaSanPham.Text = hd.MaSanPham;
            txtSoLuong.Text = hd.SoLuong.ToString();
            txtDonGia.Text = hd.DonGia.ToString("N0");
            dtpNgayLap.Value = hd.NgayLap;
            txtGiamGia.Text = hd.GiamGia.ToString();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["MaHoaDon"].Value != null &&
                    row.Cells["MaHoaDon"].Value.ToString().Equals(maHoaDon, StringComparison.OrdinalIgnoreCase))
                {
                    row.Selected = true;
                    dataGridView1.CurrentCell = row.Cells[0];
                    break;
                }
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                row.Selected = true;

                var maHoaDon = row.Cells["MaHoaDon"].Value?.ToString();
                if (string.IsNullOrWhiteSpace(maHoaDon))
                {
                    MessageBox.Show("Mã hóa đơn không hợp lệ.");
                    return;
                }
                txtMaHoaDon.Text = maHoaDon;
                txtMaKhachHang.Text = row.Cells["MaKhachHang"].Value?.ToString();
                txtMaSanPham.Text = row.Cells["MaSanPham"].Value?.ToString();

                txtSoLuong.Text = row.Cells["SoLuong"].Value?.ToString();
                txtDonGia.Text = row.Cells["DonGia"].Value?.ToString();
                txtGiamGia.Text = row.Cells["GiamGia"].Value?.ToString();

                if (DateTime.TryParse(row.Cells["NgayLap"].Value?.ToString(), out DateTime ngayLap))
                    dtpNgayLap.Value = ngayLap;

                //txtTongTien.Text = row.Cells["TongTien"].Value?.ToString();
                //txtVAT.Text = row.Cells["VAT"].Value?.ToString();
                //txtThanhToan.Text = row.Cells["ThanhToan"].Value?.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HoaDon hd = new HoaDon
            {
                MaHoaDon = txtMaHoaDon.Text.Trim(),
                MaKhachHang = txtMaKhachHang.Text.Trim(),
                MaSanPham = txtMaSanPham.Text.Trim(),
                NgayLap = dtpNgayLap.Value
            };


            if (string.IsNullOrWhiteSpace(hd.MaHoaDon))
            {
                MessageBox.Show("Mã hóa đơn không được để trống.");
                return;
            }

   
            if (!int.TryParse(txtSoLuong.Text.Trim(), out int soLuong))
            {
                MessageBox.Show("Số lượng không hợp lệ.");
                return;
            }
            hd.SoLuong = soLuong;

           
            if (!decimal.TryParse(txtDonGia.Text.Trim(), out decimal donGia))
            {
                MessageBox.Show("Đơn giá không hợp lệ.");
                return;
            }
            hd.DonGia = donGia;

           
            if (!int.TryParse(txtGiamGia.Text.Trim(), out int giamGia))
            {
                MessageBox.Show("Giảm giá không hợp lệ.");
                return;
            }
            hd.GiamGia = giamGia;

            
            hd.TongTien = soLuong * donGia * (1 - giamGia / 100m);
            hd.VAT = Math.Round(hd.TongTien * 0.1m, 2);
            hd.ThanhToan = hd.TongTien + hd.VAT;

         
            if (HDVM.ThemHoaDon(hd))
            {
                MessageBox.Show("Thêm hóa đơn thành công!");
            }
            else
            {
                MessageBox.Show("Thêm thất bại!");
            }
        }
    }
}
