using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLyBanGiay.Models
{
    public class Giay
    {
        [Key]
        public int iMaGiay { get; set; }

        [Required]
        [StringLength(25)]
        public string? sTenGiay { get; set; }

        [Required]
        [ForeignKey("iMaNCC")]
        public int? iMaNCC { get; set; }

        public int? iGia { get; set; }


        [Required]
        [ForeignKey("iMaLoaiGiay")]
        public int? iMaLoaiGiay { get; set; }

        [StringLength(1000)]
        public string? sImageUrl { get; set; }

        public int iGiaKM { get; set; }

        [StringLength(8000)]
        public string? sMota { get; set; }

    }
}
