using QLBaiGiuXe.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBaiGiuXe.DAO
{
    class PhieuThanhToanDAO
    {
        private static PhieuThanhToanDAO instence;

        public static PhieuThanhToanDAO Instence
        {
            get
            {
                if (instence == null)
                {

                    if (instence == null)
                    {
                        instence = new PhieuThanhToanDAO();
                    }

                }
                return PhieuThanhToanDAO.instence;
            }

            set => instence = value;
        }

        private PhieuThanhToanDAO() { }
        public PhieuThanhToan getPhieu(int maVeXe)
        {
            PhieuThanhToan cate=null;
            string query = "SELECT * FROM PhieuThanhToan WHERE idVeXe ="+maVeXe;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                cate = new PhieuThanhToan(item);
                return cate;
            }
            return cate;
        }
        public List<PhieuThanhToan> getListPhieuThanhToan()
        {
            List<PhieuThanhToan> list = new List<PhieuThanhToan>();
            string query = "SELECT * FROM PhieuThanhToan";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                PhieuThanhToan cate = new PhieuThanhToan(item);
                list.Add(cate);
            }
            return list;
        }
        public bool addPhieuThanhToan(PhieuThanhToan phieuThanhToan)
        {
            string query = "INSERT INTO PhieuThanhToan ( idVeXe , gioRa , tongTien ) VALUES ( @idVeXe , @gioRa , @tongTien )";

            int resulf = DataProvider.Instance.ExecuteNonQuery(query, new object[] { phieuThanhToan.IdVeXe , phieuThanhToan.GioRa , phieuThanhToan.TongTien });

            return resulf > 0;
        }

        public bool deletePhieuThanhToan(int idVeXe)
        {
            string query = "DELETE PhieuThanhToan Where idVeXe = @idVeXe";

            int resulf = DataProvider.Instance.ExecuteNonQuery(query, new object[] { idVeXe });

            return resulf > 0;
        }
    }
}
