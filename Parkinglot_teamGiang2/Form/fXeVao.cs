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
            label1.ForeColor = ThemeColor.SecondaryColor;
            
            label3.ForeColor = ThemeColor.SecondaryColor;
            label4.ForeColor = ThemeColor.SecondaryColor;
            label5.ForeColor = ThemeColor.PrimaryColor;
            label6.ForeColor = ThemeColor.PrimaryColor;
            label7.ForeColor = ThemeColor.PrimaryColor;
 

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
            
        }
    }

}
