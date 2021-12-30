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

namespace QLBaiGiuXe
{
    public partial class fXeRa : Form
    {
        public fXeRa()
        {
            InitializeComponent();
        }

        private void fXeRa_Load(object sender, EventArgs e)
        {
            LoadTheme();
            loaddgv();
          
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
            label2.ForeColor = ThemeColor.SecondaryColor;
            label3.ForeColor = ThemeColor.SecondaryColor;
            label4.ForeColor = ThemeColor.PrimaryColor;
            label5.ForeColor = ThemeColor.PrimaryColor;
            label6.ForeColor = ThemeColor.PrimaryColor;

        }

        private void loaddgv()
        {
            dgvXeRa.DataSource = null;
            dgvXeRa.DataSource = PhieuThanhToanDAO.Instence.getListPhieuThanhToan();
            dgvXeRa.DataSource = VeXeDAO.Instence.getListVeXe();
           
        }

        private void dgvXeRa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = Convert.ToInt32(dgvXeRa.Rows[e.RowIndex].Cells[0].Value);
            VeXe veXe = VeXeDAO.Instence.getVeXe(id);  
            txtMaVeXe.Text = Convert.ToString(veXe.MaVeXe);
            txtTenXe.Text = veXe.TenXe;
            txtMauXe.Text = veXe.MauXe;
            txtBienSoXe.Text = veXe.BienSoXe;
            dateTimePickerGioVao.Value = veXe.GioVao;
        }
      

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTimKiem.Text))
            {
                return;
            }
            List<VeXe> veXes = VeXeDAO.Instence.getListVeXe();
            
            int id;
            if (Int32.TryParse(txtTimKiem.Text, out id))
            {
                dgvXeRa.DataSource = null;

                dgvXeRa.DataSource = veXes.Where(c => c.MaVeXe == id).ToList();
            }
            dgvXeRa.DataSource = null;
            dgvXeRa.DataSource = veXes.Where(c => c.BienSoXe.Contains(txtTimKiem.Text)).ToList();
           

        }

        private void reload_Click(object sender, EventArgs e)
        {
            loaddgv();
        }
    }
}
