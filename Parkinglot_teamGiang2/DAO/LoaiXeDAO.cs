using QLBaiGiuXe.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBaiGiuXe.DAO
{
    class LoaiXeDAO
    {
        private static LoaiXeDAO instence;

        public static LoaiXeDAO Instence
        {
            get
            {
                if (instence == null)
                {

                    if (instence == null)
                    {
                        instence = new LoaiXeDAO();
                    }

                }
                return LoaiXeDAO.instence;
            }

            set => instence = value;
        }

        private LoaiXeDAO() { }

        public List<LoaiXe> getListLoaiXe()
        {
            List<LoaiXe> list = new List<LoaiXe>();
            string query = "SELECT maLoaiXe , tenLoaiXe , donGiaH , donGiaNgay FROM LoaiXe";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                LoaiXe cate = new LoaiXe(item);
                list.Add(cate);
            }
            return list;
        }
        public LoaiXe getLoaiXe(int maLoaiXe)
        {
            LoaiXe temp = null;
            string query = "SELECT * FROM LoaiXe WHERE maLoaiXe = " + maLoaiXe;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                temp = new LoaiXe(item);
                return temp;
            }
            return temp;
        }

        public bool addLoaiXe(LoaiXe loaiXe)
        {
            string query = "INSERT INTO LoaiXe ( tenLoaiXe , donGiaH , donGiaNgay ) VALUES ( @tenLoaiXe , @donGiaH , @donGiaNgay )";

            int resulf = DataProvider.Instance.ExecuteNonQuery(query, new object[] { loaiXe.TenLoaiXe, loaiXe.DonGiaH, loaiXe.DonGiaNgay });

            return resulf > 0;
        }

        public bool updateLoaiXe(LoaiXe loaiXe)
        {
            string query = "UPDATE LoaiXe SET tenLoaiXe = @tenLoaiXe , donGiaH = @donGiaH , donGiaNgay = @donGiaNgay WHERE maLoaiXe = @maLoaiXe";

            int resulf = DataProvider.Instance.ExecuteNonQuery(query, new object[] { loaiXe.TenLoaiXe, loaiXe.DonGiaH, loaiXe.DonGiaNgay, loaiXe.MaLoaiXe });

            return resulf > 0;
        }

        public bool deleteLoaiXe(int maLoaiXe)
        { 

            IEnumerable<BaiXe> baiXes = BaiXeDAO.Instence.getListBaiXebyLoaiXe(maLoaiXe);
            foreach (BaiXe item in baiXes)
            {
                BaiXeDAO.Instence.deleteBaiXe(item.MaBai);
            }

            string query = "DELETE LoaiXe Where maLoaiXe = @maLoaiXe";

            int resulf = DataProvider.Instance.ExecuteNonQuery(query,new object[] { maLoaiXe });

            return resulf > 0;
        }
    }
}
