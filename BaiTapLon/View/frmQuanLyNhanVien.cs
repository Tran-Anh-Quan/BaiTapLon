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
        }
    }
}
