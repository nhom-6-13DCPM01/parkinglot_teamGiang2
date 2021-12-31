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

        public static object Instance { get; internal set; }

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
        
        public Account getAccount(string tenTaiKhoan)
        {
            string query = "SELECT * FROM Account Where tenTaiKhoan = " + tenTaiKhoan;
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

        public DataTable GetListAccount()
        {
            return DataProvider.Instance.ExecuteQuery("SELECT tenTaiKhoan, tenNhanVien, vaiTro FROM dbo.Account");
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
        public bool InsertAccount(string tenTaiKhoan, string tenNhanVien, string matKhau, bool vaiTro)
        {
            string query = string.Format("INSERT dbo.Account ( tenTaiKhoan, tenNhanVien, matKhau ,vaiTro )VALUES  ( N'{0}', N'{1}','{2}' ,{3})", tenTaiKhoan, tenNhanVien, matKhau, vaiTro);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool UpdateAccount(string tenTaiKhoan, string tenNhanVien, string matkhau, bool vaiTro)
        {
            string query = string.Format("UPDATE dbo.Account SET tenNhanVien = N'{1}', vaiTro = {2} , matKhau = '{3}' WHERE tenTaiKhoan = N'{0}'", tenTaiKhoan, tenNhanVien, vaiTro, matkhau);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        //
        public bool DeleteAccount(string tenTaiKhoan)
        {
            string query = string.Format("Delete Account where tenTaiKhoan = N'{0}'", tenTaiKhoan);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool ResetPassword(string tenTaiKhoan)
        {
            string query = string.Format("update account set matKhau = N'0' where tenTaiKhoan = N'{0}'", tenTaiKhoan);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

    }
}
