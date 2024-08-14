using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyBanGiay.Models
{
    public class NhanVien
    {
        [Key]
        public int iMaNV { get; set; }

        public string? sTenNV { get; set; }

        [DataType(DataType.Date)]
        public DateTime dNgaysinh { get; set; }

        public string? sGioiTinh { get; set; }

        public string? sQueQuan { get; set; }

        public string? sSDT { get; set; }

        public int? iLuongCB { get; set; }

        public double? fHSL { get; set; }

        public int? iPC { get; set; }

        [ForeignKey("iMaBP")]
        public int iMaBP { get; set; }

        public DateTime dNgayvaolam { get; set; }

        public int iMaTK { get; set; }

        

        
    }
}
