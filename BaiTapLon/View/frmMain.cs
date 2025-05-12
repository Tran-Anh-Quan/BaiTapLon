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
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDangNhap formDangNhap = new frmDangNhap();
            formDangNhap.Show();
            this.Close();
        }
        private void OpenChild(Form childForm)
        {
            foreach (Form form in this.MdiChildren)
            {
                if (form.GetType() == childForm.GetType())
                {
                    form.Activate(); 
                    return;
                }
            }
            childForm.MdiParent = this; 
            childForm.Show();           
        }
        private void thoátToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void sảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChild(new frmQuanLySanPham());
        }

        private void nhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChild(new frmQuanLyNhanVien());
        }

        private void kháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChild(new frmQuanLyKhachHang());
        }

        private void hóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChild(new frmHoaDon());
        }

        private void tàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChild(new frmQuanLyTaiKhoan());
        }
    }
}
