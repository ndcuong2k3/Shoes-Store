using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyBanGiay.Models
{
    public class HoaDon
    {
        [Key]

        public int iMaHD { get; set; }

        [Required]
        [ForeignKey("NhanVien")]
        public int iMaNV { get; set; }

        [Required]
        [ForeignKey("KhachHang")]
        public int iMaKH { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime dNgayLap { get; set; }

    }
}
