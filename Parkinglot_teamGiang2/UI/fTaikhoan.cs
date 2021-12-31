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

            Loadcb();
        }

        public void Clear()
        {
            textBox1.Text = "";
            txtTenTK.Text = "";
            txtTenTK.Text = " ";
            txtTenNV.Text = " ";
            txtMK.Text = " ";
        }
        void Loadcb()
        {
            VaiTro vaiTro1 = new VaiTro("Admin", true);
            VaiTro vaiTro2 = new VaiTro("Nhân Viên", false);
            List<VaiTro> vaiTros = new List<VaiTro>() { vaiTro1, vaiTro2 };
            cbVaiTro.DisplayMember = "tenVaiTro";
            cbVaiTro.ValueMember = "giaTri";
            cbVaiTro.DataSource = vaiTros;
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
        void AddAccount(Account account)
        {
            if (AccountDAO.Instence.InsertAccount(account))
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

        void EditAccount(Account account)
        {
            if (AccountDAO.Instence.UpdateAccount(account))
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

        void DeleteAccount(int id)
        {

            if (AccountDAO.Instence.DeleteAccount(id))
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

        void ResetPass(int id)
        {
            if (AccountDAO.Instence.ResetPassword(id))
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

        private bool checkInput()
        {
            Account accountCheck = AccountDAO.Instence.getListAccount().SingleOrDefault(c => c.TenTaiKhoan == txtTenTK.Text);

            if (accountCheck != null)
            {
                MessageBox.Show("Tên tên khoản bị trùng");
                return false;
            }

            if (string.IsNullOrEmpty(txtTenTK.Text))
            {
                MessageBox.Show("Vui lòng nhập tên tài khoản");
                return false;
            }
            if (string.IsNullOrEmpty(txtMK.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu");
                return false;
            }
            if (string.IsNullOrEmpty(txtTenNV.Text))
            {
                MessageBox.Show("Vui lòng nhập tên nhân viên");
                return false;
            }
            return true;
        }
        //thêm
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!checkInput())
            {
                return;
            }
            if (MessageBox.Show("Bạn có chắc muốn thêm tài khoản", "Thông báo", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                return;
            }
            Account account = new Account();
            account.TenTaiKhoan = txtTenTK.Text;
            account.TenNhanVien = txtTenNV.Text;
            account.MatKhau = txtMK.Text;
            account.VaiTro = Convert.ToBoolean(cbVaiTro.SelectedValue);

            AddAccount(account);

        }
        //xóa 
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Vui lòng chọn tài khoản ", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            if (Convert.ToInt32(textBox1.Text) == 1)
            {
                MessageBox.Show("Bạn không thể xóa tài khoản này", "Thông báo");
                return;
            }
            if (MessageBox.Show("Bạn có chắc muôn xóa tài khoản", "Thông báo", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                return;
            }
            int id = Convert.ToInt32(textBox1.Text);

            DeleteAccount(id);
        }
        //sửa
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Vui lòng chọn tài khoản ", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            if (MessageBox.Show("Bạn có chắc muôn sửa tài khoản", "Thông báo", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                return;
            }
            Account account = AccountDAO.Instence.getAccount(Convert.ToInt32(textBox1.Text));
            account.TenTaiKhoan = txtTenTK.Text;
            account.TenNhanVien = txtTenNV.Text;
            account.MatKhau = txtMK.Text;
            account.VaiTro = Convert.ToBoolean(cbVaiTro.SelectedValue);

            EditAccount(account);
            txtMK.Clear();
        }

        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            Clear();
            LoadData();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTimKiem.Text))
            {
                return;
            }
            int key;
            if(Int32.TryParse(txtTimKiem.Text, out key))
            {
                dgvTK.DataSource = null;
                dgvTK.DataSource = AccountDAO.Instence.getListAccount().Where(c=>c.Id == key).ToList();
                return;
            }
            dgvTK.DataSource = null;
            dgvTK.DataSource = AccountDAO.Instence.getListAccount().Where(c => c.TenNhanVien.Contains(txtTimKiem.Text)).ToList();
        }

        void AddAccountBinding()
        {
            textBox1.DataBindings.Add(new Binding("Text", dgvTK.DataSource, "Id", true, DataSourceUpdateMode.Never));
            txtTenTK.DataBindings.Add(new Binding("Text", dgvTK.DataSource, "TenTaiKhoan", true, DataSourceUpdateMode.Never));
            txtTenNV.DataBindings.Add(new Binding("Text", dgvTK.DataSource, "TenNhanVien", true, DataSourceUpdateMode.Never));
            txtMK.DataBindings.Add(new Binding("Text", dgvTK.DataSource, "MatKhau", true, DataSourceUpdateMode.Never));

        }
        private class VaiTro
        {
            public string tenVaiTro { get; set; }
            public bool giaTri { get; set; }

            public VaiTro(string tenVaiTro, bool giaTri)
            {
                this.tenVaiTro = tenVaiTro;
                this.giaTri = giaTri;
            }
        }

        private void dgvTK_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Account account = AccountDAO.Instence.getAccount(Convert.ToInt32(dgvTK.Rows[e.RowIndex].Cells[4].Value));
                textBox1.Text = account.Id.ToString();
                txtTenTK.Text = account.TenTaiKhoan.ToString();
                txtTenNV.Text = Convert.ToString(account.TenNhanVien);
                txtMK.Text = Convert.ToString(account.MatKhau);
                cbVaiTro.SelectedValue = account.VaiTro;
            }
            catch (Exception)
            {

            }
        }

        private void txtTimKiem_Enter(object sender, EventArgs e)
        {
            if(txtTimKiem.Text == "Nhập tên nhân viên hoặc id")
            {
                txtTimKiem.Text = "";
                txtTimKiem.ForeColor = Color.Black;
            }
        }

        private void txtTimKiem_Leave(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "")
            {
                txtTimKiem.Text = "Nhập tên nhân viên hoặc id";
                txtTimKiem.ForeColor = Color.Gray;
            }
        }
    }
}
