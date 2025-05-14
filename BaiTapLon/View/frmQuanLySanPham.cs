using BaiTapLon.Models;
using BaiTapLon.ViewModels;
using System;
using System.Windows.Forms;

namespace BaiTapLon
{
    public partial class frmQuanLySanPham : Form
    {
        private SanPhamVM sanPhamVM;

        public frmQuanLySanPham()
        {
            InitializeComponent();
            sanPhamVM = new SanPhamVM();
            SetupDataGridView();
            LoadSanPhamList();
        }

        private void SetupDataGridView()
        {
            dgvSanPham.AutoGenerateColumns = false;
            dgvSanPham.Columns.Clear();
            dgvSanPham.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MaSanPham", HeaderText = "Mã Sản Phẩm" });
            dgvSanPham.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenSanPham", HeaderText = "Tên Sản Phẩm" });
            dgvSanPham.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DonGia", HeaderText = "Đơn Giá" });
            dgvSanPham.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SoLuongTon", HeaderText = "Số Lượng Tồn" });
            dgvSanPham.DataSource = sanPhamVM.SanphamList;
        }

        private void LoadSanPhamList()
        {
            sanPhamVM.LayTatCaSanPham();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSanPham.Text) || string.IsNullOrWhiteSpace(txtTenSanPham.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ mã và tên sản phẩm.");
                return;
            }

            if (!decimal.TryParse(txtDonGia.Text, out decimal donGia) || !int.TryParse(txtSoLuongTon.Text, out int soLuongTon))
            {
                MessageBox.Show("Đơn giá và số lượng tồn phải là số hợp lệ.");
                return;
            }

            var sanPham = new SanPham
            {
                MaSanPham = txtMaSanPham.Text,
                TenSanPham = txtTenSanPham.Text,
                DonGia = donGia,
                SoLuongTon = soLuongTon
            };

            if (sanPhamVM.ThemSanPham(sanPham))
            {
                sanPhamVM.LayTatCaSanPham();
                ClearInputs();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSanPham.Text))
            {
                MessageBox.Show("Vui lòng nhập mã sản phẩm để sửa.");
                return;
            }

            if (!decimal.TryParse(txtDonGia.Text, out decimal donGia) || !int.TryParse(txtSoLuongTon.Text, out int soLuongTon))
            {
                MessageBox.Show("Đơn giá và số lượng tồn phải là số hợp lệ.");
                return;
            }

            sanPhamVM.SuaSanPham(txtMaSanPham.Text, txtTenSanPham.Text, donGia, soLuongTon);
            ClearInputs();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSanPham.Text))
            {
                MessageBox.Show("Vui lòng nhập mã sản phẩm để xóa.");
                return;
            }

            sanPhamVM.XoaSanPham(txtMaSanPham.Text);
            ClearInputs();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            var result = sanPhamVM.TimKiemSanPham(txtTimKiem.Text);
            dgvSanPham.DataSource = result;
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadSanPhamList();
            ClearInputs();
            txtTimKiem.Clear();
        }

        private void dgvSanPham_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSanPham.SelectedRows.Count > 0)
            {
                var selectedSanPham = (SanPham)dgvSanPham.SelectedRows[0].DataBoundItem;
                txtMaSanPham.Text = selectedSanPham.MaSanPham;
                txtTenSanPham.Text = selectedSanPham.TenSanPham;
                txtDonGia.Text = selectedSanPham.DonGia.ToString();
                txtSoLuongTon.Text = selectedSanPham.SoLuongTon.ToString();
            }
        }

        private void ClearInputs()
        {
            txtMaSanPham.Clear();
            txtTenSanPham.Clear();
            txtDonGia.Clear();
            txtSoLuongTon.Clear();
        }
    }
}