using System.ComponentModel.DataAnnotations;

namespace QuanLyBanGiay.Models
{
    public class BoPhan
    {
      
            [Key]
            public int? iMaBP { get; set; }
            public string? sTenBP { get; set; }

       

    }
}
