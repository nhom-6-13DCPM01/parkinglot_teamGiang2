using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBaiGiuXe.Model
{
    public class VeXe
    {
        private int maVeXe;
        private int baiXe;
        private string bienSoXe;
        private string tenXe;
        private string mauXe;
        private DateTime gioVao;
        public VeXe(DataRow row)
        {
            this.maVeXe = Convert.ToInt32(row["maVeXe"]);
            this.baiXe = Convert.ToInt32(row["baiXe"]);
            this.bienSoXe = Convert.ToString(row["bienSoXe"]);
            this.tenXe = Convert.ToString(row["tenXe"]);
            this.mauXe = Convert.ToString(row["mauXe"]);
            this.gioVao = Convert.ToDateTime(row["gioVao"]);
        }
        public int MaVeXe { get => maVeXe; set => maVeXe = value; }
        public int BaiXe { get => baiXe; set => baiXe = value; }
        public string BienSoXe { get => bienSoXe; set => bienSoXe = value; }
        public string TenXe { get => tenXe; set => tenXe = value; }
        public string MauXe { get => mauXe; set => mauXe = value; }
        public DateTime GioVao { get => gioVao; set => gioVao = value; }
    }
}
