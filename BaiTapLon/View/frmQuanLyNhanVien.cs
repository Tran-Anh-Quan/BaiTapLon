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
    public partial class frmQuanLyNhanVien : Form
    {
        private NhanVienVM NVVM = new NhanVienVM();
        BindingSource bindingSource = new BindingSource();

        public frmQuanLyNhanVien()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            string maNhanVien = txtMaNhanVien.Text.Trim();
            if (string.IsNullOrEmpty(maNhanVien))
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên cần tìm kiếm.");
                return;
            }
            NhanVien nv = NVVM.LayNhanVienTheoMa(maNhanVien);
            if (nv == null)
            {
                MessageBox.Show("Không tìm thấy nhân viên.");
                return;
            }
            txtTenNhanVien.Text = nv.TenNhanVien;
            txtSoDienThoai.Text = nv.SoDienThoai;
            cboGioi.Text = nv.GioiTinh;
            cboChucVu.Text = nv.ChucVu;
            dtpNgayVaoLam.Value = nv.NgayVaoLam;
            txtLuong.Text = nv.Luong.ToString();
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

        private void HienThiNhanVien()
        {
            bindingSource.Clear();
            BindingList<NhanVien> NhanVienList = NVVM.LayTatCaNhanVien();
            bindingSource.DataSource = NhanVienList;
        }
        private void LamMoiThongTin()    
        {
            txtMaNhanVien.Clear();
            txtTenNhanVien.Clear();
            txtSoDienThoai.Clear();
            txtLuong.Clear();
            cboGioi.SelectedIndex = -1;
            cboChucVu.SelectedIndex = -1;
            dtpNgayVaoLam.Value = DateTime.Today;
            txtMaNhanVien.Focus();
        }

        private void frmQuanLyNhanVien_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView1.DataSource = bindingSource;
            HienThiNhanVien();
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
                }
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                txtMaNhanVien.Text = row.Cells["MaNhanVien"].Value.ToString();
                txtTenNhanVien.Text = row.Cells["TenNhanVien"].Value.ToString();
                txtSoDienThoai.Text = row.Cells["SoDienThoai"].Value.ToString();
                cboGioi.Text = row.Cells["GioiTinh"].Value.ToString();
                dtpNgayVaoLam.Value = Convert.ToDateTime(row.Cells["NgayVaoLam"].Value);
                cboChucVu.Text = row.Cells["ChucVu"].Value.ToString();
                txtLuong.Text = row.Cells["Luong"].Value.ToString();
            }
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NhanVien nv = new NhanVien
            {
                MaNhanVien = txtMaNhanVien.Text.Trim(),
                TenNhanVien = txtTenNhanVien.Text.Trim(),
                SoDienThoai = txtSoDienThoai.Text.Trim(),
                GioiTinh = cboGioi.Text,
                ChucVu = cboChucVu.Text,
                NgayVaoLam = dtpNgayVaoLam.Value,
                Luong = decimal.Parse(txtLuong.Text.Trim())
            };
            if (NVVM.ThemNhanVien(nv))
            {
                MessageBox.Show("Thêm nhân viên thành công!");
            }
            else
            {
                MessageBox.Show("Thêm thất bại!");
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            HienThiNhanVien();
            LamMoiThongTin();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string maNhanVien = dataGridView1.SelectedRows[0].Cells["MaNhanVien"].Value.ToString();
                NVVM.XoaNhanVien(maNhanVien);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa.");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maNhanVien = txtMaNhanVien.Text.Trim();
            string tenNhanVien = txtTenNhanVien.Text.Trim();
            string soDienThoai = txtSoDienThoai.Text.Trim();
            string gioiTinh = cboGioi.Text.Trim();
            string chucVu = cboChucVu.Text.Trim();
            DateTime ngayVaoLam = dtpNgayVaoLam.Value;
            decimal luong;

            if (string.IsNullOrEmpty(maNhanVien) || string.IsNullOrEmpty(tenNhanVien) || string.IsNullOrEmpty(soDienThoai) ||
    string.IsNullOrEmpty(chucVu) || string.IsNullOrEmpty(txtLuong.Text) || string.IsNullOrEmpty(cboGioiTinh.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }
            if (!decimal.TryParse(txtLuong.Text, out luong))
            {
                MessageBox.Show("Lương không hợp lệ.");
                return;
            }

            NVVM.SuaNhanVien(maNhanVien, tenNhanVien, soDienThoai, chucVu, ngayVaoLam, luong,gioiTinh);
        }
    }
}
