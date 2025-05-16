using BaiTapLon.Models;
using BaiTapLon.ViewModels;
using System;
using System.Windows.Forms;

namespace BaiTapLon.View
{
    public partial class frmQuanLyKhachHang : Form
    {
        private readonly KhachHangVM viewModel;

        public frmQuanLyKhachHang()
        {
            InitializeComponent();
            viewModel = new KhachHangVM();
            SetupDataGridView();
            LoadKhachHangData();
        }

        private void SetupDataGridView()
        {
            dgvKhachHang.AutoGenerateColumns = false;
            dgvKhachHang.DataSource = viewModel.KhachHangList;
            dgvKhachHang.Columns.Clear();
            dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MaKhachHang", HeaderText = "Mã Khách Hàng" });
            dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenKhachHang", HeaderText = "Tên Khách Hàng" });
            dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SoDienThoai", HeaderText = "Số Điện Thoại" });
            dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DiaChi", HeaderText = "Địa Chỉ" });
            dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Email", HeaderText = "Email" });
            dgvKhachHang.SelectionChanged += DgvKhachHang_SelectionChanged;
        }

        private void LoadKhachHangData()
        {
            viewModel.LayTatCaKhachHang();
        }


        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            string maKhachHang = txtMaKhachHang.Text.Trim();
            string tenKhachHang = txtTenKhachHang.Text.Trim();
            string soDienThoai = txtSoDienThoai.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            string email = txtEmail.Text.Trim();

            viewModel.SuaKhachHang(maKhachHang, tenKhachHang, soDienThoai, diaChi, email);
            ClearInputs();
        }


        private void DgvKhachHang_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvKhachHang.SelectedRows.Count > 0)
            {
                KhachHang selectedKhachHang = (KhachHang)dgvKhachHang.SelectedRows[0].DataBoundItem;
                txtMaKhachHang.Text = selectedKhachHang.MaKhachHang;
                txtTenKhachHang.Text = selectedKhachHang.TenKhachHang;
                txtSoDienThoai.Text = selectedKhachHang.SoDienThoai;
                txtDiaChi.Text = selectedKhachHang.DiaChi;
                txtEmail.Text = selectedKhachHang.Email;
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtMaKhachHang.Text) ||
                string.IsNullOrWhiteSpace(txtTenKhachHang.Text) ||
                string.IsNullOrWhiteSpace(txtSoDienThoai.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ mã khách hàng, tên và số điện thoại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private KhachHang CreateKhachHangFromInputs()
        {
            return new KhachHang
            {
                MaKhachHang = txtMaKhachHang.Text.Trim(),
                TenKhachHang = txtTenKhachHang.Text.Trim(),
                SoDienThoai = txtSoDienThoai.Text.Trim(),
                DiaChi = txtDiaChi.Text.Trim(),
                Email = txtEmail.Text.Trim()
            };
        }

        private void ClearInputs()
        {
            txtMaKhachHang.Clear();
            txtTenKhachHang.Clear();
            txtSoDienThoai.Clear();
            txtDiaChi.Clear();
            txtEmail.Clear();
        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            KhachHang kh = CreateKhachHangFromInputs();
            if (viewModel.ThemKhachHang(kh))
            {
                ClearInputs();
            }
        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            string maKhachHang = txtMaKhachHang.Text.Trim();
            string tenKhachHang = txtTenKhachHang.Text.Trim();
            string soDienThoai = txtSoDienThoai.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            string email = txtEmail.Text.Trim();

            viewModel.SuaKhachHang(maKhachHang, tenKhachHang, soDienThoai, diaChi, email);
            ClearInputs();
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            string maKhachHang = txtMaKhachHang.Text.Trim();
            if (string.IsNullOrEmpty(maKhachHang))
            {
                MessageBox.Show("Vui lòng nhập mã khách hàng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa khách hàng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                viewModel.XoaKhachHang(maKhachHang);
                ClearInputs();
            }
        }

        private void btnLamMoi_Click_1(object sender, EventArgs e)
        {
            ClearInputs();
            viewModel.LayTatCaKhachHang(); // Tải lại danh sách từ database
            dgvKhachHang.DataSource = null; // Xóa DataSource cũ
            dgvKhachHang.DataSource = viewModel.KhachHangList; // Cập nhật DataGridView
            dgvKhachHang.Refresh(); // Làm mới giao diện
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (this.MdiParent != null)
            {
                this.MdiParent.Activate(); 
            }
            this.Close();
        }

        private void dgvKhachHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnTim_Click_1(object sender, EventArgs e)
        {
            string keyword = txtMaKhachHang.Text.Trim();
            var result = viewModel.TimKiemKhachHang(keyword); // Tìm kiếm khách hàng
            dgvKhachHang.DataSource = null; // Xóa DataSource cũ để tránh xung đột
            dgvKhachHang.DataSource = result; // Gán lại DataSource để làm mới DataGridView
            dgvKhachHang.Refresh(); // Làm mới giao diện DataGridView
        }
    }
}