using BaiTapLon.Models;
using BaiTapLon.ViewModels;
using System;
using System.Windows.Forms;

namespace BaiTapLon.View
{
    public partial class frmQuanLyNhanVien : Form
    {
        private readonly NhanVienVM viewModel;

        public frmQuanLyNhanVien()
        {
            InitializeComponent();
            viewModel = new NhanVienVM();
            SetupDataGridView();
            SetupComboBox();
            LoadNhanVienData();
        }

        private void SetupDataGridView()
        {
            dgvNhanVien.AutoGenerateColumns = false;
            dgvNhanVien.DataSource = viewModel.NhanVienList;
            dgvNhanVien.Columns.Clear();
            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MaNhanVien", HeaderText = "Mã Nhân Viên" });
            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenNhanVien", HeaderText = "Tên Nhân Viên" });
            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SoDienThoai", HeaderText = "Số Điện Thoại" });
            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "GioiTinh", HeaderText = "Giới Tính" });
            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ChucVu", HeaderText = "Chức Vụ" });
            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NgayVaoLam", HeaderText = "Ngày Vào Làm", DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" } });
            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Luong", HeaderText = "Lương", DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" } });
            dgvNhanVien.SelectionChanged += DgvNhanVien_SelectionChanged;
        }

        private void SetupComboBox()
        {
            cboGioiTinh.Items.Add("Nam");
            cboGioiTinh.Items.Add("Nữ");
            cboGioiTinh.SelectedIndex = 0;

            cboChucVu.Items.Add("Quản lý");
            cboChucVu.Items.Add("Nhân viên bán hàng");
            cboChucVu.Items.Add("Kế toán");
            cboChucVu.SelectedIndex = 0;
        }

        private void LoadNhanVienData()
        {
            viewModel.LayTatCaNhanVien();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            NhanVien nv = CreateNhanVienFromInputs();
            if (viewModel.ThemNhanVien(nv))
            {
                ClearInputs();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            string maNhanVien = txtMaNhanVien.Text.Trim();
            string tenNhanVien = txtTenNhanVien.Text.Trim();
            string soDienThoai = txtSoDienThoai.Text.Trim();
            string chucVu = cboChucVu.SelectedItem.ToString();
            DateTime ngayVaoLam = dtpNgayVaoLam.Value;
            decimal luong = decimal.Parse(txtLuong.Text);
            string gioiTinh = cboGioiTinh.SelectedItem.ToString();

            viewModel.SuaNhanVien(maNhanVien, tenNhanVien, soDienThoai, chucVu, ngayVaoLam, luong, gioiTinh);
            ClearInputs();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maNhanVien = txtMaNhanVien.Text.Trim();
            if (string.IsNullOrEmpty(maNhanVien))
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa nhân viên này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                viewModel.XoaNhanVien(maNhanVien);
                ClearInputs();
            }
        }

        private void btnTim_Click(object peacocksender, EventArgs e)
        {
            string keyword = txtMaNhanVien.Text.Trim();
            var result = viewModel.TimKiemNhanVien(keyword);
            dgvNhanVien.DataSource = null;
            dgvNhanVien.DataSource = result;
            dgvNhanVien.Refresh();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearInputs();
            viewModel.LayTatCaNhanVien();
            dgvNhanVien.DataSource = null;
            dgvNhanVien.DataSource = viewModel.NhanVienList;
            dgvNhanVien.Refresh();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DgvNhanVien_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvNhanVien.SelectedRows.Count > 0)
            {
                NhanVien selectedNhanVien = (NhanVien)dgvNhanVien.SelectedRows[0].DataBoundItem;
                txtMaNhanVien.Text = selectedNhanVien.MaNhanVien;
                txtTenNhanVien.Text = selectedNhanVien.TenNhanVien;
                txtSoDienThoai.Text = selectedNhanVien.SoDienThoai;
                cboGioiTinh.SelectedItem = selectedNhanVien.GioiTinh;
                cboChucVu.SelectedItem = selectedNhanVien.ChucVu;
                dtpNgayVaoLam.Value = selectedNhanVien.NgayVaoLam;
                txtLuong.Text = selectedNhanVien.Luong.ToString("N0");
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtMaNhanVien.Text) ||
                string.IsNullOrWhiteSpace(txtTenNhanVien.Text) ||
                string.IsNullOrWhiteSpace(txtSoDienThoai.Text) ||
                cboChucVu.SelectedItem == null ||
                string.IsNullOrWhiteSpace(txtLuong.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin nhân viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!decimal.TryParse(txtLuong.Text, out decimal luong) || luong < 0)
            {
                MessageBox.Show("Lương phải là số dương.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private NhanVien CreateNhanVienFromInputs()
        {
            return new NhanVien
            {
                MaNhanVien = txtMaNhanVien.Text.Trim(),
                TenNhanVien = txtTenNhanVien.Text.Trim(),
                SoDienThoai = txtSoDienThoai.Text.Trim(),
                GioiTinh = cboGioiTinh.SelectedItem.ToString(),
                ChucVu = cboChucVu.SelectedItem.ToString(),
                NgayVaoLam = dtpNgayVaoLam.Value,
                Luong = decimal.Parse(txtLuong.Text)
            };
        }

        private void ClearInputs()
        {
            txtMaNhanVien.Clear();
            txtTenNhanVien.Clear();
            txtSoDienThoai.Clear();
            cboGioiTinh.SelectedIndex = 0;
            cboChucVu.SelectedIndex = 0;
            dtpNgayVaoLam.Value = DateTime.Now;
            txtLuong.Clear();
        }
    }
}