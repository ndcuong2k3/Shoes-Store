using Microsoft.AspNetCore.Mvc;
using QuanLyBanGiay.Models;
using System.ComponentModel.DataAnnotations;

namespace QuanLyBanGiay.Models
{
    public class BoPhanTest
    {
        [Key]
        public int? iMaBP { get; set; }
        public string? sTenBP { get; set; }

        [RegularExpression(@"^\d.{9}$", ErrorMessage = "Verified must start with a digit and have 10 characters.")]
        public string? Verified { get; set; }

    }
}

//[HttpPost]
//public IActionResult BoPhanTest_ThemMoi(BoPhanTest boPhan)
//{
//    if (boPhan.Verified == null || boPhan.Verified.Length != 10 || !char.IsDigit(boPhan.Verified[0]))
//    {
//        ModelState.AddModelError("Verified", "Verified must start with a digit and have 10 characters.");
//    }

//    if (ModelState.IsValid)
//    {
//        _dbCustx.BoPhanTest.Add(boPhan);
//        _dbCustx.SaveChanges();
//        return RedirectToAction("BoPhanTest");
//    }

//    return View(boPhan);
//}

