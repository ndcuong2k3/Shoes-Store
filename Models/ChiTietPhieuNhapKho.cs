using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLyBanGiay.Models
{
    public class ChiTietPhieuNhapKho
    {

        [ForeignKey("PhieuNhapKho")]
        public int iMaPNK { get; set; }


        [ForeignKey("Giay")]
        public int iMaGiay { get; set; }

        public int iSize {  get; set; }

        public int iSlNhap { get; set; }

        public int iGiaNhap { get; set; }

    }
}
