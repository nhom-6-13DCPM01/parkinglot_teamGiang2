using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBaiGiuXe.Model
{
    public class BaiXe
    {
        private int maBai;
        private string tenBai;
        private string viTri;
        private int loaiXe;

        public BaiXe()
        {
        }

        public BaiXe(DataRow row)
        {
            this.MaBai = Convert.ToInt32(row["maBai"]);
            this.tenBai = row["tenBai"].ToString();
            this.viTri = row["viTri"].ToString();
            this.loaiXe = Convert.ToInt32(row["loaiXe"]);
        }
        public int MaBai { get => maBai; set => maBai = value; }
        public string TenBai { get => tenBai; set => tenBai = value; }
        public string ViTri { get => viTri; set => viTri = value; }
        public int LoaiXe { get => loaiXe; set => loaiXe = value; }
        
    }
}
