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
    public partial class fSuaVe : Form
    {
        VeXe veXe;
        public fSuaVe(VeXe veXe)
        {
            this.veXe = veXe;
            InitializeComponent();
            loadDataCB();
            loadData();
        }
        private void loadDataCB()
        {
            cbLoaiXe.DataSource = null;
            cbLoaiXe.DisplayMember = "TenLoaiXe";
            cbLoaiXe.ValueMember = "MaLoaiXe";
            cbLoaiXe.DataSource = LoaiXeDAO.Instence.getListLoaiXe();
        }
        private void loadData()
        {
            BaiXe baiXe = BaiXeDAO.Instence.GetBaiXe(veXe.BaiXe);
            txtMaVe.Text = veXe.MaVeXe.ToString();
            txtBienSo.Text = veXe.BienSoXe;
            txtTenXe.Text = Convert.ToString(veXe.TenXe);
            txtMauXe.Text = Convert.ToString(veXe.MauXe);
            txtNgay.Text = veXe.GioVao.ToString();
            cbLoaiXe.SelectedValue = baiXe.LoaiXe;
            cbBaiXe.SelectedValue = veXe.BaiXe;
        }

        private void cbLoaiXe_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id;
            Int32.TryParse(Convert.ToString(cbLoaiXe.SelectedValue), out id);
            BaiXe baiXe = BaiXeDAO.Instence.GetBaiXe(veXe.BaiXe);
            cbBaiXe.DataSource = null;
            cbBaiXe.DisplayMember = "TenBai";
            cbBaiXe.ValueMember = "MaBai";
            List<BaiXe> baiXes = BaiXeDAO.Instence.getListBaiXeNonVe(id);
            if(baiXe.LoaiXe == id)
            {
                baiXes.Add(baiXe);
            }
            cbBaiXe.DataSource = baiXes;
            
        }

        public bool xacNhan(string Message)
        {
            if (MessageBox.Show(Message, "EF CRUP Operation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                return true;
            }
            return false;
        }

        private bool CheckInput()
        {
            if (cbBaiXe.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn bãi xe", "Notification");
                return false;
            }

            if (txtBienSo.Text == "")
            {
                MessageBox.Show("Vui lòng nhập biển số xe, please", "Notification");
                txtBienSo.Focus();
                return false;
            }

            return true;
        }
        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (!CheckInput())
            {
                return;
            }

            if (!xacNhan("Bạn có chắc muốn sửa vé"))
            {
                return;
            }
            veXe.BaiXe = Convert.ToInt32(cbBaiXe.SelectedValue);
            veXe.BienSoXe = txtBienSo.Text;
            veXe.TenXe = txtTenXe.Text;
            veXe.MauXe = txtMauXe.Text;

            VeXeDAO.Instence.updateVeXe(veXe);

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!CheckInput())
            {
                return;
            }

            if (!xacNhan("Bạn có chắc muốn in vé đồng thời sửa lại vé"))
            {
                return;
            }
            veXe.BaiXe = Convert.ToInt32(cbBaiXe.SelectedValue);
            veXe.BienSoXe = txtBienSo.Text;
            veXe.TenXe = txtTenXe.Text;
            veXe.MauXe = txtMauXe.Text;

            VeXeDAO.Instence.updateVeXe(veXe);

            using (VeGuiXe veGuiXe = new VeGuiXe(veXe.MaVeXe, cbLoaiXe.Text, cbBaiXe.Text, veXe.BienSoXe, veXe.MauXe, veXe.TenXe, veXe.GioVao))
            {
                veGuiXe.ShowDialog();
            }
            this.Close();
        }
    }
}
