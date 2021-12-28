using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBaiGiuXe.Model
{
    public class PhieuThanhToan
    {
        private int idVeXe;
        private DateTime gioRa;
        private decimal tongTien;
        public PhieuThanhToan(DataRow row)
        {
            this.idVeXe = Convert.ToInt32(row["idVeXe"]);
            this.gioRa = Convert.ToDateTime(row["gioRa"]);
            this.tongTien = Convert.ToDecimal(row["tongTien"]);
        }
        public int IdVeXe { get => idVeXe; set => idVeXe = value; }
        public DateTime GioRa { get => gioRa; set => gioRa = value; }
        public decimal TongTien { get => tongTien; set => tongTien = value; }
    }
}
