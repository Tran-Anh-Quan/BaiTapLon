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
        private void OpenChildForm(Form childForm)
        {

            foreach (Form frm in this.MdiChildren)
            {
                frm.Close();
            }

            childForm.MdiParent = this;
            childForm.WindowState = FormWindowState.Maximized;
            childForm.Show();
        }
        private void mnuDangXuat_Click(object sender, EventArgs e)
        {
            this.Hide();  
            frmDangNhap frm = new frmDangNhap();
            frm.Show();
              
        }

        private void mnuThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mnuSanPham_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmQuanLySanPham());
        }

        private void mnuNhanVien_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmQuanLyNhanVien());
        }

        private void mnuKhachHang_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmQuanLyKhachHang());
        }

        private void mnuHoaDon_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmHoaDon());
        }

        private void mnuTaiKhoan_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmQuanLyTaiKhoan());
        }


        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            switch (e.Node.Text)
            {
                case "Quản lý nhân viên":
                    OpenChildForm(new frmQuanLyNhanVien());
                    break;

                case "Quản lý tài khoản":
                    OpenChildForm(new frmQuanLyTaiKhoan());
                    break;

                case "Quản lý sản phẩm":
                    OpenChildForm(new frmQuanLySanPham());
                    break;
                case "Quản lý khách hàng":
                    OpenChildForm(new frmQuanLyKhachHang());
                    break;

                case "Quản lý hóa đơn":
                    OpenChildForm(new frmHoaDon());
                    break;

                case "Thoát":
                    Application.Exit();
                    break;

                case "Đăng xuất":
                    this.Hide();
                    frmDangNhap frmLogin = new frmDangNhap();
                    frmLogin.ShowDialog();
                    break;

            }
        }

        private void mnuHeThong_Click(object sender, EventArgs e)
        {

        }
    }
}