using System;
using System.Windows.Forms;

namespace BaiTapLon.View
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void mnuDangXuat_Click(object sender, EventArgs e)
        {
            frmDangNhap frm = new frmDangNhap();
            frm.Show();
            this.Hide();
        }

        private void mnuThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mnuSanPham_Click(object sender, EventArgs e)
        {
            OpenForm(new frmQuanLySanPham());
        }

        private void mnuNhanVien_Click(object sender, EventArgs e)
        {
            OpenForm(new frmQuanLyNhanVien());
        }

        private void mnuKhachHang_Click(object sender, EventArgs e)
        {
            OpenForm(new frmQuanLyKhachHang());
        }

        private void mnuHoaDon_Click(object sender, EventArgs e)
        {
            OpenForm(new frmHoaDon());
        }

        private void mnuTaiKhoan_Click(object sender, EventArgs e)
        {
            OpenForm(new frmQuanLyTaiKhoan());
        }

        private void OpenForm(Form form)
        {
            form.FormClosed += (s, args) => this.Show();
            form.Show();
            this.Hide();
        }
    }
}