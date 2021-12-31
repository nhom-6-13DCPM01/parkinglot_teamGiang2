using Parkinglot_teamGiang2.UI;
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
            dgvXeRa.DataSource = VeXeDAO.Instence.getListVeXeWithBill();
        }

        private void dgvXeRa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(dgvXeRa.Rows[e.RowIndex].Cells[0].Value);
                VeXe veXe = VeXeDAO.Instence.getVeXe(id);
                PhieuThanhToan phieuThanhToan = PhieuThanhToanDAO.Instence.getPhieu(veXe.MaVeXe);
                BaiXe baiXe = BaiXeDAO.Instence.GetBaiXe(veXe.BaiXe);
                txtBaiXe.Text = baiXe.TenBai;
                txtMaVeXe.Text = Convert.ToString(veXe.MaVeXe);
                txtTenXe.Text = veXe.TenXe;
                txtMauXe.Text = veXe.MauXe;
                txtBienSoXe.Text = veXe.BienSoXe;
                txtGioVao.Text = veXe.GioVao.ToString();
                txtGioRa.Text = phieuThanhToan.GioRa.ToString();
                txtTongTien.Text = phieuThanhToan.TongTien.ToString("#,##") + " đ";
            }
            catch (Exception)
            {

            }


        }

        private void clear()
        {
            txtBaiXe.Text = "";
            txtMaVeXe.Text = "";
            txtTenXe.Text = "";
            txtMauXe.Text = "";
            txtBienSoXe.Text = "";
            txtGioVao.Text = "";
            txtGioRa.Text = "";
            txtTongTien.Text = "";
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTimKiem.Text))
            {
                return;
            }
            DataTable data = VeXeDAO.Instence.getListVeXeWithBill();
            int id;
            if (Int32.TryParse(txtTimKiem.Text, out id))
            {
                DataView view = new DataView(data);  //FROM myDataTable
                view.RowFilter = "maVeXe = " + id;  //WHERE RowNo = 1
                DataTable results = view.ToTable(true);  //SELECT *
                dgvXeRa.DataSource = null;
                dgvXeRa.DataSource = results;
                return;
            }
            DataView view1 = new DataView(data);  //FROM myDataTable
            view1.RowFilter = "bienSoXe LIKE '%" + txtTimKiem.Text + "%'";  //WHERE RowNo = 1
            DataTable results1 = view1.ToTable(true);  //SELECT *
            dgvXeRa.DataSource = null;
            dgvXeRa.DataSource = results1;

        }

        private void reload_Click(object sender, EventArgs e)
        {
            loaddgv();
        }

        private void txtTimKiem_Enter(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "Nhập mã vé hoặc biển số")
            {
                txtTimKiem.Text = "";
                txtTimKiem.ForeColor = Color.Black;
            }
        }

        private void txtTimKiem_Leave(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "")
            {
                txtTimKiem.Text = "Nhập mã vé hoặc biển số";
                txtTimKiem.ForeColor = Color.Gray;
            }
        }

        private void btnXoaVe_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaVeXe.Text))
            {
                MessageBox.Show("Vui lòng chọn vé muốn xóa", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            if (!xacNhan("Bạn có chắc muốn xóa vé có mã: " + txtMaVeXe.Text))
            {
                return;
            }

            VeXeDAO.Instence.deleteVeXe(Convert.ToInt32(txtMaVeXe.Text));
            loaddgv();
            clear();
        }
        public bool xacNhan(string Message)
        {
            if (MessageBox.Show(Message, "EF CRUP Operation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                return true;
            }
            return false;
        }

        private void btnDenBu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaVeXe.Text))
            {
                MessageBox.Show("Vui lòng chọn vé đền bù", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            if (!xacNhan("Bạn có chắc muốn lập phiếu đền bù cho vé có mã : " + txtMaVeXe.Text))
            {
                return;
            }
            int id = Convert.ToInt32(txtMaVeXe.Text);
            using (fSuCo denBu = new fSuCo(id,PhieuThanhToanDAO.Instence.getPhieu(id)))
            {
                denBu.ShowDialog();
            }

            loaddgv();
            clear();
        }


    }


}




