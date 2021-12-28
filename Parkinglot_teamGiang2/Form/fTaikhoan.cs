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
            Load();
        }

        private void fTaikhoan_Load(object sender, EventArgs e)
        {
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
        void Load()
        {
            dgvTK.DataSource = accountList;
            LoadAccount();
            AddAccountBinding();
        }
        public List<Account> SearchAccountByName(string tenTaiKhoan)
        {

            List<Account> listAccount = AccountDAO.Instence.SearchAccountByName(tenTaiKhoan);

            return listAccount;
        }
        void AddAccountBinding()
        {
            txtTenTK.DataBindings.Add(new Binding("Text", dgvTK.DataSource, "tenTaiKhoan", true, DataSourceUpdateMode.Never));
            txtTenNV.DataBindings.Add(new Binding("Text", dgvTK.DataSource, "tenNhanVien", true, DataSourceUpdateMode.Never));
            
        }

        void LoadAccount()
        {
            accountList.DataSource = AccountDAO.Instence.GetListAccount();
        }

        //
        void AddAccount(string tenTaiKhoan, string tenNhanVien, string matKhau, int vaiTro)
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
        }

        void EditAccount(string tenTaiKhoan, string tenNhanVien, string matKhau, int vaiTro)
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
        }
        //thêm
        private void btnThem_Click(object sender, EventArgs e)
        {
            string tenTaiKhoan = txtTenTK.Text;
            string tenNhanVien = txtTenNV.Text;
            string matKhau = txtMK.Text;
            int vaiTro = (int)cbVaiTro.SelectedIndex;

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
            int vaiTro = (int)cbVaiTro.SelectedIndex;

            EditAccount(tenTaiKhoan, tenNhanVien, matKhau, vaiTro);
            txtMK.Clear();
        }

        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            string tenTaiKhoan = txtTenTK.Text;
            ResetPass(tenTaiKhoan);
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            accountList.DataSource = SearchAccountByName(txtTimKiem.Text);
        }
    }
}
