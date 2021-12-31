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
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tenTaiKhoan = txtUsername.Text;
            string matKhau = txtPassword.Text;
            if (Login(tenTaiKhoan, matKhau))
            {
                Account loginAccount = AccountDAO.Instence.getAccount(tenTaiKhoan);
                fMainMenu f = new fMainMenu();
                this.Hide();
                f.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Sai tên tài khoản hoặc mật khẩu");
                txtUsername.Clear();
                txtPassword.Clear();
                txtUsername.Focus();
            }
        }

        bool Login(string tenTaiKhoan, string matKhau)
        {
            return AccountDAO.Instence.Login(tenTaiKhoan, matKhau);
        }
    }
}
