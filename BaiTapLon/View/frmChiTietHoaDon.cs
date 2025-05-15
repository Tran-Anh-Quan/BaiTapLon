using BaiTapLon.Models;
using BaiTapLon.ViewModels;
using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;

namespace BaiTapLon.View
{
    public partial class frmChiTietHoaDon : Form
    {
        private readonly ChiTietHoaDonVM viewModelCT;
        private readonly HoaDonVM viewModel;
        private readonly HoaDonChiTietVM viewModelHDCT;

        public frmChiTietHoaDon()
        {
            InitializeComponent();
            viewModel = new HoaDonVM();
            viewModelCT = new ChiTietHoaDonVM();
            viewModelHDCT = new HoaDonChiTietVM();

            SetupDataBindings();
            LoadHoaDonData();
        }

        private void SetupDataBindings()
        {
            // Bind DataGridView to HoaDonList
            var danhSach = viewModelHDCT.LayTatCaHoaDonGopChiTiet(); // có đúng là List<HoaDonChiTietDTO> không?
            dgvHoaDon.DataSource = danhSach;

            // Define DataGridView columns
            dgvHoaDon.Columns.Clear();
            dgvHoaDon.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MaHoaDon",
                HeaderText = "Mã Hóa Đơn"
            });
            dgvHoaDon.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MaKhachHang",
                HeaderText = "Mã Khách Hàng"
            });
            dgvHoaDon.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MaSanPham",
                HeaderText = "Mã Sản Phẩm"
            });
            dgvHoaDon.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "SoLuong",
                HeaderText = "Số Lượng"
            });
            dgvHoaDon.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DonGia",
                HeaderText = "Đơn Giá",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" }
            });
            dgvHoaDon.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "GiamGia",
                HeaderText = "Giảm Giá"
            });
            dgvHoaDon.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TongTien",
                HeaderText = "Tổng Tiền",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" }
            });
            dgvHoaDon.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "VAT",
                HeaderText = "VAT",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" }
            });
            dgvHoaDon.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ThanhToan",
                HeaderText = "Thanh Toán",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" }
            });



            // Event handlers for buttons
            btnKiemTraKH.Click += BtnKiemTraKH_Click;
            btnThanhToan.Click += BtnThanhToan_Click;
            btnXuatBaoCao.Click += BtnXuatBaoCao_Click;

            // Handle DataGridView selection to populate customer info
            dgvHoaDon.SelectionChanged += DgvHoaDon_SelectionChanged;
        }

        private void LoadHoaDonData()
        {
            viewModel.LayTatCaHoaDon();
            viewModelCT.LayTatCaHoaDonCT();
        }

        private void BtnKiemTraKH_Click(object sender, EventArgs e)
        {
            string maKhachHang = txtMaHD.Text.Trim();
            if (string.IsNullOrEmpty(maKhachHang))
            {
                MessageBox.Show("Vui lòng nhập mã hóa đơn để kiểm tra khách hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Assuming a method to get customer info based on MaKhachHang from the invoice
            var selectedHoaDon = viewModel.HoaDonList.FirstOrDefault(hd => hd.MaHoaDon == maKhachHang);
            if (selectedHoaDon != null)
            {
                // Mock customer info (replace with actual database query if available)
                txtTenKH.Text = $"Khách Hàng {selectedHoaDon.MaKhachHang}";
                txtDienThoai.Text = "0123456789"; // Replace with actual data
                txtDiaChi.Text = "123 Đường ABC, Quận 1, TP.HCM"; // Replace with actual data
                dtpNgayLap.Value = selectedHoaDon.NgayLap;
            }
            else
            {
                MessageBox.Show("Không tìm thấy hóa đơn với mã này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnThanhToan_Click(object sender, EventArgs e)
        {
            string maHoaDon = txtMaHD.Text.Trim();
            if (string.IsNullOrEmpty(maHoaDon))
            {
                MessageBox.Show("Vui lòng nhập mã hóa đơn để thanh toán.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            viewModelCT.ThanhToanHoaDon(maHoaDon);
            // Update ThanhToanValue label
            var selectedHoaDon = viewModel.HoaDonList.FirstOrDefault(hd => hd.MaHoaDon == maHoaDon);
            if (selectedHoaDon != null)
            {
                lblThanhToanValue.Text = selectedHoaDon.ThanhToan.ToString("N2");
            }
        }

        private void BtnXuatBaoCao_Click(object sender, EventArgs e)
        {

        }

        private void DgvHoaDon_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvHoaDon.SelectedRows.Count > 0)
            {
                var selectedHoaDon = (HoaDonChiTietDTO)dgvHoaDon.SelectedRows[0].DataBoundItem;
                txtMaHD.Text = selectedHoaDon.MaHoaDon;
                dtpNgayLap.Value = selectedHoaDon.NgayLap;
                // Mock customer info (replace with actual database query)
                txtTenKH.Text = $"Khách Hàng {selectedHoaDon.MaKhachHang}";
                txtDienThoai.Text = "0123456789"; // Replace with actual data
                txtDiaChi.Text = "123 Đường ABC, Quận 1, TP.HCM"; // Replace with actual data
                lblThanhToanValue.Text = selectedHoaDon.ThanhToan.ToString("N2");
            }
        }

        private void btnXuatBaoCao_Click_1(object sender, EventArgs e)
        {
            frmXuatHoaDon xuatHoaDon = new frmXuatHoaDon();
            xuatHoaDon.Show();
        }


    }
}