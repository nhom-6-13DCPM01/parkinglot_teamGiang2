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
    public partial class fTaikhoan : Form
    {
        BindingSource accountList = new BindingSource();
        
        public fTaikhoan()
        {

            InitializeComponent();
        }
        private void fTaikhoan_Load_1(object sender, EventArgs e)
        {
            LoadData();
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
            label3.ForeColor = ThemeColor.PrimaryColor;
            label4.ForeColor = ThemeColor.PrimaryColor;
        }
        public void LoadData()
        {
            dgvTK.DataSource = null;
            dgvTK.DataSource = AccountDAO.Instence.getListAccount();
            AddAccountBinding();
        }
       
        public void Clear()
        {
            txtTenTK.Text = " ";
            txtTenNV.Text = " ";
            txtMK.Text = " ";

        }
        public List<Account> SearchAccountByName(string tenTaiKhoan)
        {

            List<Account> listAccount = AccountDAO.Instence.SearchAccountByName(tenTaiKhoan);

            return listAccount;
        }
        void LoadAccount()
        {
            accountList.DataSource = AccountDAO.Instence.getListAccount();
        }
        //
        void AddAccount(string tenTaiKhoan, string tenNhanVien, string matKhau, string vaiTro)
        {
            if (AccountDAO.Instence.InsertAccount(tenTaiKhoan, tenNhanVien, matKhau, vaiTro))
            {
                MessageBox.Show("Thêm tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Thêm tài khoản thất bại");
            }
            LoadAccount();
            LoadData();
            Clear();

        }

        void EditAccount(string tenTaiKhoan, string tenNhanVien, string matKhau, string vaiTro)
        {
            if (AccountDAO.Instence.UpdateAccount(tenTaiKhoan, tenNhanVien, matKhau, vaiTro))
            {
                MessageBox.Show("Cập nhật tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Cập nhật tài khoản thất bại");
            }
            LoadAccount();
            LoadData();
            Clear();
        }

        void DeleteAccount(string tenTaiKhoan)
        {

            if (AccountDAO.Instence.DeleteAccount(tenTaiKhoan))
            {
                MessageBox.Show("Xóa tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Xóa tài khoản thất bại");
            }
            LoadAccount();
            LoadData();
            Clear();

        }

        void ResetPass(string tentaiKhoan)
        {
            if (AccountDAO.Instence.ResetPassword(tentaiKhoan))
            {
                MessageBox.Show("Đặt lại mật khẩu thành công");
            }
            else
            {
                MessageBox.Show("Đặt lại mật khẩu thất bại");
            }
            LoadAccount();
            LoadData();
            Clear();
        }
        //thêm
        private void btnThem_Click(object sender, EventArgs e)
        {
            string tenTaiKhoan = txtTenTK.Text;
            string tenNhanVien = txtTenNV.Text;
            string matKhau = txtMK.Text;
            string vaiTro = txtVaiTro.Text;

            AddAccount(tenTaiKhoan, tenNhanVien, matKhau, vaiTro);
            txtMK.Clear();
        }
        //xóa 
        private void btnXoa_Click(object sender, EventArgs e)
        {
            string tenTaiKhoan = txtTenTK.Text;

            DeleteAccount(tenTaiKhoan);
        }
        //sửa
        private void btnSua_Click(object sender, EventArgs e)
        {
            string tenTaiKhoan = txtTenTK.Text;
            string tenNhanVien = txtTenNV.Text;
            string matKhau = txtMK.Text;
            string vaiTro = txtVaiTro.Text;

            EditAccount(tenTaiKhoan, tenNhanVien, matKhau, vaiTro);
            txtMK.Clear();
        }

        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {

        }

        void AddAccountBinding()
        {
            txtTenTK.DataBindings.Add(new Binding("Text", dgvTK.DataSource, "TenTaiKhoan", true, DataSourceUpdateMode.Never));
            txtTenNV.DataBindings.Add(new Binding("Text", dgvTK.DataSource, "TenNhanVien", true, DataSourceUpdateMode.Never));
            txtMK.DataBindings.Add(new Binding("Text", dgvTK.DataSource, "MatKhau", true, DataSourceUpdateMode.Never));
           
        }
    }
}
