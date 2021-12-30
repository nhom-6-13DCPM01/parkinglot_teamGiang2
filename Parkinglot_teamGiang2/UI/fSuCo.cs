using Parkinglot_teamGiang2.ReportViewer;
using QLBaiGiuXe.DAO;
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

namespace Parkinglot_teamGiang2.UI
{
    public partial class fSuCo : Form
    {
        private VeXe veXe;
        private int id;
        private PhieuThanhToan phieuThanhToan;
        public fSuCo(int id, PhieuThanhToan phieuThanhToan=null)
        {
            this.phieuThanhToan = phieuThanhToan;
            this.veXe = VeXeDAO.Instence.getVeXe(id);
            InitializeComponent();
        }

        private void fSuCo_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void loadData()
        {
            BaiXe baiXe = BaiXeDAO.Instence.GetBaiXe(veXe.BaiXe);
            txtMaVe.Text = veXe.MaVeXe.ToString();
            txtLoaiXe.Text = LoaiXeDAO.Instence.getLoaiXe(baiXe.LoaiXe).TenLoaiXe;
            txtBaiXe.Text = baiXe.TenBai;
            txtVitri.Text = baiXe.ViTri;
            txtBienSo.Text = veXe.BienSoXe;
            txtTenXe.Text = veXe.TenXe;
            txtMauXe.Text = veXe.MauXe;
            txtNgay.Text = veXe.GioVao.ToString();
            if(phieuThanhToan  != null)
            {
                txtNgayXuat.Text = phieuThanhToan.GioRa.ToString();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Bạn có chắc muốn lập phiếu đền bù cho vé có mã: "+txtMaVe.Text+"\nVới số tiền: "+PriceDenBu.Value,"Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (DenBu denBu = new DenBu(veXe, txtLyDo.Text, txtBaiXe.Text, txtLoaiXe.Text, txtVitri.Text, Convert.ToDecimal(PriceDenBu.Value), DateTime.Now))
                {
                    denBu.ShowDialog();
                    VeXeDAO.Instence.deleteVeXe(veXe.MaVeXe);
                    this.Close();
                } 
            }
        }
    }
}
