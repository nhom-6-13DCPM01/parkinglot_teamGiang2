using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parkinglot_teamGiang2.ReportViewer
{
    public partial class VeGuiXe : Form
    {
        int maVeXe;
        string loaiXe, tenBai, bienSoXe, mauXe, tenXe;
        DateTime ngayGio;
        public VeGuiXe(int maVeXe,string loaiXe,string tenBai,string bienSoXe,string mauXe,string tenXe, DateTime ngayGio)
        {
            InitializeComponent();
            this.maVeXe = maVeXe;
            this.loaiXe = loaiXe;
            this.tenBai = tenBai;
            this.bienSoXe = bienSoXe;
            this.mauXe = mauXe;
            this.tenXe = tenXe;
            this.ngayGio = ngayGio;
        }

        private void VeGuiXe_Load(object sender, EventArgs e)
        {
            ReportParameter[] para = new ReportParameter[]
            {
                new ReportParameter("MaVeXe",maVeXe.ToString()),
                new ReportParameter("LoaiXe",loaiXe),
                new ReportParameter("TenBaiXe",tenBai),
                new ReportParameter("BienSoXe",bienSoXe),
                new ReportParameter("MauXe",mauXe),
                new ReportParameter("NgayGio",ngayGio.ToString())
            };
            this.reportViewer1.LocalReport.SetParameters(para);
            this.reportViewer1.RefreshReport();
        }

    }
}
