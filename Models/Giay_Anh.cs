using System.ComponentModel.DataAnnotations;

namespace QuanLyBanGiay.Models
{
    public class Giay_Anh
    {
        [Key]
        public string? sMaGiay {  get; set; }
        public string? sImageUrl {  get; set; }
    }
}
