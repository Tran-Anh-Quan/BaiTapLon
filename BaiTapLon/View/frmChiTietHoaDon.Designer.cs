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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlCustomerInfo = new System.Windows.Forms.Panel();
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
            this.pnlInvoiceDetails = new System.Windows.Forms.Panel();
            this.dgvHoaDon = new System.Windows.Forms.DataGridView();
            this.lblInvoiceDetails = new System.Windows.Forms.Label();
            this.pnlSummary = new System.Windows.Forms.Panel();
            this.lblThanhToanValue = new System.Windows.Forms.Label();
            this.lblVATValue = new System.Windows.Forms.Label();
            this.lblTongTienValue = new System.Windows.Forms.Label();
            this.lblThanhToan = new System.Windows.Forms.Label();
            this.lblVAT = new System.Windows.Forms.Label();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.btnThanhToan = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlCustomerInfo.SuspendLayout();
            this.pnlInvoiceDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoaDon)).BeginInit();
            this.pnlSummary.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(900, 50);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(900, 50);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Quản lý hóa đơn bán hàng";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlCustomerInfo
            // 
            this.pnlCustomerInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCustomerInfo.Controls.Add(this.btnXuatBaoCao);
            this.pnlCustomerInfo.Controls.Add(this.btnKiemTraKH);
            this.pnlCustomerInfo.Controls.Add(this.txtDiaChi);
            this.pnlCustomerInfo.Controls.Add(this.lblDiaChi);
            this.pnlCustomerInfo.Controls.Add(this.txtDienThoai);
            this.pnlCustomerInfo.Controls.Add(this.lblDienThoai);
            this.pnlCustomerInfo.Controls.Add(this.txtTenKH);
            this.pnlCustomerInfo.Controls.Add(this.lblTenKH);
            this.pnlCustomerInfo.Controls.Add(this.dtpNgayLap);
            this.pnlCustomerInfo.Controls.Add(this.lblNgayLap);
            this.pnlCustomerInfo.Controls.Add(this.txtMaHD);
            this.pnlCustomerInfo.Controls.Add(this.lblMaHD);
            this.pnlCustomerInfo.Location = new System.Drawing.Point(20, 70);
            this.pnlCustomerInfo.Name = "pnlCustomerInfo";
            this.pnlCustomerInfo.Size = new System.Drawing.Size(860, 150);
            this.pnlCustomerInfo.TabIndex = 1;
            // 
            // btnXuatBaoCao
            // 
            this.btnXuatBaoCao.Location = new System.Drawing.Point(700, 66);
            this.btnXuatBaoCao.Name = "btnXuatBaoCao";
            this.btnXuatBaoCao.Size = new System.Drawing.Size(120, 30);
            this.btnXuatBaoCao.TabIndex = 11;
            this.btnXuatBaoCao.Text = "Xuất báo cáo";
            this.btnXuatBaoCao.UseVisualStyleBackColor = true;
            this.btnXuatBaoCao.Click += new System.EventHandler(this.btnXuatBaoCao_Click_1);
            // 
            // btnKiemTraKH
            // 
            this.btnKiemTraKH.Location = new System.Drawing.Point(700, 30);
            this.btnKiemTraKH.Name = "btnKiemTraKH";
            this.btnKiemTraKH.Size = new System.Drawing.Size(120, 30);
            this.btnKiemTraKH.TabIndex = 10;
            this.btnKiemTraKH.Text = "Kiểm tra";
            this.btnKiemTraKH.UseVisualStyleBackColor = true;
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.Location = new System.Drawing.Point(450, 66);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new System.Drawing.Size(200, 22);
            this.txtDiaChi.TabIndex = 9;
            // 
            // lblDiaChi
            // 
            this.lblDiaChi.AutoSize = true;
            this.lblDiaChi.Location = new System.Drawing.Point(350, 66);
            this.lblDiaChi.Name = "lblDiaChi";
            this.lblDiaChi.Size = new System.Drawing.Size(50, 16);
            this.lblDiaChi.TabIndex = 8;
            this.lblDiaChi.Text = "Địa chỉ:";
            // 
            // txtDienThoai
            // 
            this.txtDienThoai.Location = new System.Drawing.Point(450, 30);
            this.txtDienThoai.Name = "txtDienThoai";
            this.txtDienThoai.Size = new System.Drawing.Size(200, 22);
            this.txtDienThoai.TabIndex = 7;
            // 
            // lblDienThoai
            // 
            this.lblDienThoai.AutoSize = true;
            this.lblDienThoai.Location = new System.Drawing.Point(350, 33);
            this.lblDienThoai.Name = "lblDienThoai";
            this.lblDienThoai.Size = new System.Drawing.Size(88, 16);
            this.lblDienThoai.TabIndex = 6;
            this.lblDienThoai.Text = "Số điện thoại:";
            // 
            // txtTenKH
            // 
            this.txtTenKH.Location = new System.Drawing.Point(120, 90);
            this.txtTenKH.Name = "txtTenKH";
            this.txtTenKH.Size = new System.Drawing.Size(200, 22);
            this.txtTenKH.TabIndex = 5;
            // 
            // lblTenKH
            // 
            this.lblTenKH.AutoSize = true;
            this.lblTenKH.Location = new System.Drawing.Point(20, 93);
            this.lblTenKH.Name = "lblTenKH";
            this.lblTenKH.Size = new System.Drawing.Size(80, 16);
            this.lblTenKH.TabIndex = 4;
            this.lblTenKH.Text = "Khách hàng:";
            // 
            // dtpNgayLap
            // 
            this.dtpNgayLap.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayLap.Location = new System.Drawing.Point(120, 60);
            this.dtpNgayLap.Name = "dtpNgayLap";
            this.dtpNgayLap.Size = new System.Drawing.Size(200, 22);
            this.dtpNgayLap.TabIndex = 3;
            // 
            // lblNgayLap
            // 
            this.lblNgayLap.AutoSize = true;
            this.lblNgayLap.Location = new System.Drawing.Point(20, 63);
            this.lblNgayLap.Name = "lblNgayLap";
            this.lblNgayLap.Size = new System.Drawing.Size(65, 16);
            this.lblNgayLap.TabIndex = 2;
            this.lblNgayLap.Text = "Ngày lập:";
            // 
            // txtMaHD
            // 
            this.txtMaHD.Location = new System.Drawing.Point(120, 30);
            this.txtMaHD.Name = "txtMaHD";
            this.txtMaHD.Size = new System.Drawing.Size(200, 22);
            this.txtMaHD.TabIndex = 1;
            // 
            // lblMaHD
            // 
            this.lblMaHD.AutoSize = true;
            this.lblMaHD.Location = new System.Drawing.Point(20, 33);
            this.lblMaHD.Name = "lblMaHD";
            this.lblMaHD.Size = new System.Drawing.Size(81, 16);
            this.lblMaHD.TabIndex = 0;
            this.lblMaHD.Text = "Mã hóa đơn:";
            // 
            // pnlInvoiceDetails
            // 
            this.pnlInvoiceDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlInvoiceDetails.Controls.Add(this.dgvHoaDon);
            this.pnlInvoiceDetails.Controls.Add(this.lblInvoiceDetails);
            this.pnlInvoiceDetails.Location = new System.Drawing.Point(20, 240);
            this.pnlInvoiceDetails.Name = "pnlInvoiceDetails";
            this.pnlInvoiceDetails.Size = new System.Drawing.Size(860, 300);
            this.pnlInvoiceDetails.TabIndex = 2;
            //this.pnlInvoiceDetails.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlInvoiceDetails_Paint);
            // 
            // dgvHoaDon
            // 
            this.dgvHoaDon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHoaDon.Location = new System.Drawing.Point(20, 50);
            this.dgvHoaDon.Name = "dgvHoaDon";
            this.dgvHoaDon.RowHeadersWidth = 51;
            this.dgvHoaDon.RowTemplate.Height = 29;
            this.dgvHoaDon.Size = new System.Drawing.Size(820, 230);
            this.dgvHoaDon.TabIndex = 1;
            // 
            // lblInvoiceDetails
            // 
            this.lblInvoiceDetails.AutoSize = true;
            this.lblInvoiceDetails.Location = new System.Drawing.Point(20, 20);
            this.lblInvoiceDetails.Name = "lblInvoiceDetails";
            this.lblInvoiceDetails.Size = new System.Drawing.Size(98, 16);
            this.lblInvoiceDetails.TabIndex = 0;
            this.lblInvoiceDetails.Text = "Chi tiết hóa đơn";
            // 
            // pnlSummary
            // 
            this.pnlSummary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSummary.Controls.Add(this.lblThanhToanValue);
            this.pnlSummary.Controls.Add(this.lblVATValue);
            this.pnlSummary.Controls.Add(this.lblTongTienValue);
            this.pnlSummary.Controls.Add(this.lblThanhToan);
            this.pnlSummary.Controls.Add(this.lblVAT);
            this.pnlSummary.Controls.Add(this.lblTongTien);
            this.pnlSummary.Location = new System.Drawing.Point(20, 560);
            this.pnlSummary.Name = "pnlSummary";
            this.pnlSummary.Size = new System.Drawing.Size(600, 80);
            this.pnlSummary.TabIndex = 3;
            // 
            // lblThanhToanValue
            // 
            this.lblThanhToanValue.AutoSize = true;
            this.lblThanhToanValue.Location = new System.Drawing.Point(450, 50);
            this.lblThanhToanValue.Name = "lblThanhToanValue";
            this.lblThanhToanValue.Size = new System.Drawing.Size(0, 16);
            this.lblThanhToanValue.TabIndex = 5;
            // 
            // lblVATValue
            // 
            this.lblVATValue.AutoSize = true;
            this.lblVATValue.Location = new System.Drawing.Point(450, 30);
            this.lblVATValue.Name = "lblVATValue";
            this.lblVATValue.Size = new System.Drawing.Size(0, 16);
            this.lblVATValue.TabIndex = 4;
            // 
            // lblTongTienValue
            // 
            this.lblTongTienValue.AutoSize = true;
            this.lblTongTienValue.Location = new System.Drawing.Point(450, 10);
            this.lblTongTienValue.Name = "lblTongTienValue";
            this.lblTongTienValue.Size = new System.Drawing.Size(0, 16);
            this.lblTongTienValue.TabIndex = 3;
            // 
            // lblThanhToan
            // 
            this.lblThanhToan.AutoSize = true;
            this.lblThanhToan.Location = new System.Drawing.Point(250, 50);
            this.lblThanhToan.Name = "lblThanhToan";
            this.lblThanhToan.Size = new System.Drawing.Size(115, 16);
            this.lblThanhToan.TabIndex = 2;
            this.lblThanhToan.Text = "Số tiền thanh toán:";
            // 
            // lblVAT
            // 
            this.lblVAT.AutoSize = true;
            this.lblVAT.Location = new System.Drawing.Point(250, 30);
            this.lblVAT.Name = "lblVAT";
            this.lblVAT.Size = new System.Drawing.Size(108, 16);
            this.lblVAT.TabIndex = 1;
            this.lblVAT.Text = "Thuế VAT (10%):";
            // 
            // lblTongTien
            // 
            this.lblTongTien.AutoSize = true;
            this.lblTongTien.Location = new System.Drawing.Point(250, 10);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Size = new System.Drawing.Size(118, 16);
            this.lblTongTien.TabIndex = 0;
            this.lblTongTien.Text = "Tổng tiền hóa đơn:";
            // 
            // btnThanhToan
            // 
            this.btnThanhToan.Location = new System.Drawing.Point(700, 570);
            this.btnThanhToan.Name = "btnThanhToan";
            this.btnThanhToan.Size = new System.Drawing.Size(150, 40);
            this.btnThanhToan.TabIndex = 4;
            this.btnThanhToan.Text = "Thanh toán";
            this.btnThanhToan.UseVisualStyleBackColor = true;
            // 
            // frmChiTietHoaDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 660);
            this.Controls.Add(this.btnThanhToan);
            this.Controls.Add(this.pnlSummary);
            this.Controls.Add(this.pnlInvoiceDetails);
            this.Controls.Add(this.pnlCustomerInfo);
            this.Controls.Add(this.pnlHeader);
            this.Name = "frmChiTietHoaDon";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý hóa đơn bán hàng";
            this.pnlHeader.ResumeLayout(false);
            this.pnlCustomerInfo.ResumeLayout(false);
            this.pnlCustomerInfo.PerformLayout();
            this.pnlInvoiceDetails.ResumeLayout(false);
            this.pnlInvoiceDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoaDon)).EndInit();
            this.pnlSummary.ResumeLayout(false);
            this.pnlSummary.PerformLayout();
            this.ResumeLayout(false);

        }

        //private void pnlInvoiceDetails_Paint(object sender, PaintEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        private Panel pnlHeader;
        private Label lblTitle;
        private Panel pnlCustomerInfo;
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
        private Panel pnlInvoiceDetails;
        private DataGridView dgvHoaDon;
        private Label lblInvoiceDetails;
        private Panel pnlSummary;
        private Label lblTongTien;
        private Label lblTongTienValue;
        private Label lblVAT;
        private Label lblVATValue;
        private Label lblThanhToan;
        private Label lblThanhToanValue;
        private Button btnThanhToan;
    }
}