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
    public partial class frmQuanLySanPham : Form
    {
        private SanPhamVM SPVM = new SanPhamVM();
        BindingSource bindingSource = new BindingSource();
        public frmQuanLySanPham()
        {
            InitializeComponent();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            SanPham sp = new SanPham
            {
                MaSanPham = txtMaSanPham.Text.Trim(),
                TenSanPham = txtTenSanPham.Text.Trim(),
                DonGia = decimal.Parse(txtDonGia.Text.Trim()),
                SoLuongTon = int.Parse(txtSoLuongTon.Text.Trim())
            };

            if (SPVM.ThemSanPham(sp))
            {
                MessageBox.Show("Thêm sản phẩm thành công!");
            }
            else
            {
                MessageBox.Show("Thêm sản phẩm thất bại!");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string maSanPham = txtMaSanPham.Text.Trim();
            string tenSanPham = txtTenSanPham.Text.Trim();
            decimal donGia;
            int soLuongTon;

            if (string.IsNullOrEmpty(maSanPham) || string.IsNullOrEmpty(tenSanPham) ||
                string.IsNullOrEmpty(txtDonGia.Text) || string.IsNullOrEmpty(txtSoLuongTon.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            if (!decimal.TryParse(txtDonGia.Text, out donGia))
            {
                MessageBox.Show("Đơn giá không hợp lệ.");
                return;
            }

            if (!int.TryParse(txtSoLuongTon.Text, out soLuongTon))
            {
                MessageBox.Show("Số lượng tồn không hợp lệ.");
                return;
            }
            SPVM.SuaSanPham(maSanPham, tenSanPham, donGia, soLuongTon);

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string maSanPham = dataGridView1.SelectedRows[0].Cells["MaSanPham"].Value.ToString();
                SPVM.XoaSanPham(maSanPham);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa.");
            }

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string maSanPham = txtMaSanPham.Text.Trim();
            if (string.IsNullOrEmpty(maSanPham))
            {
                MessageBox.Show("Vui lòng nhập mã sản phẩm cần tìm kiếm.");
                return;
            }

            SanPham sp = SPVM.LaySanPhamTheoMa(maSanPham);
            if (sp == null)
            {
                MessageBox.Show("Không tìm thấy sản phẩm.");
                return;
            }

            txtTenSanPham.Text = sp.TenSanPham;
            txtDonGia.Text = sp.DonGia.ToString();
            txtSoLuongTon.Text = sp.SoLuongTon.ToString();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["MaSanPham"].Value != null &&
                    row.Cells["MaSanPham"].Value.ToString().Equals(maSanPham, StringComparison.OrdinalIgnoreCase))
                {
                    row.Selected = true;
                    dataGridView1.CurrentCell = row.Cells[0];
                    break;
                }
            }

        }
        private void HienThiSanPham()
        {
            bindingSource.Clear();
            BindingList<SanPham> sanPhamList = SPVM.LayTatCaSanPham();
            bindingSource.DataSource = sanPhamList;
        }
        private void LamMoiThongTin()
        {
            txtMaSanPham.Clear();
            txtTenSanPham.Clear();
            txtDonGia.Clear();
            txtSoLuongTon.Clear();
            txtMaSanPham.Focus();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            HienThiSanPham();
            LamMoiThongTin();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView1.Rows[e.RowIndex].Selected = true;

                var maSanPhamValue = dataGridView1.Rows[e.RowIndex].Cells["MaSanPham"].Value;

                if (maSanPhamValue != null && !string.IsNullOrEmpty(maSanPhamValue.ToString()))
                {
                    string maSanPham = maSanPhamValue.ToString();
                }
                else
                {
                    MessageBox.Show("Mã sản phẩm không hợp lệ.");
                    return;
                }

                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                txtMaSanPham.Text = row.Cells["MaSanPham"].Value.ToString();
                txtTenSanPham.Text = row.Cells["TenSanPham"].Value.ToString();
                txtDonGia.Text = row.Cells["DonGia"].Value.ToString();
                txtSoLuongTon.Text = row.Cells["SoLuongTon"].Value.ToString();
            }

        }

        private void frmQuanLySanPham_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView1.DataSource = bindingSource;
            HienThiSanPham();
        }
    }
}
