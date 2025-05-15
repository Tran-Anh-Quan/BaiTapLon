using BaiTapLon.Models;
using BaiTapLon.ViewModels;
using System;
using System.Windows.Forms;

namespace BaiTapLon.View
{
    public partial class frmQuanLyTaiKhoan : Form
    {
        private readonly TaiKhoanVM viewModel;

        public frmQuanLyTaiKhoan()
        {
            InitializeComponent();
            viewModel = new TaiKhoanVM();
            SetupDataGridView();
            viewModel.LoadTaiKhoanData(); // Tải danh sách tài khoản khi mở form
        }

        private void SetupDataGridView()
        {
            dgvTaiKhoan.AutoGenerateColumns = false;
            dgvTaiKhoan.DataSource = viewModel.List;
            dgvTaiKhoan.Columns.Clear();
            dgvTaiKhoan.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MaNhanVien", HeaderText = "Mã Nhân Viên" });
            dgvTaiKhoan.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenDangNhap", HeaderText = "Tên Đăng Nhập" });
            dgvTaiKhoan.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MatKhau", HeaderText = "Mật Khẩu" });
            dgvTaiKhoan.SelectionChanged += DgvTaiKhoan_SelectionChanged;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maNhanVien = txtMaNhanVien.Text.Trim();
            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();

            if (string.IsNullOrEmpty(maNhanVien) || string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var taiKhoan = new TaiKhoan { MaNhanVien = maNhanVien, TenDangNhap = tenDangNhap, MatKhau = matKhau };
            if (viewModel.ThemTaiKhoan(taiKhoan))
            {
                viewModel.LoadTaiKhoanData(); // Tải lại danh sách sau khi thêm
                ClearInputs();
                MessageBox.Show("Thêm tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maNhanVien = txtMaNhanVien.Text.Trim();
            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();

            if (string.IsNullOrEmpty(maNhanVien) || string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            viewModel.SuaTaiKhoan(maNhanVien, tenDangNhap, matKhau);
            viewModel.LoadTaiKhoanData(); // Tải lại danh sách sau khi sửa
            ClearInputs();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maNhanVien = txtMaNhanVien.Text.Trim();

            if (string.IsNullOrEmpty(maNhanVien))
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa tài khoản này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                viewModel.XoaTaiKhoan(maNhanVien);
                viewModel.LoadTaiKhoanData(); // Tải lại danh sách sau khi xóa
                ClearInputs();
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string maNhanVien = txtMaNhanVien.Text.Trim();
            viewModel.List.Clear(); // Xóa danh sách hiện tại trong DataGridView
            viewModel.List = viewModel.TimKiemTaiKhoan(maNhanVien); // Cập nhật danh sách với kết quả tìm kiếm
            dgvTaiKhoan.DataSource = viewModel.List; // Gán lại nguồn dữ liệu cho DataGridView

            if (viewModel.List.Count > 0)
            {
                txtTenDangNhap.Text = viewModel.List[0].TenDangNhap;
                txtMatKhau.Text = viewModel.List[0].MatKhau;
            }
            else
            {
                ClearInputs();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            var frmDangNhap = new frmDangNhap();
            frmDangNhap.Show();
        }

        private void DgvTaiKhoan_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTaiKhoan.SelectedRows.Count > 0)
            {
                var selectedTaiKhoan = (TaiKhoan)dgvTaiKhoan.SelectedRows[0].DataBoundItem;
                txtMaNhanVien.Text = selectedTaiKhoan.MaNhanVien;
                txtTenDangNhap.Text = selectedTaiKhoan.TenDangNhap;
                txtMatKhau.Text = selectedTaiKhoan.MatKhau;
            }
        }

        private void ClearInputs()
        {
            txtMaNhanVien.Clear();
            txtTenDangNhap.Clear();
            txtMatKhau.Clear();
        }
    }
}