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
            label2.ForeColor = ThemeColor.SecondaryColor;
            label3.ForeColor = ThemeColor.PrimaryColor;
            label4.ForeColor = ThemeColor.SecondaryColor;
            label5.ForeColor = ThemeColor.SecondaryColor;
            label6.ForeColor = ThemeColor.SecondaryColor;
            label7.ForeColor = ThemeColor.SecondaryColor;
            label8.ForeColor = ThemeColor.PrimaryColor;

        }
        
        public void loadData()
        {
            gridVeXe.DataSource = null;
            gridVeXe.DataSource = VeXeDAO.Instence.getListVeXeNonBill();

            cbLoaiXe.DataSource = LoaiXeDAO.Instence.getListLoaiXe();
            cbLoaiXe.DisplayMember = "TenLoaiXe";
            cbLoaiXe.ValueMember = "MaLoaiXe";


        }

        private void cbLoaiXe_TextChanged(object sender, EventArgs e)
        {
            
            cbBaiXe.DataSource = null;
            int maLoaiXe;
            Int32.TryParse(cbLoaiXe.SelectedValue.ToString(), out maLoaiXe);
            if (maLoaiXe == 0)
            {
                maLoaiXe = (from c in LoaiXeDAO.Instence.getListLoaiXe() select c.MaLoaiXe).First();
            }
            List<BaiXe> baiXes = BaiXeDAO.Instence.getListBaiXeNonVe(maLoaiXe) ;
            if (baiXes.Count()==0)
            {
                cbBaiXe.Enabled = false;
                cbBaiXe.Text = "Hết Bãi";
                return;
            }
            cbBaiXe.Enabled = true;
            cbBaiXe.DataSource = baiXes;
            cbBaiXe.DisplayMember = "TenBai";
            cbBaiXe.ValueMember = "MaBai";
        }

        private void cbBaiXe_TextChanged(object sender, EventArgs e)
        {
            int maBai;
            Int32.TryParse(cbBaiXe.SelectedValue.ToString(), out maBai);
            BaiXe baiXe = BaiXeDAO.Instence.GetBaiXe(maBai);
            txtViTri.Text = baiXe.ViTri;
        }
    }

}
