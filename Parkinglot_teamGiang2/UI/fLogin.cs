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
            Account loginAccount = Login(tenTaiKhoan,matKhau);
            if (loginAccount != null)
            {
                fMainMenu f = new fMainMenu(loginAccount);
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

        private Account Login(string tenTaiKhoan, string matKhau)
        {
            return AccountDAO.Instence.Login(tenTaiKhoan, matKhau);
        }
    }
}
