using QLBaiGiuXe.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBaiGiuXe.DAO
{
    class VeXeDAO
    {
        private static VeXeDAO instence;

        public static VeXeDAO Instence
        {
            get
            {
                if (instence == null)
                {

                    if (instence == null)
                    {
                        instence = new VeXeDAO();
                    }

                }
                return VeXeDAO.instence;
            }

            set => instence = value;
        }

        private VeXeDAO() { }

        public List<VeXe> getListVeXe()
        {
            List<VeXe> list = new List<VeXe>();
            string query = "SELECT * FROM VeXe";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                VeXe cate = new VeXe(item);
                list.Add(cate);
            }
            return list;
        }
        public List<VeXe> getListVeXebyBaiXe(int baiXe)
        {
            List<VeXe> list = new List<VeXe>();
            string query = "SELECT * FROM VeXe Where baiXe = "+baiXe;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                VeXe cate = new VeXe(item);
                list.Add(cate);
            }
            return list;
        }

        public bool deleteVeXe(int maVeXe)
        {
            PhieuThanhToanDAO.Instence.deletePhieuThanhToan(maVeXe);

            string query = "DELETE VeXe Where maVeXe = @maVeXe";

            int resulf = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maVeXe });

            return resulf > 0;
        }
    }
}
