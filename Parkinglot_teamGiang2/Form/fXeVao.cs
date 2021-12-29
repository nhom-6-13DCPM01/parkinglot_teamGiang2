using Parkinglot_teamGiang2.ReportViewer;
using QLBaiGiuXe.DAO;
using QLBaiGiuXe.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBaiGiuXe
{
    public partial class fXeVao : Form
    {
        public fXeVao()
        {
            InitializeComponent();
        }

        private void fXeVao_Load(object sender, EventArgs e)
        {
            LoadTheme();
            loadData();
        }

        private void LoadTheme()
        {
            foreach (Control btns in this.Controls)
            {
                if (btns.GetType() == typeof(Button))
                {
                    Button btn = (Button)btns;
                    btn.BackColor = ThemeColor.PrimaryColor;
                    btn.ForeColor = Color.White;
                    btn.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;
                }
            }
            label1.ForeColor = ThemeColor.PrimaryColor;
            label2.ForeColor = ThemeColor.PrimaryColor;
            label3.ForeColor = ThemeColor.PrimaryColor;
            label4.ForeColor = ThemeColor.SecondaryColor;
            label5.ForeColor = ThemeColor.SecondaryColor;
            lbThongBao.ForeColor = ThemeColor.PrimaryColor;
            label8.ForeColor = ThemeColor.PrimaryColor;
            
        }

        public void loadData()
        {
            loadDataGrid();
            cbLoaiXe.DisplayMember = "TenLoaiXe";
            cbLoaiXe.ValueMember = "MaLoaiXe";
            cbLoaiXe.DataSource = LoaiXeDAO.Instence.getListLoaiXe();
            return;
        }

        public bool xacNhan(string Message)
        {
            if (MessageBox.Show(Message, "EF CRUP Operation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                return true;
            }
            return false;
        }

        private void cbLoaiXe_TextChanged(object sender, EventArgs e)
        {
            loadCBBX();
        }

        private void loadCBBX()
        {
            cbBaiXe.DataSource = null;
            int maLoaiXe;
            Int32.TryParse(Convert.ToString(cbLoaiXe.SelectedValue), out maLoaiXe);
            List<BaiXe> baiXes = BaiXeDAO.Instence.getListBaiXeNonVe(maLoaiXe);
            if (baiXes.Count() == 0)
            {
                lbThongBao.Visible = true;
                cbBaiXe.Enabled = false;
                txtViTri.Text = "";
                return;
            }
            cbBaiXe.Enabled = true;
            lbThongBao.Visible = false;
            cbBaiXe.DisplayMember = "TenBai";
            cbBaiXe.ValueMember = "MaBai";
            cbBaiXe.DataSource = baiXes;
        }

        private void loadDataGrid()
        {
            gridVeXe.DataSource = null;
            gridVeXe.DataSource = VeXeDAO.Instence.getListVeXeNonBill().OrderByDescending(c=>c.MaVeXe).ToList();
        }
        private void cbBaiXe_SelectedValueChanged(object sender, EventArgs e)
        {
            int maBai;
            Int32.TryParse(Convert.ToString(cbBaiXe.SelectedValue), out maBai);
            BaiXe baiXe = BaiXeDAO.Instence.GetBaiXe(maBai);
            if (baiXe == null)
            {
                return;
            }
            txtViTri.Text = baiXe.ViTri;
        }

        private void btnVaoBai_Click(object sender, EventArgs e)
        {
            if (!CheckInput())
            {
                return;
            }
            if(!xacNhan("Bạn có chắc muốn xe có biển số: "+ txtBienSo.Text + "\n\nVào bãi xe: "+ cbBaiXe.Text+"\n\nCho loại xe : " + cbLoaiXe.Text ))
            {
                return;
            }
            VeXe veXe1 = VeXeDAO.Instence.getListVeXeNonBill().SingleOrDefault(c => c.BienSoXe == txtBienSo.Text);
            if (veXe1 != null)
            {
                xacNhan("Xe đã có trong bãi");
                return;
            }
            VeXe veXe = new VeXe();
            veXe.BaiXe = Convert.ToInt32(cbBaiXe.SelectedValue);
            veXe.BienSoXe = txtBienSo.Text;
            veXe.TenXe = txtTenXe.Text;
            veXe.MauXe = txtMauXe.Text;
            veXe.GioVao = DateTime.Now;
            VeXeDAO.Instence.addVeXe(veXe);
            
            VeXe veXe2 = VeXeDAO.Instence.getListVeXeNonBill().OrderByDescending(c => c.MaVeXe).First();
            MessageBox.Show("Xe vào bãi thành công", "Thông báo", MessageBoxButtons.OK);
            using (VeGuiXe veGuiXe = new VeGuiXe(veXe2.MaVeXe,cbLoaiXe.Text,cbBaiXe.Text,veXe.BienSoXe,veXe.MauXe,veXe.TenXe,veXe.GioVao))
            {
                veGuiXe.ShowDialog();
            }
            loadDataGrid();
            loadCBBX();
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

    }

}
