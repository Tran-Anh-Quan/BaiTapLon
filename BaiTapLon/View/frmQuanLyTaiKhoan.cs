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
    public partial class frmQuanLyTaiKhoan : Form
    {
        private TaiKhoanVM TKVM = new TaiKhoanVM();
        BindingSource bindingSource = new BindingSource();
        public frmQuanLyTaiKhoan()
        {
            InitializeComponent();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            TaiKhoan tk = new TaiKhoan
            {
                TenDangNhap = txtTenDangNhap.Text.Trim(),
                MatKhau = txtMatKhau.Text.Trim(),
                MaNhanVien = txtMaNhanVien.Text.Trim()
            };

            if (TKVM.ThemTaiKhoan(tk))
            {
                MessageBox.Show("Thêm tài khoản thành công!");
            }
            else
            {
                MessageBox.Show("Thêm tài khoản thất bại!");
            }

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();
            string maNhanVien = txtMaNhanVien.Text.Trim();

            if (string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(matKhau) || string.IsNullOrEmpty(maNhanVien))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin tài khoản.");
                return;
            }

            TKVM.SuaTaiKhoan(maNhanVien, tenDangNhap, matKhau);

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string maNhanVien = dataGridView1.SelectedRows[0].Cells["MaNhanVien"].Value.ToString();

                DialogResult result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa tài khoản có tên đăng nhập \"{maNhanVien}\" không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    TKVM.XoaTaiKhoan(maNhanVien);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần xóa.");
            }

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string maNhanVien = txtMaNhanVien.Text.Trim();
            if (string.IsNullOrEmpty(maNhanVien))
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên cần tìm kiếm.");
                return;
            }

            TaiKhoan tk = TKVM.LayTaiKhoanTheoMaNhanVien(maNhanVien);
            if (tk == null)
            {
                MessageBox.Show("Không tìm thấy tài khoản với mã nhân viên này.");
                return;
            }

            txtTenDangNhap.Text = tk.TenDangNhap;
            txtMatKhau.Text = tk.MatKhau;
            txtMaNhanVien.Text = tk.MaNhanVien;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["MaNhanVien"].Value != null &&
                    row.Cells["MaNhanVien"].Value.ToString().Equals(maNhanVien, StringComparison.OrdinalIgnoreCase))
                {
                    row.Selected = true;
                    dataGridView1.CurrentCell = row.Cells[0];
                    break;
                }
            }

        }
        private void HienThiTaiKhoan()
        {
            bindingSource.Clear();
            BindingList<TaiKhoan> taiKhoanList = TKVM.LayTatCaTaiKhoan();
            bindingSource.DataSource = taiKhoanList;
        }
        private void LamMoiThongTinTaiKhoan()
        {
            txtTenDangNhap.Clear();
            txtMatKhau.Clear();
            txtMaNhanVien.Clear();
            txtTenDangNhap.Focus();
        }

        private void frmQuanLyTaiKhoan_Load(object sender, EventArgs e)
        {
            HienThiTaiKhoan();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView1.DataSource = bindingSource;
            LamMoiThongTinTaiKhoan();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            HienThiTaiKhoan();
            LamMoiThongTinTaiKhoan();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                dataGridView1.Rows[e.RowIndex].Selected = true;

 
                var maNhanVienValue = dataGridView1.Rows[e.RowIndex].Cells["MaNhanVien"].Value;


                if (maNhanVienValue != null && !string.IsNullOrEmpty(maNhanVienValue.ToString()))
                {
                    string maNhanVien = maNhanVienValue.ToString();
                }
                else
                {
                    MessageBox.Show("Mã nhân viên không hợp lệ.");
                    return;
                }


                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                txtTenDangNhap.Text = row.Cells["TenDangNhap"].Value.ToString();
                txtMatKhau.Text = row.Cells["MatKhau"].Value.ToString();
                txtMaNhanVien.Text = row.Cells["MaNhanVien"].Value.ToString();
            }

        }
    }
}
