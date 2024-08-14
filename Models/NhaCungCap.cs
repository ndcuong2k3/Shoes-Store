using System.ComponentModel.DataAnnotations;

namespace QuanLyBanGiay.Models
{
    public class NhaCungCap
    {
        [Key]
        public int iMaNCC { get; set; }

        [Required]
        public string? sTenNCC { get; set; }

        public string? sDiaChi { get; set; }

        public string? sSDT { get; set; }
    }
}
