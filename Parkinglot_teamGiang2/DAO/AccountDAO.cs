using QLBaiGiuXe.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBaiGiuXe.DAO
{
    class AccountDAO
    {
        private static AccountDAO instence;

        public static AccountDAO Instence
        {
            get
            {
                if (instence == null)
                {

                    if (instence == null)
                    {
                        instence = new AccountDAO();
                    }

                }
                return AccountDAO.instence;
            }

            set => instence = value;
        }


        private AccountDAO() { }

        public List<Account> getListAccount()
        {
            List<Account> list = new List<Account>();
            string query = "SELECT * FROM Account";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Account cate = new Account(item);
                list.Add(cate);
            }
            return list;
        }
        
        public Account getAccount(int id)
        {
            string query = "SELECT * FROM Account Where id = " + id;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Account cate = new Account(item);
                return cate;
            }
            return null;
        }

        public Account Login(string tenTaiKhoan, string matKhau)
        {
            string query = "USP_Login @userName , @passWord ";
            Account account = null;
            DataTable data = DataProvider.Instance.ExecuteQuery(query , new object[] { tenTaiKhoan , matKhau });
            foreach (DataRow item in data.Rows)
            {
                account = new Account(item);
                return account;
            }
            return account;
        }


        public List<Account> SearchAccountByName(string tenTaiKhoan)
        {
            List<Account> list = new List<Account>();

            string query = string.Format("SELECT * FROM Account WHERE dbo.fuConvertToUnsign1(tenTaiKhoan) LIKE N'%' + dbo.fuConvertToUnsign1(N'{0}') + '%'", tenTaiKhoan);

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Account account = new Account(item);
                list.Add(account);
            }

            return list;
        }
        public bool InsertAccount(Account account)
        {
            string query = "INSERT dbo.Account ( tenTaiKhoan, tenNhanVien, matKhau ,vaiTro )VALUES  ( @tenTaiKhoan , @tenNhanVien , @matKhau , @vaiTro )";
            int result = DataProvider.Instance.ExecuteNonQuery(query , new object[] {account.TenTaiKhoan,account.TenNhanVien,account.MatKhau,account.VaiTro});

            return result > 0;
        }

        public bool UpdateAccount(Account account)
        {
            string query = "UPDATE Account SET tenTaiKhoan = @tenTaiKhoan , tenNhanVien = @tenNhanVien , vaiTro = @vaiTro , matKhau = @matKhau WHERE id = @id ";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { account.TenTaiKhoan, account.TenNhanVien, account.VaiTro , account.MatKhau,  account.Id });

            return result > 0;
        }
        //
        public bool DeleteAccount(int id)
        {
            string query = "Delete Account where id = "+ id;
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool ResetPassword(int id)
        {
            string query = string.Format("update account set matKhau = N'0' where id = {0}", id);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

    }
}
