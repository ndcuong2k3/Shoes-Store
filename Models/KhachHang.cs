using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyBanGiay.Models
{
    public class KhachHang
    {
        [Key]
        public int iMaKH { get; set; }

        public string? sTenKH { get; set; }

        public string? sDiaChi { get; set; }

        public string? sSDT { get; set; }

        [ForeignKey("iMaTK")]
        public int iMaTK {  get; set; }
    }
}
