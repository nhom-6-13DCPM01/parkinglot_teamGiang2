using QLBaiGiuXe.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBaiGiuXe.DAO
{
    class BaiXeDAO
    {
        private static BaiXeDAO instence;

        public static BaiXeDAO Instence
        {
            get
            {
                if (instence == null)
                {

                    if (instence == null)
                    {
                        instence = new BaiXeDAO();
                    }

                }
                return BaiXeDAO.instence;
            }

            set => instence = value;
        }

        private BaiXeDAO() { }

        public List<BaiXe> getListBaiXe()
        {
            List<BaiXe> list = new List<BaiXe>();
            string query = "SELECT * FROM BaiXe";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                BaiXe cate = new BaiXe(item);
                list.Add(cate);
            }
            return list;
        }

        public List<BaiXe> getListBaiXebyLoaiXe(int loaiXe)
        {
            List<BaiXe> list = new List<BaiXe>();
            string query = "SELECT * FROM BaiXe Where loaiXe = "+ loaiXe;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                BaiXe cate = new BaiXe(item);
                list.Add(cate);
            }
            return list;
        }

        public BaiXe GetBaiXe(int maBai)
        {
            BaiXe temp = null;
            string query = "SELECT * FROM BaiXe WHERE maBai = " + maBai;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                temp = new BaiXe(item);
                return temp;
            }
            return temp;
        }

        public bool addBaiXe(BaiXe baiXe)
        {
            string query = "INSERT INTO BaiXe ( tenBai , viTri , loaiXe ) VALUES ( @tenBai , @viTri , @loaiXe )";

            int resulf = DataProvider.Instance.ExecuteNonQuery(query, new object[] { baiXe.TenBai , baiXe.ViTri , baiXe.LoaiXe });

            return resulf > 0;
        }

        public bool updateBaiXe(BaiXe baiXe)
        {
            string query = "UPDATE baiXe SET tenBai = @tenBai , viTri = @viTri  WHERE maBai = @maBai";

            int resulf = DataProvider.Instance.ExecuteNonQuery(query, new object[] { baiXe.TenBai , baiXe.ViTri , baiXe.MaBai });

            return resulf > 0;
        }

        public bool deleteBaiXe(int maBai)
        {
            IEnumerable<VeXe> veXes = VeXeDAO.Instence.getListVeXebyBaiXe(maBai);
            foreach (VeXe item in veXes)
            {
                VeXeDAO.Instence.deleteVeXe(item.MaVeXe);
            }
            string query = "DELETE BaiXe Where maBai = @maBai";

            int resulf = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maBai });

            return resulf > 0;
        }
    }
}

