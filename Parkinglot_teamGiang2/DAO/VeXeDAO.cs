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

        public List<VeXe> getListVeXeNonBill()
        {
            List<VeXe> list = new List<VeXe>();
            string query = "SELECT * FROM VeXe AS V Where (SELECT COUNT(*) FROM PhieuThanhToan AS P Where P.idVeXe = V.maVeXe) = 0 ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                VeXe cate = new VeXe(item);
                list.Add(cate);
            }
            return list;
        }

        public DataTable getListVeXeWithBill()
        {
            List<VeXe> list = new List<VeXe>();
            string query = "SELECT C.maVeXe , C.baiXe , C.bienSoXe , C.tenXe , C.mauXe , C.gioVao , U.gioRa , U.tongTien " +
                "FROM (SELECT * FROM VeXe AS V " +
                "Where (SELECT COUNT(*) FROM PhieuThanhToan AS P Where P.idVeXe = V.maVeXe) = 1) AS C,PhieuThanhToan AS U " +
                "WHERE C.maVeXe = idVeXe";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            return data;
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

        public VeXe getVeXe(int maVeXe)
        {
            VeXe temp = null;
            string query = "SELECT * FROM VeXe WHERE maVeXe = " + maVeXe;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                temp = new VeXe(item);
                return temp;
            }
            return temp;
        }
        public bool addVeXe(VeXe veXe)
        {
            string query = "INSERT INTO VeXe ( baiXe , bienSoXe , tenXe , mauXe , gioVao ) VALUES ( @BaiXe , @BienSoXe , @TenXe , @MauXe , @GioVao )";

            int resulf = DataProvider.Instance.ExecuteNonQuery(query, new object[] { veXe.BaiXe, veXe.BienSoXe, veXe.TenXe , veXe.MauXe , veXe.GioVao });

            return resulf > 0;
        }

        public bool updateVeXe(VeXe veXe)
        {
            string query = "UPDATE VeXe SET baiXe = @baiXe , bienSoXe = @bienSoXe , tenXe = @tenXe , mauXe = @mauXe WHERE maVeXe = @maVeXe";

            int resulf = DataProvider.Instance.ExecuteNonQuery(query, new object[] { veXe.BaiXe , veXe.BienSoXe , veXe.TenXe , veXe.MauXe , veXe.MaVeXe });

            return resulf > 0;
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
