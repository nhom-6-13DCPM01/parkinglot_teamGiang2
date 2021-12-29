using Microsoft.Reporting.WinForms;
using QLBaiGiuXe.Model;
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
    public partial class ThanhToan : Form
    {
        private VeXe veXe;
        private PhieuThanhToan phieuThanhToan;
        private string loaiXe, tenBai;
        public ThanhToan(VeXe veXe , PhieuThanhToan phieuThanhToan,string loaiXe,string tenBai)
        {
            this.veXe = veXe;
            this.phieuThanhToan = phieuThanhToan;
            this.loaiXe = loaiXe;
            this.tenBai = tenBai;
            InitializeComponent();
        }

        private void ThanhToan_Load(object sender, EventArgs e)
        {
            ReportParameter[] para = new ReportParameter[]
           {
                new ReportParameter("MaVeXe",veXe.MaVeXe.ToString()),
                new ReportParameter("LoaiXe",loaiXe),
                new ReportParameter("TenBai",tenBai),
                new ReportParameter("BienSoXe",veXe.BienSoXe),
                new ReportParameter("MauXe",veXe.MauXe),
                new ReportParameter("NgayGui",veXe.GioVao.ToString()),
                new ReportParameter("NgayXuatBai",phieuThanhToan.GioRa.ToString()),
                new ReportParameter("TongTien",phieuThanhToan.TongTien.ToString("#,##")+"đ")
           };
            this.reportViewer1.LocalReport.SetParameters(para);
            this.reportViewer1.RefreshReport();
        }
    }
}
