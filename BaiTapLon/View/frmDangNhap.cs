using BaiTapLon.ViewModels;
using System;
using System.Windows.Forms;

namespace BaiTapLon.View
{
    public partial class frmDangNhap : Form
    {
        private readonly TaiKhoanVM viewModel = new TaiKhoanVM();

        public frmDangNhap()
        {
            InitializeComponent();
        }
        private void btnDangNhap_Click_1(object sender, EventArgs e)
        {
            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();

            if (viewModel.DangNhapTaiKhoan(tenDangNhap, matKhau, this))
            {
                MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmMain fm = new frmMain();
                fm.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click_1(object sender, EventArgs e)
        {

        }

    }
}