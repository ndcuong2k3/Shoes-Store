using System.ComponentModel.DataAnnotations;

namespace QuanLyBanGiay.Models
{
    public class TaiKhoan
    {
        [Key]
        public int iMaTK { get; set; }

        [Required]
        [StringLength(20)]
        public string? sTenTK { get; set; }

        [Required]
        [StringLength(20)]
        public string? sMatkhau { get; set; }
    }
}
