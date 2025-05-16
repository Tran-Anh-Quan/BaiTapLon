using System;
using System.Windows.Forms;

namespace BaiTapLon.View
{
    partial class frmChiTietHoaDon
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChiTietHoaDon));
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnXuatBaoCao = new System.Windows.Forms.Button();
            this.btnKiemTraKH = new System.Windows.Forms.Button();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.lblDiaChi = new System.Windows.Forms.Label();
            this.txtDienThoai = new System.Windows.Forms.TextBox();
            this.lblDienThoai = new System.Windows.Forms.Label();
            this.txtTenKH = new System.Windows.Forms.TextBox();
            this.lblTenKH = new System.Windows.Forms.Label();
            this.dtpNgayLap = new System.Windows.Forms.DateTimePicker();
            this.lblNgayLap = new System.Windows.Forms.Label();
            this.txtMaHD = new System.Windows.Forms.TextBox();
            this.lblMaHD = new System.Windows.Forms.Label();
            this.dgvHoaDon = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblThanhToanValue = new System.Windows.Forms.Label();
            this.lblInvoiceDetails = new System.Windows.Forms.Label();
            this.btnThanhToan = new System.Windows.Forms.Button();
            this.lblVATValue = new System.Windows.Forms.Label();
            this.lblTongTienValue = new System.Windows.Forms.Label();
            this.lblThanhToan = new System.Windows.Forms.Label();
            this.lblVAT = new System.Windows.Forms.Label();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoaDon)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(699, 50);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(699, 50);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Quản lý hóa đơn bán hàng";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnXuatBaoCao
            // 
            this.btnXuatBaoCao.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.btnXuatBaoCao.Image = ((System.Drawing.Image)(resources.GetObject("btnXuatBaoCao.Image")));
            this.btnXuatBaoCao.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnXuatBaoCao.Location = new System.Drawing.Point(531, 90);
            this.btnXuatBaoCao.Name = "btnXuatBaoCao";
            this.btnXuatBaoCao.Size = new System.Drawing.Size(130, 36);
            this.btnXuatBaoCao.TabIndex = 11;
            this.btnXuatBaoCao.Text = "Xuất hóa đơn";
            this.btnXuatBaoCao.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnXuatBaoCao.UseVisualStyleBackColor = true;
            this.btnXuatBaoCao.Click += new System.EventHandler(this.btnXuatBaoCao_Click_1);
            // 
            // btnKiemTraKH
            // 
            this.btnKiemTraKH.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKiemTraKH.Image = ((System.Drawing.Image)(resources.GetObject("btnKiemTraKH.Image")));
            this.btnKiemTraKH.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnKiemTraKH.Location = new System.Drawing.Point(531, 36);
            this.btnKiemTraKH.Name = "btnKiemTraKH";
            this.btnKiemTraKH.Size = new System.Drawing.Size(130, 39);
            this.btnKiemTraKH.TabIndex = 10;
            this.btnKiemTraKH.Text = "    Kiểm tra";
            this.btnKiemTraKH.UseVisualStyleBackColor = true;
            this.btnKiemTraKH.Click += new System.EventHandler(this.btnKiemTraKH_Click_1);
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.Location = new System.Drawing.Point(109, 102);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new System.Drawing.Size(404, 24);
            this.txtDiaChi.TabIndex = 9;
            // 
            // lblDiaChi
            // 
            this.lblDiaChi.AutoSize = true;
            this.lblDiaChi.Location = new System.Drawing.Point(10, 104);
            this.lblDiaChi.Name = "lblDiaChi";
            this.lblDiaChi.Size = new System.Drawing.Size(57, 18);
            this.lblDiaChi.TabIndex = 8;
            this.lblDiaChi.Text = "Địa chỉ:";
            // 
            // txtDienThoai
            // 
            this.txtDienThoai.Location = new System.Drawing.Point(375, 63);
            this.txtDienThoai.Name = "txtDienThoai";
            this.txtDienThoai.Size = new System.Drawing.Size(139, 24);
            this.txtDienThoai.TabIndex = 7;
            // 
            // lblDienThoai
            // 
            this.lblDienThoai.AutoSize = true;
            this.lblDienThoai.Location = new System.Drawing.Point(281, 69);
            this.lblDienThoai.Name = "lblDienThoai";
            this.lblDienThoai.Size = new System.Drawing.Size(98, 18);
            this.lblDienThoai.TabIndex = 6;
            this.lblDienThoai.Text = "Số điện thoại:";
            // 
            // txtTenKH
            // 
            this.txtTenKH.Location = new System.Drawing.Point(375, 30);
            this.txtTenKH.Name = "txtTenKH";
            this.txtTenKH.Size = new System.Drawing.Size(139, 24);
            this.txtTenKH.TabIndex = 5;
            // 
            // lblTenKH
            // 
            this.lblTenKH.AutoSize = true;
            this.lblTenKH.Location = new System.Drawing.Point(281, 33);
            this.lblTenKH.Name = "lblTenKH";
            this.lblTenKH.Size = new System.Drawing.Size(90, 18);
            this.lblTenKH.TabIndex = 4;
            this.lblTenKH.Text = "Khách hàng:";
            // 
            // dtpNgayLap
            // 
            this.dtpNgayLap.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayLap.Location = new System.Drawing.Point(110, 63);
            this.dtpNgayLap.Name = "dtpNgayLap";
            this.dtpNgayLap.Size = new System.Drawing.Size(139, 24);
            this.dtpNgayLap.TabIndex = 3;
            // 
            // lblNgayLap
            // 
            this.lblNgayLap.AutoSize = true;
            this.lblNgayLap.Location = new System.Drawing.Point(10, 66);
            this.lblNgayLap.Name = "lblNgayLap";
            this.lblNgayLap.Size = new System.Drawing.Size(69, 18);
            this.lblNgayLap.TabIndex = 2;
            this.lblNgayLap.Text = "Ngày lập:";
            // 
            // txtMaHD
            // 
            this.txtMaHD.Location = new System.Drawing.Point(110, 33);
            this.txtMaHD.Name = "txtMaHD";
            this.txtMaHD.Size = new System.Drawing.Size(139, 24);
            this.txtMaHD.TabIndex = 1;
            // 
            // lblMaHD
            // 
            this.lblMaHD.AutoSize = true;
            this.lblMaHD.Location = new System.Drawing.Point(10, 36);
            this.lblMaHD.Name = "lblMaHD";
            this.lblMaHD.Size = new System.Drawing.Size(91, 18);
            this.lblMaHD.TabIndex = 0;
            this.lblMaHD.Text = "Mã hóa đơn:";
            // 
            // dgvHoaDon
            // 
            this.dgvHoaDon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHoaDon.Location = new System.Drawing.Point(21, 21);
            this.dgvHoaDon.Name = "dgvHoaDon";
            this.dgvHoaDon.RowHeadersWidth = 51;
            this.dgvHoaDon.RowTemplate.Height = 29;
            this.dgvHoaDon.Size = new System.Drawing.Size(617, 163);
            this.dgvHoaDon.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnXuatBaoCao);
            this.groupBox1.Controls.Add(this.btnKiemTraKH);
            this.groupBox1.Controls.Add(this.txtDiaChi);
            this.groupBox1.Controls.Add(this.txtMaHD);
            this.groupBox1.Controls.Add(this.lblMaHD);
            this.groupBox1.Controls.Add(this.lblDiaChi);
            this.groupBox1.Controls.Add(this.lblNgayLap);
            this.groupBox1.Controls.Add(this.dtpNgayLap);
            this.groupBox1.Controls.Add(this.txtDienThoai);
            this.groupBox1.Controls.Add(this.lblDienThoai);
            this.groupBox1.Controls.Add(this.txtTenKH);
            this.groupBox1.Controls.Add(this.lblTenKH);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(20, 70);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(667, 150);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin hóa đơn";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvHoaDon);
            this.groupBox2.Location = new System.Drawing.Point(20, 237);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(661, 202);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Chi tiết hóa đơn";
            // 
            // lblThanhToanValue
            // 
            this.lblThanhToanValue.AutoSize = true;
            this.lblThanhToanValue.Location = new System.Drawing.Point(195, 107);
            this.lblThanhToanValue.Name = "lblThanhToanValue";
            this.lblThanhToanValue.Size = new System.Drawing.Size(0, 16);
            this.lblThanhToanValue.TabIndex = 14;
            // 
            // lblInvoiceDetails
            // 
            this.lblInvoiceDetails.AutoSize = true;
            this.lblInvoiceDetails.Location = new System.Drawing.Point(6, 0);
            this.lblInvoiceDetails.Name = "lblInvoiceDetails";
            this.lblInvoiceDetails.Size = new System.Drawing.Size(126, 16);
            this.lblInvoiceDetails.TabIndex = 7;
            this.lblInvoiceDetails.Text = "Thanh toán hóa đơn";
            // 
            // btnThanhToan
            // 
            this.btnThanhToan.Image = ((System.Drawing.Image)(resources.GetObject("btnThanhToan.Image")));
            this.btnThanhToan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnThanhToan.Location = new System.Drawing.Point(510, 107);
            this.btnThanhToan.Name = "btnThanhToan";
            this.btnThanhToan.Size = new System.Drawing.Size(121, 40);
            this.btnThanhToan.TabIndex = 12;
            this.btnThanhToan.Text = "     Thanh toán";
            this.btnThanhToan.UseVisualStyleBackColor = true;
            // 
            // lblVATValue
            // 
            this.lblVATValue.AutoSize = true;
            this.lblVATValue.Location = new System.Drawing.Point(194, 74);
            this.lblVATValue.Name = "lblVATValue";
            this.lblVATValue.Size = new System.Drawing.Size(0, 16);
            this.lblVATValue.TabIndex = 13;
            // 
            // lblTongTienValue
            // 
            this.lblTongTienValue.AutoSize = true;
            this.lblTongTienValue.Location = new System.Drawing.Point(192, 40);
            this.lblTongTienValue.Name = "lblTongTienValue";
            this.lblTongTienValue.Size = new System.Drawing.Size(0, 16);
            this.lblTongTienValue.TabIndex = 11;
            // 
            // lblThanhToan
            // 
            this.lblThanhToan.AutoSize = true;
            this.lblThanhToan.Location = new System.Drawing.Point(58, 95);
            this.lblThanhToan.Name = "lblThanhToan";
            this.lblThanhToan.Size = new System.Drawing.Size(115, 16);
            this.lblThanhToan.TabIndex = 10;
            this.lblThanhToan.Text = "Số tiền thanh toán:";
            // 
            // lblVAT
            // 
            this.lblVAT.AutoSize = true;
            this.lblVAT.Location = new System.Drawing.Point(57, 62);
            this.lblVAT.Name = "lblVAT";
            this.lblVAT.Size = new System.Drawing.Size(108, 16);
            this.lblVAT.TabIndex = 9;
            this.lblVAT.Text = "Thuế VAT (10%):";
            // 
            // lblTongTien
            // 
            this.lblTongTien.AutoSize = true;
            this.lblTongTien.Location = new System.Drawing.Point(55, 28);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Size = new System.Drawing.Size(118, 16);
            this.lblTongTien.TabIndex = 8;
            this.lblTongTien.Text = "Tổng tiền hóa đơn:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblTongTien);
            this.groupBox3.Controls.Add(this.lblInvoiceDetails);
            this.groupBox3.Controls.Add(this.lblThanhToanValue);
            this.groupBox3.Controls.Add(this.lblVAT);
            this.groupBox3.Controls.Add(this.lblThanhToan);
            this.groupBox3.Controls.Add(this.btnThanhToan);
            this.groupBox3.Controls.Add(this.lblVATValue);
            this.groupBox3.Controls.Add(this.lblTongTienValue);
            this.groupBox3.Location = new System.Drawing.Point(20, 455);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(661, 162);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "groupBox3";
            this.groupBox3.Enter += new System.EventHandler(this.groupBox3_Enter);
            // 
            // frmChiTietHoaDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 628);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pnlHeader);
            this.Name = "frmChiTietHoaDon";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý hóa đơn bán hàng";
            this.pnlHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoaDon)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        private void pnlInvoiceDetails_Paint(object sender, PaintEventArgs e)
        {
            throw new NotImplementedException();
        }

        //private void pnlInvoiceDetails_Paint(object sender, PaintEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        private Panel pnlHeader;
        private Label lblTitle;
        private TextBox txtMaHD;
        private Label lblMaHD;
        private DateTimePicker dtpNgayLap;
        private Label lblNgayLap;
        private TextBox txtTenKH;
        private Label lblTenKH;
        private TextBox txtDienThoai;
        private Label lblDienThoai;
        private TextBox txtDiaChi;
        private Label lblDiaChi;
        private Button btnKiemTraKH;
        private Button btnXuatBaoCao;
        private DataGridView dgvHoaDon;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label lblThanhToanValue;
        private Label lblInvoiceDetails;
        private Button btnThanhToan;
        private Label lblVATValue;
        private Label lblTongTienValue;
        private Label lblThanhToan;
        private Label lblVAT;
        private Label lblTongTien;
        private GroupBox groupBox3;
    }
}