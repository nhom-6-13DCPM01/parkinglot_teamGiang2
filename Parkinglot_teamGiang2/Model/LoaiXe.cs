using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBaiGiuXe.Model
{
    public class LoaiXe
    {
        
        private int maLoaiXe;
        private string tenLoaiXe;
        private decimal donGiaH;
        private decimal donGiaNgay;

        public LoaiXe()
        {
        }

        public LoaiXe(DataRow row)
        {
            this.MaLoaiXe = Convert.ToInt32(row["maLoaiXe"]);
            this.tenLoaiXe = row["tenLoaiXe"].ToString();
            this.donGiaH = Convert.ToDecimal(row["donGiaH"]);
            this.donGiaNgay = Convert.ToDecimal(row["donGiaNgay"]);
        }

        
        public int MaLoaiXe { get => maLoaiXe; set => maLoaiXe = value; }
        public string TenLoaiXe { get => tenLoaiXe; set => tenLoaiXe = value; }
        public decimal DonGiaH { get => donGiaH; set => donGiaH = value; }
        public decimal DonGiaNgay { get => donGiaNgay; set => donGiaNgay = value; }
        
    }
}
