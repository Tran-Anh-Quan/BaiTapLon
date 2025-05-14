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
            LoadTaiKhoanData();
        }

        private void SetupDataGridView()
        {
            dgvTaiKhoan.AutoGenerateColumns = false;
            dgvTaiKhoan.DataSource = viewModel.List;
            dgvTaiKhoan.Columns.Clear();
            dgvTaiKhoan.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MaNhanVien",
                HeaderText = "Mã Nhân Viên"
            });
            dgvTaiKhoan.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TenDangNhap",
                HeaderText = "Tên Đăng Nhập"
            });
            dgvTaiKhoan.SelectionChanged += DgvTaiKhoan_SelectionChanged;
        }

        private void LoadTaiKhoanData()
        {
            viewModel.List.Clear();
            using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyBanHang;Integrated Security=True"))
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT * FROM TaiKhoan";
                    using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sql, conn))
                    {
                        using (System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                viewModel.List.Add(new TaiKhoan
                                {
                                    MaNhanVien = reader["MaNhanVien"].ToString(),
                                    TenDangNhap = reader["TenDangNhap"].ToString(),
                                    MatKhau = reader["MatKhau"].ToString()
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải danh sách tài khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maNhanVien = txtMaNhanVien.Text.Trim();
            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();

            if (string.IsNullOrWhiteSpace(maNhanVien) || string.IsNullOrWhiteSpace(tenDangNhap) || string.IsNullOrWhiteSpace(matKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin tài khoản.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            TaiKhoan tk = new TaiKhoan
            {
                MaNhanVien = maNhanVien,
                TenDangNhap = tenDangNhap,
                MatKhau = matKhau
            };

            if (viewModel.ThemTaiKhoan(tk))
            {
                MessageBox.Show("Thêm tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadTaiKhoanData();
                ClearInputs();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maNhanVien = txtMaNhanVien.Text.Trim();
            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();

            if (string.IsNullOrWhiteSpace(maNhanVien) || string.IsNullOrWhiteSpace(tenDangNhap) || string.IsNullOrWhiteSpace(matKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin tài khoản.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            viewModel.SuaTaiKhoan(maNhanVien, tenDangNhap, matKhau);
            LoadTaiKhoanData();
            ClearInputs();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maNhanVien = txtMaNhanVien.Text.Trim();

            if (string.IsNullOrWhiteSpace(maNhanVien))
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa tài khoản này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                viewModel.XoaTaiKhoan(maNhanVien);
                LoadTaiKhoanData();
                ClearInputs();
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string maNhanVien = txtMaNhanVien.Text.Trim();

            if (string.IsNullOrWhiteSpace(maNhanVien))
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên để tìm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            TaiKhoan tk = viewModel.LayTaiKhoanTheoMaNhanVien(maNhanVien);
            if (tk != null)
            {
                txtTenDangNhap.Text = tk.TenDangNhap;
                txtMatKhau.Text = tk.MatKhau; // Note: Password is hashed, consider showing a placeholder or handling differently
            }
            else
            {
                MessageBox.Show("Không tìm thấy tài khoản với mã nhân viên này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputs();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            frmDangNhap frm = new frmDangNhap();
            frm.Show();
        }

        private void DgvTaiKhoan_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTaiKhoan.SelectedRows.Count > 0)
            {
                TaiKhoan selectedTaiKhoan = (TaiKhoan)dgvTaiKhoan.SelectedRows[0].DataBoundItem;
                txtMaNhanVien.Text = selectedTaiKhoan.MaNhanVien;
                txtTenDangNhap.Text = selectedTaiKhoan.TenDangNhap;
                txtMatKhau.Text = selectedTaiKhoan.MatKhau; // Note: Password is hashed
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