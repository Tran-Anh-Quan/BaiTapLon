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
    public partial class frmQuanLyKhachHang : Form
    {
        KhachHangVM KHVM = new KhachHangVM();
        BindingSource bindingSource = new BindingSource();
        public frmQuanLyKhachHang()
        {
            InitializeComponent();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            KhachHang kh = new KhachHang
            {
                MaKhachHang = txtMaKhachHang.Text.Trim(),
                TenKhachHang = txtTenKhachHang.Text.Trim(),
                SoDienThoai = txtSoDienThoai.Text.Trim(),
                DiaChi = txtDiaChi.Text.Trim(),
                Email = txtEmail.Text.Trim()
            };
            if (string.IsNullOrWhiteSpace(kh.MaKhachHang))
            {
                MessageBox.Show("Mã khách hàng không được để trống.");
                return;
            }

            if (KHVM.ThemKhachHang(kh))
            {
                MessageBox.Show("Thêm khách hàng thành công!");
            }
            else
            {
                MessageBox.Show("Thêm thất bại!");
            }

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maKhachHang = txtMaKhachHang.Text.Trim();
            string tenKhachHang = txtTenKhachHang.Text.Trim();
            string soDienThoai = txtSoDienThoai.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            string email = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(maKhachHang) || string.IsNullOrEmpty(tenKhachHang) || string.IsNullOrEmpty(soDienThoai) ||
                string.IsNullOrEmpty(diaChi) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }
            KHVM.SuaKhachHang(maKhachHang, tenKhachHang, soDienThoai, diaChi, email);

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string maKhachHang = dataGridView1.SelectedRows[0].Cells["MaKhachHang"].Value.ToString();
                KHVM.XoaKhachHang(maKhachHang);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa.");
            }

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string maKhachHang = txtMaKhachHang.Text.Trim();
            if (string.IsNullOrEmpty(maKhachHang))
            {
                MessageBox.Show("Vui lòng nhập mã khách hàng cần tìm kiếm.");
                return;
            }

            KhachHang kh = KHVM.LayKhachHangTheoMa(maKhachHang);
            if (kh == null)
            {
                MessageBox.Show("Không tìm thấy khách hàng.");
                return;
            }

            txtTenKhachHang.Text = kh.TenKhachHang;
            txtSoDienThoai.Text = kh.SoDienThoai;
            txtDiaChi.Text = kh.DiaChi;
            txtEmail.Text = kh.Email;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["MaKhachHang"].Value != null &&
                    row.Cells["MaKhachHang"].Value.ToString().Equals(maKhachHang, StringComparison.OrdinalIgnoreCase))
                {
                    row.Selected = true;
                    dataGridView1.CurrentCell = row.Cells[0]; 
                    break;
                }
            }

        }
        private void LamMoiThongTinKhachHang()
        {
            txtMaKhachHang.Clear();
            txtTenKhachHang.Clear();
            txtSoDienThoai.Clear();
            txtDiaChi.Clear();
            txtEmail.Clear();
            txtMaKhachHang.Focus();
        }
        private void HienThiKhachHang()
        {
            bindingSource.Clear();
            BindingList<KhachHang> KhachHangList = KHVM.LayTatCaKhachHang();
            bindingSource.DataSource = KhachHangList;
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            HienThiKhachHang();
            LamMoiThongTinKhachHang();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView1.Rows[e.RowIndex].Selected = true;

                var maKhachHangValue = dataGridView1.Rows[e.RowIndex].Cells["MaKhachHang"].Value;

                if (maKhachHangValue != null && !string.IsNullOrEmpty(maKhachHangValue.ToString()))
                {
                    string maKhachHang = maKhachHangValue.ToString();
                }
                else
                {
                    MessageBox.Show("Mã khách hàng không hợp lệ.");
                }

                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                txtMaKhachHang.Text = row.Cells["MaKhachHang"].Value.ToString();
                txtTenKhachHang.Text = row.Cells["TenKhachHang"].Value.ToString();
                txtSoDienThoai.Text = row.Cells["SoDienThoai"].Value.ToString();
                txtDiaChi.Text = row.Cells["DiaChi"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
            }

        }

        private void frmQuanLyKhachHang_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView1.DataSource = bindingSource;
            HienThiKhachHang();
        }
    }
}
