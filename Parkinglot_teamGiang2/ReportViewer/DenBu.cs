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
    public partial class DenBu : Form
    {
        private VeXe veXe;
        private string lyDo, tenBai, loaiXe,viTri;
        private Decimal soTienDenBu;
        private DateTime ngayLapPhieu;
        public DenBu(VeXe veXe, string lyDo, string tenBai, string loaiXe,string viTri, Decimal soTienDenBu, DateTime ngayLapPhieu)
        {
            this.veXe = veXe;
            this.lyDo = lyDo;
            this.tenBai = tenBai;
            this.loaiXe = loaiXe;
            this.viTri = viTri;
            this.soTienDenBu = soTienDenBu;
            this.ngayLapPhieu = ngayLapPhieu;
            InitializeComponent();
        }

        private void DenBu_Load(object sender, EventArgs e)
        {
            ReportParameter[] para = new ReportParameter[]
           {
                new ReportParameter("MaVe",veXe.MaVeXe.ToString()),
                new ReportParameter("LoaiXe",loaiXe),
                new ReportParameter("BaiXe",tenBai),
                new ReportParameter("BienSoXe",veXe.BienSoXe),
                new ReportParameter("TenXe",veXe.TenXe),
                new ReportParameter("MauXe",veXe.MauXe),
                new ReportParameter("ViTri",viTri),
                new ReportParameter("LyDo",lyDo),
                new ReportParameter("NgayLapPhieu",ngayLapPhieu.ToString()),
                new ReportParameter("SoTienDenBu",soTienDenBu.ToString("#,##")+"đ")
           };
            this.reportViewer1.LocalReport.SetParameters(para);
            this.reportViewer1.RefreshReport();
        }
    }
}
