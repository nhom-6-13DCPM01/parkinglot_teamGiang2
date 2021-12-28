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
    public partial class fQLBX : Form
    {
        public fQLBX()
        {
            InitializeComponent();
        }

        private void fQLBX_Load(object sender, EventArgs e)
        {
            loadData();
            LoadTheme();
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
            label4.ForeColor = ThemeColor.SecondaryColor;
            label5.ForeColor = ThemeColor.PrimaryColor;
            label6.ForeColor = ThemeColor.PrimaryColor;

        }
        /// <summary>
        /// CODE XỬ LÝ CỦA LOẠI XE
        /// </summary>
        private void loadData()
        {
            gridLoaiXe.DataSource = null;
            gridLoaiXe.DataSource = LoaiXeDAO.Instence.getListLoaiXe();
        }

        private void clearData()
        {
            txtMaLoaiXe.Text = "";
            txtTenLoaiXe.Text = "";
            txtPriceH.Text = "";
            txtPriceNgay.Text = "";
        }

        bool CheckInput()
        {
            Decimal result;
            string priceH = txtPriceH.Text;
            string priceNgay = txtPriceNgay.Text;
            if (txtTenLoaiXe.Text == "")
            {
                MessageBox.Show("Hãy nhập đúng tên, please", "Notification");
                txtTenLoaiXe.Focus();
                return false;
            }

            //SL ko được nhập chữ
            if (!(Decimal.TryParse(priceH, out result)))
            {
                MessageBox.Show("Hãy nhập đúng định dạng tiền theo Giờ", "Notification");
                txtPriceH.Focus();
                return false;
            }

            if (!(Decimal.TryParse(priceNgay, out result)))
            {
                MessageBox.Show("Hãy nhập đúng định dạng tiền theo Ngày", "Notification");
                txtPriceNgay.Focus();
                return false;
            }
            return true;
        }
        public bool xacNhan(string Message)
        {
            if (MessageBox.Show(Message, "EF CRUP Operation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                return true;
            }
            return false;
        }
        private void btnAddLoai_Click(object sender, EventArgs e)
        {
            if (!CheckInput())
            {
                return;
            }

            if (xacNhan("Bạn có muôn thêm ?") == false)
            {
                return;
            }

            LoaiXe loaiXe = new LoaiXe();
            loaiXe.TenLoaiXe = txtTenLoaiXe.Text;
            loaiXe.DonGiaH = Convert.ToDecimal(txtPriceH.Text);
            loaiXe.DonGiaNgay = Convert.ToDecimal(txtPriceNgay.Text);

            LoaiXeDAO.Instence.addLoaiXe(loaiXe);

            loadData();
            clearData();
            loadDataBaiXe();
            clearDataBaiXe();
        }

        private void gridLoaiXe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(gridLoaiXe.Rows[e.RowIndex].Cells[0].Value);
                LoaiXe loaiXe = LoaiXeDAO.Instence.getLoaiXe(id);
                txtMaLoaiXe.Text = Convert.ToString(loaiXe.MaLoaiXe);
                txtTenLoaiXe.Text = loaiXe.TenLoaiXe;
                txtPriceH.Text = Convert.ToString(loaiXe.DonGiaH);
                txtPriceNgay.Text = Convert.ToString(loaiXe.DonGiaNgay);
                loadDataBaiXe();
                clearDataBaiXe();
            }
            catch (Exception)
            {

            }

        }

        private void btnSuaLoai_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Convert.ToString(txtMaLoaiXe.Text)))
            {
                return;
            }
            int id = Convert.ToInt32(txtMaLoaiXe.Text);
            if (!CheckInput())
            {
                return;
            }
            if (!xacNhan("Bạn có chắc muôn sửa Loại xe có mã = " + id))
            {
                return;
            }
            LoaiXe loaiXe = LoaiXeDAO.Instence.getLoaiXe(id);
            loaiXe.TenLoaiXe = txtTenLoaiXe.Text;
            loaiXe.DonGiaH = Convert.ToDecimal(txtPriceH.Text);
            loaiXe.DonGiaNgay = Convert.ToDecimal(txtPriceNgay.Text);
            LoaiXeDAO.Instence.updateLoaiXe(loaiXe);
            loadData();
            clearData();
            loadDataBaiXe();
            clearDataBaiXe();
        }

        private void btnXoaLoai_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Convert.ToString(txtMaLoaiXe.Text)))
            {
                return;
            }
            int id = Convert.ToInt32(txtMaLoaiXe.Text);
            if (!xacNhan("Bạn có chắc muôn Xóa Loại xe có mã = " + id))
            {
                return;
            }

            LoaiXeDAO.Instence.deleteLoaiXe(id);
            loadData();
            clearData();
            loadDataBaiXe();
            clearDataBaiXe();
        }

        /// <summary>
        /// CODE XỬ LÝ CỦA BÃI XE
        /// </summary>
        /// 
        private void loadDataBaiXe()
        {
            if (string.IsNullOrEmpty(Convert.ToString(txtMaLoaiXe.Text)))
            {
                gridBaiXe.DataSource = null;

                lbLoaiXe.Text = "Tên Loại";
                lbSLBai.Text = "0";
                return;
            }
            int id = Convert.ToInt32(txtMaLoaiXe.Text);
            gridBaiXe.DataSource = null;
            List<BaiXe> listBaiXe = BaiXeDAO.Instence.getListBaiXebyLoaiXe(id);
            gridBaiXe.DataSource = listBaiXe;
            lbLoaiXe.Text = txtTenLoaiXe.Text;
            lbSLBai.Text = Convert.ToString(listBaiXe.Count());
        }

        private void clearDataBaiXe()
        {
            txtmaBai.Text = "";
            txtTenBai.Text = "";
            txtVitri.Text = "";
        }

        bool CheckInputBaiXe()
        {
            if (string.IsNullOrEmpty(txtMaLoaiXe.Text))
            {
                return false;
            }

            if (string.IsNullOrEmpty(txtTenBai.Text))
            {
                MessageBox.Show("Hãy nhập tên bãi, please", "Notification");
                txtTenBai.Focus();
                return false;
            }

            return true;
        }
        private void btnAddBai_Click(object sender, EventArgs e)
        {
            if (!CheckInputBaiXe())
            {
                return;
            }

            if (!xacNhan("Bạn có muôn thêm bãi xe ?"))
            {
                return;
            }

            BaiXe baiXe = new BaiXe();
            baiXe.TenBai = txtTenBai.Text;
            baiXe.ViTri = txtVitri.Text;
            baiXe.LoaiXe = Convert.ToInt32(txtMaLoaiXe.Text);
            BaiXeDAO.Instence.addBaiXe(baiXe);

            loadDataBaiXe();
            clearDataBaiXe();
        }

        private void gridBaiXe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(gridBaiXe.Rows[e.RowIndex].Cells[0].Value);
                BaiXe baiXe = BaiXeDAO.Instence.GetBaiXe(id);
                txtmaBai.Text = Convert.ToString(baiXe.MaBai);
                txtTenBai.Text = baiXe.TenBai;
                txtVitri.Text = baiXe.ViTri;
            }
            catch (Exception)
            {

            }

        }

        private void btnSuaBai_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Convert.ToString(txtmaBai.Text)))
            {
                return;
            }
            int id = Convert.ToInt32(txtmaBai.Text);
            if (!CheckInputBaiXe())
            {
                return;
            }
            if (!xacNhan("Bạn có chắc muôn sửa Bãi xe có mã = " + id))
            {
                return;
            }
            BaiXe baiXe = BaiXeDAO.Instence.GetBaiXe(id);
            baiXe.TenBai = txtTenBai.Text;
            baiXe.ViTri = txtVitri.Text;

            BaiXeDAO.Instence.updateBaiXe(baiXe);

            loadDataBaiXe();
            clearDataBaiXe();
        }

        private void btnXoaBai_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Convert.ToString(txtmaBai.Text)))
            {
                return;
            }
            int id = Convert.ToInt32(txtmaBai.Text);
            if (!xacNhan("Bạn có chắc muốn Xóa Bãi xe có mã = " + id))
            {
                return;
            }

            BaiXeDAO.Instence.deleteBaiXe(id);
            loadDataBaiXe();
            clearDataBaiXe();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                loadDataBaiXe();
                return;
            }
            if (string.IsNullOrEmpty(Convert.ToString(txtMaLoaiXe.Text)))
            {
                gridBaiXe.DataSource = null;
                lbLoaiXe.Text = "Tên Loại";
                lbSLBai.Text = "0";
                return;
            }

            int id ;
            
            List<BaiXe> listBaiXe = BaiXeDAO.Instence.getListBaiXebyLoaiXe(Convert.ToInt32(txtMaLoaiXe.Text));
            if (Int32.TryParse(txtSearch.Text, out id))
            {
                gridBaiXe.DataSource = null;
                gridBaiXe.DataSource = (from c in listBaiXe where c.MaBai == id select c).ToList();
                return;
            }
            gridBaiXe.DataSource = null;
            gridBaiXe.DataSource = (from c in listBaiXe where c.TenBai.Contains(txtSearch.Text) select c).ToList();
        }

        private void btnLamTuoi_Click(object sender, EventArgs e)
        {
            loadDataBaiXe();
            clearDataBaiXe();
        }
    }
}
