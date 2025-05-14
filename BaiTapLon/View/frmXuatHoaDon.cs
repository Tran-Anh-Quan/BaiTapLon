
using BaiTapLon.dataTableAdapters;
using BaiTapLon.Report;
using CrystalDecisions.CrystalReports.Engine;
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
    public partial class frmXuatHoaDon : Form
    {
        public frmXuatHoaDon()
        {
            InitializeComponent();
        }

        private void frmXuatHoaDon_Load(object sender, EventArgs e)
        {

        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            HoaDonTableAdapter hd = new HoaDonTableAdapter();
            DataTable dt = hd.GetData();

            ReportDocument reportDocument = new ReportDocument();
            reportDocument.Load("C:\\Users\\Admin\\source\\repos\\BaiTapLon\\BaiTapLon\\Report\\BaoCaopHoaDon.rpt");


            reportDocument.SetDataSource(dt);

            crystalReportViewer1.ReportSource = reportDocument;
            crystalReportViewer1.Refresh(); 
        }

    }
}
