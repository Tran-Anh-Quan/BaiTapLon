using BaiTapLon.Models;
using BaiTapLon.ViewModels;
using System;
using System.Linq;
using System.Windows.Forms;

namespace BaiTapLon.View
{
    public partial class frmHoaDon : Form
    {
        private readonly HoaDonVM viewModel;
        private readonly ChiTietHoaDonVM viewModelCT;
        private readonly HoaDonChiTietVM viewModelHDCT;

        public frmHoaDon()
        {
            InitializeComponent();
            viewModelCT = new ChiTietHoaDonVM();
            viewModel = new HoaDonVM();
            viewModelHDCT = new HoaDonChiTietVM();
            SetupDataGridView();
            LoadHoaDonData();
            SetupEventHandlers();
        }

        private void SetupDataGridView()
        {
            dgvHoaDon.AutoGenerateColumns = false;
            dgvHoaDon.DataSource = viewModelHDCT.LayTatCaHoaDonGopChiTiet();

            dgvHoaDon.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MaHoaDon", HeaderText = "Mã Hóa Đơn" });
            dgvHoaDon.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MaKhachHang", HeaderText = "Mã KH" });
            dgvHoaDon.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NgayLap", HeaderText = "Ngày Lập", DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" } });
            dgvHoaDon.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MaSanPham", HeaderText = "Mã SP" });
            dgvHoaDon.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SoLuong", HeaderText = "Số Lượng" });
            dgvHoaDon.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DonGia", HeaderText = "Đơn Giá", DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" } });
            dgvHoaDon.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "GiamGia", HeaderText = "Giảm Giá (%)" });
            dgvHoaDon.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TongTien", HeaderText = "Tổng Tiền", DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" } });
            dgvHoaDon.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "VAT", HeaderText = "VAT", DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" } });
            dgvHoaDon.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ThanhToan", HeaderText = "Thanh Toán", DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" } });
        }
        

        private void LoadHoaDonData()
        {
            viewModel.LayTatCaHoaDon();
            viewModelCT.LayTatCaHoaDonCT();

        }

        private void SetupEventHandlers()
        {
            txtSoLuong.TextChanged += CalculateSummary;
            txtDonGia.TextChanged += CalculateSummary;
            txtGiamGia.TextChanged += CalculateSummary;
            dgvHoaDon.SelectionChanged += DgvHoaDon_SelectionChanged;
        }

        private void CalculateSummary(object sender, EventArgs e)
        {
            try
            {
                int soLuong = string.IsNullOrEmpty(txtSoLuong.Text) ? 0 : int.Parse(txtSoLuong.Text);
                decimal donGia = string.IsNullOrEmpty(txtDonGia.Text) ? 0 : decimal.Parse(txtDonGia.Text);
                int giamGia = string.IsNullOrEmpty(txtGiamGia.Text) ? 0 : int.Parse(txtGiamGia.Text);

                decimal tongTien = soLuong * donGia * (1 - giamGia / 100m);
                decimal vat = Math.Round(tongTien * 0.1m, 2);
                decimal thanhToan = tongTien + vat;

                txtTongTien.Text = tongTien.ToString("N2");
                txtVAT.Text = vat.ToString("N2");
                txtThanhToan.Text = thanhToan.ToString("N2");
            }
            catch
            {
                txtTongTien.Text = "0.00";
                txtVAT.Text = "0.00";
                txtThanhToan.Text = "0.00";
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            string maHoaDon = txtMaHoaDon.Text.Trim();

            // Kiểm tra xem mã hóa đơn đã tồn tại trong danh sách ViewModel
            var hoaDonDaCo = viewModel.HoaDonList.FirstOrDefault(h => h.MaHoaDon == maHoaDon);

            // Nếu hóa đơn chưa tồn tại, tạo mới và thêm vào DB
            if (hoaDonDaCo == null)
            {
                HoaDon hoaDon = CreateHoaDonFromInputs();
                if (!viewModel.ThemHoaDon(hoaDon))
                {
                    return; // Nếu thêm hóa đơn thất bại thì dừng lại
                }
            }

            // Tạo và thêm chi tiết hóa đơn vào DB
            ChiTietHoaDon chiTiet = CreateChiTietHoaDonFromInputs();
            if (viewModel.ThemHoaDonCT(chiTiet))
            {
                MessageBox.Show("Đã thêm sản phẩm vào hóa đơn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputs();
            }
        }


        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            HoaDon hoaDon = CreateHoaDonFromInputs();
            ChiTietHoaDon chiTietHoaDon = CreateChiTietHoaDonFromInputs();
            viewModel.SuaHoaDonCT(chiTietHoaDon);
            viewModel.SuaHoaDon(hoaDon);
            ClearInputs();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maHoaDon = txtMaHoaDon.Text.Trim();
            if (string.IsNullOrEmpty(maHoaDon))
            {
                MessageBox.Show("Vui lòng nhập mã hóa đơn để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa hóa đơn này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                viewModel.XoaHoaDon(maHoaDon);
                ClearInputs();
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string keyword = txtMaHoaDon.Text.Trim();
            var result = viewModel.TimKiemHoaDon(keyword); // Tìm kiếm hóa đơn
            dgvHoaDon.DataSource = null; // Xóa DataSource cũ để tránh xung đột
            dgvHoaDon.DataSource = result; // Gán lại DataSource để làm mới DataGridView
            dgvHoaDon.Refresh(); // Làm mới giao diện DataGridView
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearInputs();
            viewModelHDCT.LayTatCaHoaDonGopChiTiet(); // Tải lại danh sách từ database
            dgvHoaDon.DataSource = null; // Xóa DataSource cũ
            dgvHoaDon.DataSource = viewModelHDCT.LayTatCaHoaDonGopChiTiet(); // Cập nhật DataGridView
            dgvHoaDon.Refresh(); // Làm mới giao diện
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            frmChiTietHoaDon frm = new frmChiTietHoaDon();
            frm.MdiParent = this.MdiParent; 
            frm.Show();
        }

        private void DgvHoaDon_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvHoaDon.SelectedRows.Count > 0)
            {
                var selectedItem = dgvHoaDon.SelectedRows[0].DataBoundItem as HoaDonChiTietDTO;

                if (selectedItem != null)
                {
                    txtMaHoaDon.Text = selectedItem.MaHoaDon;
                    txtMaKhachHang.Text = selectedItem.MaKhachHang;
                    txtMaSanPham.Text = selectedItem.MaSanPham;
                    txtSoLuong.Text = selectedItem.SoLuong.ToString();
                    txtDonGia.Text = selectedItem.DonGia.ToString("N2");
                    txtGiamGia.Text = selectedItem.GiamGia.ToString();
                    dtpNgayLap.Value = selectedItem.NgayLap;
                    txtTongTien.Text = selectedItem.TongTien.ToString("N2");
                    txtVAT.Text = selectedItem.VAT.ToString("N2");
                    txtThanhToan.Text = selectedItem.ThanhToan.ToString("N2");
                }
            }
        }



        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtMaHoaDon.Text) ||
                string.IsNullOrWhiteSpace(txtMaKhachHang.Text) ||
                string.IsNullOrWhiteSpace(txtMaSanPham.Text) ||
                string.IsNullOrWhiteSpace(txtSoLuong.Text) ||
                string.IsNullOrWhiteSpace(txtDonGia.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin hóa đơn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private HoaDon CreateHoaDonFromInputs()
        {
            return new HoaDon
            {
                MaHoaDon = txtMaHoaDon.Text.Trim(),
                MaKhachHang = txtMaKhachHang.Text.Trim(),
                NgayLap = dtpNgayLap.Value,
                TongTien = decimal.Parse(txtTongTien.Text),
                VAT = decimal.Parse(txtVAT.Text),
                ThanhToan = decimal.Parse(txtThanhToan.Text)
            };
        }
        private ChiTietHoaDon CreateChiTietHoaDonFromInputs()
        {
            return new ChiTietHoaDon
            {
                MaHoaDon = txtMaHoaDon.Text.Trim(),
                MaSanPham = txtMaSanPham.Text.Trim(),
                SoLuong = int.Parse(txtSoLuong.Text),
                DonGia = decimal.Parse(txtDonGia.Text),
                GiamGia = string.IsNullOrEmpty(txtGiamGia.Text) ? 0 : int.Parse(txtGiamGia.Text)
            };
        }

        private void ClearInputs()
        {
            txtMaHoaDon.Clear();
            txtMaKhachHang.Clear();
            txtMaSanPham.Clear();
            txtSoLuong.Clear();
            txtDonGia.Clear();
            txtGiamGia.Clear();
            dtpNgayLap.Value = DateTime.Now;
            txtTongTien.Text = "0.00";
            txtVAT.Text = "0.00";
            txtThanhToan.Text = "0.00";
        }


    }
}