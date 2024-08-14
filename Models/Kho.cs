using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLyBanGiay.Models
{
    public class Kho
    {
      
        public int iMaNCC { get; set; }
 
        public int iMaGiay { get; set; }

        [Range(1, int.MaxValue, ErrorMessage ="Giá trị của Size phải lớn hơn 0")]
        public int iSize { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Giá trị của Số lượng phải lớn hơn 0")]
        public int iSoLuongKho { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Giá trị của Giá nhập phải lớn hơn 0")]
        public int iGiaNhap { get; set; }
    }
}
