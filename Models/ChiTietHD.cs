using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyBanGiay.Models
{
    public class ChiTietHD
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("HoaDon")]
        public int iMaHD { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Giay")]
        public int iMaGiay { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int iSl { get; set; }

        [Required]
        public int iTien { get; set; }
    }
}
