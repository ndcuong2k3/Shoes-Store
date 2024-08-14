using System.ComponentModel.DataAnnotations;

namespace QuanLyBanGiay.Models
{
    public class LoaiGiay
    {
        [Key]
        public int iMaLoaiGiay { get; set; }
        public string? sTenLoaiGiay { get; set; }
    }
}
