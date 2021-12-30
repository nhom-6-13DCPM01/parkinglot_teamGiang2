using Parkinglot_teamGiang2.ReportViewer;
using QLBaiGiuXe.DAO;
using QLBaiGiuXe.Model;
using System;
using Parkinglot_teamGiang2.UI;
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
            gridVeXe.DataSource = VeXeDAO.Instence.getListVeXeNonBill().OrderByDescending(c => c.MaVeXe).ToList();
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
            if (!xacNhan("Bạn có chắc muốn xe có biển số: " + txtBienSo.Text + "\n\nVào bãi xe: " + cbBaiXe.Text + "\n\nCho loại xe : " + cbLoaiXe.Text))
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
            using (VeGuiXe veGuiXe = new VeGuiXe(veXe2.MaVeXe, cbLoaiXe.Text, cbBaiXe.Text, veXe.BienSoXe, veXe.MauXe, veXe.TenXe, veXe.GioVao))
            {
                veGuiXe.ShowDialog();
            }
            loadDataGrid();
            loadCBBX();
            clearTxt();
        }
        private void clearTxt()
        {
             txtBienSo.Text="";
             txtTenXe.Text="";
             txtMauXe.Text="";
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
        private void loadLabel(bool visible)
        {
            List<Label> labels = new List<Label>() { lbBienSo, lbTen, lbMau, lbLoai, lbBai, lbNgay };
            foreach (Label item in labels)
            {
                item.Visible = visible;
            }
            if(visible == false)
            {
                textBox1.Text = "";
            }
            
        }
        private void gridVeXe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(gridVeXe.Rows[e.RowIndex].Cells[0].Value);
                VeXe veXe = VeXeDAO.Instence.getVeXe(id);
                BaiXe baiXe = BaiXeDAO.Instence.GetBaiXe(veXe.BaiXe);
                textBox1.Text = veXe.MaVeXe.ToString();
                lbBienSo.Text = veXe.BienSoXe.ToString();
                lbTen.Text = Convert.ToString(veXe.TenXe);
                lbMau.Text = Convert.ToString(veXe.MauXe);
                lbNgay.Text = veXe.GioVao.ToString();

                lbBai.Text = baiXe.TenBai;
                lbLoai.Text = LoaiXeDAO.Instence.getLoaiXe(baiXe.LoaiXe).TenLoaiXe;
                loadLabel(true);
            }
            catch (Exception)
            {

            }

        }
        
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTimKiem.Text))
            {
                return;
            }
            List<VeXe> veXes = VeXeDAO.Instence.getListVeXeNonBill();
            if (radioButton1.Checked == true)
            {
                int id;
                if (Int32.TryParse(txtTimKiem.Text,out id))
                {
                    gridVeXe.DataSource = null;
                    
                    gridVeXe.DataSource = veXes.Where(c => c.MaVeXe == id ).ToList();
                }
                return;
            }
            gridVeXe.DataSource = null;
            gridVeXe.DataSource = veXes.Where(c => c.BienSoXe.Contains(txtTimKiem.Text)).ToList();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            loadDataGrid();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Vui lòng chọn vé muốn sửa", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            VeXe veXe = VeXeDAO.Instence.getVeXe(Convert.ToInt32(textBox1.Text));
            using (fSuaVe fSuaVe = new fSuaVe(veXe))
            {
                fSuaVe.ShowDialog();
            }
            BaiXe baiXe = BaiXeDAO.Instence.GetBaiXe(veXe.BaiXe);
            textBox1.Text = veXe.MaVeXe.ToString();
            lbBienSo.Text = veXe.BienSoXe.ToString();
            lbTen.Text = Convert.ToString(veXe.TenXe);
            lbMau.Text = Convert.ToString(veXe.MauXe);
            lbNgay.Text = veXe.GioVao.ToString();

            lbBai.Text = baiXe.TenBai;
            lbLoai.Text = LoaiXeDAO.Instence.getLoaiXe(baiXe.LoaiXe).TenLoaiXe;
            loadLabel(true);
            loadDataGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Vui lòng chọn vé muốn thanh toán", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            if (!xacNhan("Bạn có chắc muốn thanh toán vé có mã: "+textBox1.Text))
            {
                return;
            }
            VeXe veXe = VeXeDAO.Instence.getVeXe(Convert.ToInt32(textBox1.Text));
            BaiXe baiXe = BaiXeDAO.Instence.GetBaiXe(veXe.BaiXe);
            LoaiXe loaiXe = LoaiXeDAO.Instence.getLoaiXe(baiXe.LoaiXe);
            PhieuThanhToan phieuThanhToan = new PhieuThanhToan();
            phieuThanhToan.IdVeXe = veXe.MaVeXe;
            phieuThanhToan.GioRa = DateTime.Now;
            TimeSpan time =  phieuThanhToan.GioRa - veXe.GioVao ;
            if(time.Days == 0)
            {
                if(time.Hours == 0)
                {
                    phieuThanhToan.TongTien = loaiXe.DonGiaH;
                }
                else
                {
                    phieuThanhToan.TongTien = loaiXe.DonGiaH * time.Hours;
                }
            }
            else
            {
                phieuThanhToan.TongTien = loaiXe.DonGiaNgay * time.Days;
            }
            PhieuThanhToanDAO.Instence.addPhieuThanhToan(phieuThanhToan);
            using(ThanhToan thanhToan = new ThanhToan(veXe, phieuThanhToan, lbLoai.Text, lbBai.Text))
            {
                thanhToan.ShowDialog();
            }
            loadLabel(false);
            loadData();
            loadDataGrid();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Vui lòng chọn vé muốn xóa", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            if (!xacNhan("Bạn có chắc muốn xóa vé có mã: " + textBox1.Text))
            {
                return;
            }

            VeXeDAO.Instence.deleteVeXe(Convert.ToInt32(textBox1.Text));
            loadLabel(false);
            loadDataGrid();
            loadData();
        }

        private void btnDenBu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Vui lòng chọn vé đền bù", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            if (!xacNhan("Bạn có chắc muốn lập phiếu đền bù cho vé có mã : " + textBox1.Text))
            {
                return;
            }
            using (fSuCo denBu = new fSuCo(Convert.ToInt32(textBox1.Text)))
            {
                denBu.ShowDialog();
            }
            
            loadLabel(false);
            loadDataGrid();
            loadData();
        }
    }

}
