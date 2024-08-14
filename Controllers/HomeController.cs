//using AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyBanGiay.Models;
using System;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace QuanLyBanGiay.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private DBCustx _dbCustx { get; }
 
        //int MaKH = 99;
        private int MaDB;
       
        


        public HomeController(ILogger<HomeController> logger, DBCustx dBCustx)
        {
            _logger = logger;
            _dbCustx = dBCustx;
            MaDB = GenerateGuestCustomerId();


        }

        public IActionResult Index()
        {
            //var a = _dbCustx.tblGiay.ToList();
            //var firstFourGiay = a.Take(4).ToList();
            //return View(firstFourGiay);
            var allGiay = _dbCustx.tblGiay.ToList();
            var firstFourGiay = allGiay.Take(4).ToList();
            var remainingGiay = allGiay.Where(item => item.iMaLoaiGiay == 1).Skip(2).Take(4);

            var giaynu = allGiay.Skip(4).Take(4).Where(item =>item.iMaLoaiGiay == 2);

            ViewBag.FirstFourGiay = firstFourGiay;
            ViewBag.RemainingGiay = remainingGiay;
            ViewBag.GiayNu = giaynu;

            return View();
        }

        public IActionResult GiamGia()
        {
            var allGiay = _dbCustx.tblGiay.ToList();
            var giaynam = allGiay.Where(item => item.iMaLoaiGiay == 1 && item.iGiaKM < item.iGia).ToList();
            var giaynu = allGiay.Where(item => item.iMaLoaiGiay == 2 && item.iGiaKM < item.iGia).ToList();

            ViewBag.GiayNam = giaynam;
            ViewBag.GiayNu = giaynu;

            return View();
        }

        [HttpPost]
        public IActionResult GiamGia(string TenGiay)
        {
            var allGiay = _dbCustx.tblGiay.ToList();
            var giaynam = allGiay.Where(item => item.iMaLoaiGiay == 1 && item.iGiaKM < item.iGia && item.sTenGiay.ToLower().Contains(TenGiay.ToLower()) == true).ToList();
            var giaynu = allGiay.Where(item => item.iMaLoaiGiay == 2 && item.iGiaKM < item.iGia && item.sTenGiay.ToLower().Contains(TenGiay) == true).ToList();
            ViewBag.GiayNam = giaynam;
            ViewBag.GiayNu = giaynu;

            return View();
        }

        public IActionResult Nam()
        {
            var allGiay = _dbCustx.tblGiay.ToList();
            var giaynam = allGiay.Where(item => item.iMaLoaiGiay == 1).ToList();
            return View(giaynam);
        }
        [HttpPost]
        public IActionResult Nam(string TenGiay)
        {
            var allGiay = _dbCustx.tblGiay.ToList();
            var giaynam = allGiay.Where(item => item.iMaLoaiGiay == 1 && item.sTenGiay.ToLower().Contains(TenGiay.ToLower())==true).ToList();
            return View(giaynam);
        }


        public IActionResult Nu()
        {
            var allGiay = _dbCustx.tblGiay.ToList();
            var giaynu = allGiay.Where(item => item.iMaLoaiGiay == 2).ToList();
            return View(giaynu);
        }
        [HttpPost]
        public IActionResult Nu(string TenGiay)
        {
            var allGiay = _dbCustx.tblGiay.ToList();
            var giaynu = allGiay.Where(item => item.iMaLoaiGiay == 2 && item.sTenGiay.ToLower().Contains(TenGiay.ToLower()) == true).ToList();
            return View(giaynu);
        }

        public IActionResult ChiTietGiay(int id)
        {
            var a = GetGiayById(id);
            var DanhSachSize = _dbCustx.tblKho.Where(item =>item.iMaGiay == id).Select(item => item.iSize).ToList();
            ViewBag.DanhSachSize = DanhSachSize;
            return View(a);
        }

        public Giay GetGiayById(int id)
        {
            return _dbCustx.tblGiay.FirstOrDefault(g => g.iMaGiay == id);
        }
        //[HttpPost]
        //public IActionResult GioHang_Them(int productId, string imageUrl)
        //{
        //    int MaKH = Convert.ToInt32(HttpContext.Session.GetString("MaKH"));
        //    GioHang gh = new GioHang();
        //    gh.iMaGioHang = _dbCustx.tblGioHang.Max(gh => gh.iMaGioHang) + 1;
        //    gh.iMaKH = MaKH;
        //    gh.iMaGiay = productId;
        //    gh.iSoLuong = 1;
        //    gh.iSize = _dbCustx.tblKho.Where(item => item.iMaGiay == productId).Select(item=> item.iSize).First();
        //    gh.sImageUrl = imageUrl;
        //    _dbCustx.tblGioHang.Add(gh);
        //    _dbCustx.SaveChanges();
        //}
        //[HttpPost]
        //public IActionResult GioHang_Them(int productId, string imageUrl)
        //{
        //    int MaKH = 0;
        //    // Kiểm tra người dùng đã đăng nhập chưa
        //    if (HttpContext.Session.GetString("MaKH") != null)
        //    {
        //        MaKH = Convert.ToInt32(HttpContext.Session.GetString("MaKH"));

        //        // Xác định iMaGioHang mới
        //        int newGioHangId = _dbCustx.tblGioHang.Max(gh => gh.iMaGioHang) + 1;

        //        // Tạo mới đối tượng GioHang
        //        GioHang gh = new GioHang
        //        {
        //            iMaGioHang = newGioHangId,
        //            iMaKH = MaKH,
        //            iMaGiay = productId,
        //            iSoLuong = 1,
        //            iSize = _dbCustx.tblKho.Where(item => item.iMaGiay == productId).Select(item => item.iSize).FirstOrDefault(),
        //            sImageUrl = imageUrl
        //        };

        //        // Thêm vào cơ sở dữ liệu
        //        _dbCustx.tblGioHang.Add(gh);
        //         _dbCustx.SaveChanges();

        //        // Trả về kết quả thành công
        //        return Json(new { success = true });
        //    }
        //    else
        //    {
        //        // Trả về kết quả thất bại nếu người dùng chưa đăng nhập
        //        return Json(new { success = false,message ="TenTK" });
        //    }
        //}

        [HttpPost]
        public IActionResult GioHang_Them(int productId, string imageUrl)
        {
            int MaKH = 0;
            // Kiểm tra người dùng đã đăng nhập chưa
            if (HttpContext.Session.GetString("MaKH") != null)
            {
                MaKH = Convert.ToInt32(HttpContext.Session.GetString("MaKH"));

                // Kiểm tra xem sản phẩm đã tồn tại trong giỏ hàng hay chưa
                var existingCartItem = _dbCustx.tblGioHang.SingleOrDefault(item => item.iMaKH == MaKH && item.iMaGiay == productId);

                if (existingCartItem != null)
                {
                    // Nếu sản phẩm đã tồn tại, cập nhật số lượng
                    existingCartItem.iSoLuong += 1;
                    _dbCustx.SaveChanges();
                }
                else
                {
                    // Nếu sản phẩm chưa tồn tại, thêm mới vào giỏ hàng
                    //int newGioHangId = _dbCustx.tblGioHang.Max(gh => gh.iMaGioHang) + 1;
                    int newGioHangId;

                    if (_dbCustx.tblGioHang.Any())
                    {
                        newGioHangId = _dbCustx.tblGioHang.Max(gh => gh.iMaGioHang) + 1;
                    }
                    else
                    {
                        newGioHangId = 1;
                    }

                    GioHang gh = new GioHang
                    {
                        iMaGioHang = newGioHangId,
                        iMaKH = MaKH,
                        iMaGiay = productId,
                        iSoLuong = 1,
                        iSize = _dbCustx.tblKho.Where(item => item.iMaGiay == productId).Select(item => item.iSize).FirstOrDefault(),
                        sImageUrl = imageUrl
                    };

                    _dbCustx.tblGioHang.Add(gh);
                    _dbCustx.SaveChanges();
                }

                
                return Json(new { success = true });
            }
            else
            {
                // Trả về kết quả thất bại nếu người dùng chưa đăng nhập
                return Json(new { success = false, message = "TenTK" });
            }
        }


        [HttpPost]
        public IActionResult GioHang_Them_CT(int productId, int soluong, int size, string imageUrl)
        {
            int MaKH = 0;
            // Kiểm tra người dùng đã đăng nhập chưa
            if (HttpContext.Session.GetString("MaKH") != null)
            {
                MaKH = Convert.ToInt32(HttpContext.Session.GetString("MaKH"));

                // Xác định iMaGioHang mới
                var existingCartItem = _dbCustx.tblGioHang.SingleOrDefault(item => item.iMaKH == MaKH && item.iMaGiay == productId);

                if (existingCartItem != null)
                {
                    // Nếu sản phẩm đã tồn tại, cập nhật số lượng
                    existingCartItem.iSoLuong += soluong;
                }
                else
                {
                    // Nếu sản phẩm chưa tồn tại, thêm mới vào giỏ hàng
                    int newGioHangId;

                    if (_dbCustx.tblGioHang.Any())
                    {
                        newGioHangId = _dbCustx.tblGioHang.Max(gh => gh.iMaGioHang) + 1;
                    }
                    else
                    {
                        newGioHangId = 1;
                    }


                    GioHang gh = new GioHang
                    {
                        iMaGioHang = newGioHangId,
                        iMaKH = MaKH,
                        iMaGiay = productId,
                        iSoLuong = soluong,
                        iSize = size,
                        sImageUrl = imageUrl
                    };

                    _dbCustx.tblGioHang.Add(gh);
                }
                _dbCustx.SaveChanges();

                // Trả về kết quả thành công
                return Json(new { success = true });
            }
            else
            {
                // Trả về kết quả thất bại nếu người dùng chưa đăng nhập
                return Json(new { success = false, message = "TenTK" });
            }
        }


        public IActionResult GioHang()
        {
            int MaKH = Convert.ToInt32(HttpContext.Session.GetString("MaKH"));
            // var a = _dbCustx.tblGioHang.Where(item => item.iMaKH == MaKH).ToList();
            var query = from giohang in _dbCustx.tblGioHang.ToList()
                        join giay in _dbCustx.tblGiay.ToList() on giohang.iMaGiay equals giay.iMaGiay
                        where giohang.iMaKH == MaKH
                        select new
                        {
                            MaGioHang = giohang.iMaGioHang,
                            MaKH = giohang.iMaKH,
                            MaGiay = giay.iMaGiay,
                            AnhGiay = giay.sImageUrl,
                            TenGiay = giay.sTenGiay,
                            SoLuong = giohang.iSoLuong,
                            Size = giohang.iSize,
                            Gia = giay.iGia
                        };
            var reslut = query.ToList();
            return View(reslut);
        }

        [HttpPost]
        public IActionResult GioHang_Xoa(int MGiay, int MKH) 
        {
            var item = _dbCustx.tblGioHang.Where(item => item.iMaGiay == MGiay && item.iMaKH == MKH).ToList();
            if (item == null)
            {

                return Json(new { success = false, message = "Product not found!" });
            }
            else
            {
                for(int i = 0; i < item.Count(); i++)
                {
                    _dbCustx.tblGioHang.Remove(item[i]);
                }
                

            }

            _dbCustx.SaveChanges();
            return Json(new { success = true });
            
        }

        [HttpPost]
        public IActionResult GioHang_CapNhat(int MGiay, int MKH, int Size, int SoLuong, int MGHang) 
        {
            var item = _dbCustx.tblGioHang.SingleOrDefault(item => item.iMaGiay == MGiay && item.iMaKH == MKH && item.iMaGioHang == MGHang); // S? d?ng MGHang ?? l?c
            if (item == null)
            {
                return Json(new { success = false, message = "Product not found!" });
            }
            else
            {
                item.iSoLuong = SoLuong;
                item.iSize = Size;
            }

            _dbCustx.SaveChanges();
            return Json(new { success = true });
        }

        public JsonResult getGioHang()
        {
            int MaKH = Convert.ToInt32(HttpContext.Session.GetString("MaKH"));
            var query = from giohang in _dbCustx.tblGioHang.ToList()
                        join giay in _dbCustx.tblGiay.ToList() on giohang.iMaGiay equals giay.iMaGiay
                        where giohang.iMaKH == MaKH
                        select new
                        {
                            MaGioHang = giohang.iMaGioHang,
                            MaKH = giohang.iMaKH,
                            MaGiay = giay.iMaGiay,
                            AnhGiay = giay.sImageUrl,
                            TenGiay = giay.sTenGiay,
                            SoLuong = giohang.iSoLuong,
                            Size = giohang.iSize,
                            Gia = giay.iGia
                        };
            var reslut = query.ToList();
            return Json(reslut);
        }

        //public IActionResult GetSizes()
        //{
        //    var sizes = _dbCustx.tblKho.Select(k => k.iSize).Distinct().ToList();
        //    return Json(sizes);
        //}

        public IActionResult GetSizesByProductId(int productId)
        {
            var sizes = _dbCustx.tblKho
                              .Where(k => k.iMaGiay == productId)
                              .Select(k => k.iSize)
                              .Distinct()
                              .ToList();
            return Json(sizes);
        }

        public IActionResult DangNhap()
        {
            
            string tenTK = HttpContext.Session.GetString("TenTK");
            string matKhau = HttpContext.Session.GetString("MatKhau");
            string MaTK = HttpContext.Session.GetString("a");
            string NV = HttpContext.Session.GetString("MaNV");
            ViewBag.NV = NV;
            ViewBag.TenTK = tenTK;
            ViewBag.MaTK = MaTK;
            return View();
        }
        //[HttpPost]
        //public IActionResult DangNhap(string TenTK, string MatKhau)
        //{
        //    var tenTK = _dbCustx.tblTaiKhoan.FirstOrDefault(tk => tk.sTenTK == TenTK);
        //    var MK = _dbCustx.tblTaiKhoan.FirstOrDefault(tk => tk.sMatkhau == MatKhau);

        //    var a = _dbCustx.tblTaiKhoan.Where(item => item.sTenTK.Equals(TenTK) == true && item.sMatkhau.Equals(MatKhau) == true).Select(item => item.iMaTK).FirstOrDefault();

        //    var MaKH = _dbCustx.tblKhachHang.Where(item => item.iMaTK == a).Select(item => item.iMaKH).FirstOrDefault();

        //    //var a = from tk in _dbCustx.tblTaiKhoan.ToList() join
        //    //        nv in _dbCustx.tblNhanVien.ToList() on tk.iMaTK equals nv.iMaTK
        //    if (tenTK != null && MK != null && tenTK.sTenTK == TenTK && MK.sMatkhau == MatKhau)
        //    {
        //        MaKH = Convert.ToInt32(MaKH);
        //        HttpContext.Session.SetString("MaKH", MaKH.ToString());

        //     //   int MaKHa = Convert.ToInt32(HttpContext.Session.GetString("MaKH"));
        //        HttpContext.Session.SetString("a", a.ToString());
        //        HttpContext.Session.SetString("TenTK", tenTK.sTenTK);
        //        HttpContext.Session.SetString("MatKhau", MK.sMatkhau);
        //        return Json(new { success = true });
        //    }
        //    return Json(new { success = false });
        //}

        [HttpPost]
        public IActionResult DangNhap(string TenTK, string MatKhau)
        {
            var tenTK = _dbCustx.tblTaiKhoan.FirstOrDefault(tk => tk.sTenTK == TenTK);
            var MK = _dbCustx.tblTaiKhoan.FirstOrDefault(tk => tk.sMatkhau == MatKhau);

            var a = _dbCustx.tblTaiKhoan.Where(item => item.sTenTK.Equals(TenTK) == true && item.sMatkhau.Equals(MatKhau) == true).Select(item => item.iMaTK).FirstOrDefault();

            var nhanVien = _dbCustx.tblNhanVien.FirstOrDefault(nv => nv.iMaTK == a);
            var khachHang = _dbCustx.tblKhachHang.FirstOrDefault(kh => kh.iMaTK == a);

            if (nhanVien != null)
            {
                
                if(nhanVien.iMaBP == 1)
                {
                   
                    HttpContext.Session.SetString("MaNV", nhanVien.iMaBP.ToString());
                    HttpContext.Session.SetString("TenTK", tenTK.sTenTK.ToString());
                    return Json(new { success = true });
                }
            }
            else if (khachHang != null)
            {
                // `iMaTK` tương ứng là của một khách hàng
                var MaKH = _dbCustx.tblKhachHang.Where(item => item.iMaTK == a).Select(item => item.iMaKH).FirstOrDefault();

                //var a = from tk in _dbCustx.tblTaiKhoan.ToList() join
                //        nv in _dbCustx.tblNhanVien.ToList() on tk.iMaTK equals nv.iMaTK
                if (tenTK != null && MK != null && tenTK.sTenTK == TenTK && MK.sMatkhau == MatKhau)
                {
                    MaKH = Convert.ToInt32(MaKH);
                    HttpContext.Session.SetString("MaKH", MaKH.ToString());

                    //   int MaKHa = Convert.ToInt32(HttpContext.Session.GetString("MaKH"));
                    HttpContext.Session.SetString("a", a.ToString());
                    HttpContext.Session.SetString("TenTK", tenTK.sTenTK);
                    HttpContext.Session.SetString("MatKhau", MK.sMatkhau);
                    return Json(new { success = true });
                }
            }

           
            return Json(new { success = false });
        }

        public IActionResult DangXuat()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("DangNhap");
        }

        public IActionResult DangKi()
        {
            return View();
        }
        [HttpPost]
        public IActionResult DangKi(string TenKH, string DiaChi, string SDT, string TenTK, string MatKhau)
        {
            try
            {
                var TonTaiSDT = _dbCustx.tblKhachHang.FirstOrDefault(kh => kh.sSDT == SDT);
                if (TonTaiSDT != null)
                {

                    return Json(new { success = false, message = "SDT" });
                }

                var TonTaiTK = _dbCustx.tblTaiKhoan.FirstOrDefault(tk => tk.sTenTK == TenTK);
                if (TonTaiTK != null)
                {

                    return Json(new { success = false, message = "TenTK" });
                }
                int maxMaKH = _dbCustx.tblKhachHang.Max(kh => kh.iMaKH);
                int maxMaTK = _dbCustx.tblTaiKhoan.Max(tk => tk.iMaTK);

                TaiKhoan taikhoan = new TaiKhoan
                {
                    iMaTK = maxMaTK + 1,
                    sTenTK = TenTK,
                    sMatkhau = MatKhau
                };

                _dbCustx.tblTaiKhoan.Add(taikhoan);
                _dbCustx.SaveChanges();

                KhachHang newKhachHang = new KhachHang
                {
                    iMaKH = maxMaKH + 1,
                    sTenKH = TenKH,
                    sDiaChi = DiaChi,
                    sSDT = SDT,
                    iMaTK = maxMaTK + 1
                };

                _dbCustx.tblKhachHang.Add(newKhachHang);
                _dbCustx.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public IActionResult ThanhToan()
        {
            
            // Kiểm tra người dùng đã đăng nhập chưa
            if (HttpContext.Session.GetString("MaKH") != null)
            {
                int MaKH = Convert.ToInt32(HttpContext.Session.GetString("MaKH"));
                var TenKH = _dbCustx.tblKhachHang.Where(item => item.iMaKH == MaKH).Select(item => item.sTenKH).First();
                var DiaChi = _dbCustx.tblKhachHang.Where(item => item.iMaKH == MaKH).Select(item => item.sDiaChi).First();
                var SDT = _dbCustx.tblKhachHang.Where(item => item.iMaKH == MaKH).Select(item => item.sSDT).First();
                ViewBag.TenKH_TT = TenKH;
                ViewBag.DiaChi_TT = DiaChi;
                ViewBag.SDT_TT = SDT;
                ViewBag.MaKH_TT = Convert.ToInt16(MaKH);
                //int MaKH = Convert.ToInt32(HttpContext.Session.GetString("MaKH"));
                // var a = _dbCustx.tblGioHang.Where(item => item.iMaKH == MaKH).ToList();
                var query = from giohang in _dbCustx.tblGioHang.ToList()
                            join giay in _dbCustx.tblGiay.ToList() on giohang.iMaGiay equals giay.iMaGiay
                            join kh in _dbCustx.tblKhachHang.ToList() on giohang.iMaKH equals kh.iMaKH
                            where giohang.iMaKH == MaKH
                            select new
                            {
                                TenKH = kh.sTenKH,
                                MaGioHang = giohang.iMaGioHang,
                                MaKH = giohang.iMaKH,
                                MaGiay = giay.iMaGiay,
                                AnhGiay = giay.sImageUrl,
                                TenGiay = giay.sTenGiay,
                                SoLuong = giohang.iSoLuong,
                                Size = giohang.iSize,
                                Gia = giay.iGia
                            };

                var reslut = query.ToList();
                return View(reslut);
            }
            else
            {
                int MaKH = 200;
                var TenKH = _dbCustx.tblKhachHang.Where(item => item.iMaKH == MaKH).Select(item => item.sTenKH).First();
                var DiaChi = _dbCustx.tblKhachHang.Where(item => item.iMaKH == MaKH).Select(item => item.sDiaChi).First();
                var SDT = _dbCustx.tblKhachHang.Where(item => item.iMaKH == MaKH).Select(item => item.sSDT).First();
                ViewBag.TenKH_TT = TenKH;
                ViewBag.DiaChi_TT = DiaChi;
                ViewBag.SDT_TT = SDT;
                ViewBag.MaKH_TT = Convert.ToInt16(MaKH);
                //int MaKH = Convert.ToInt32(HttpContext.Session.GetString("MaKH"));
                // var a = _dbCustx.tblGioHang.Where(item => item.iMaKH == MaKH).ToList();
                var query = from giohang in _dbCustx.tblGioHang.ToList()
                            join giay in _dbCustx.tblGiay.ToList() on giohang.iMaGiay equals giay.iMaGiay
                            join kh in _dbCustx.tblKhachHang.ToList() on giohang.iMaKH equals kh.iMaKH
                            where giohang.iMaKH == MaKH
                            select new
                            {
                                TenKH = kh.sTenKH,
                                MaGioHang = giohang.iMaGioHang,
                                MaKH = giohang.iMaKH,
                                MaGiay = giay.iMaGiay,
                                AnhGiay = giay.sImageUrl,
                                TenGiay = giay.sTenGiay,
                                SoLuong = giohang.iSoLuong,
                                Size = giohang.iSize,
                                Gia = giay.iGia
                            };

                var reslut = query.ToList();
                return View(reslut);
            }

        }

        [HttpPost]
        public IActionResult XuLyThanhToan(int MAKH)
        {
            if (MAKH == 200)
            {
                var a = _dbCustx.tblGioHang.FirstOrDefault(item => item.iMaKH.Equals(MAKH) == true);
                _dbCustx.tblGioHang.Remove(a);
                _dbCustx.SaveChanges();

                var d = _dbCustx.tblKhachHang.Where(item => item.iMaKH.Equals(MAKH) == true).Select(item => item.iMaTK).First();
                var b = _dbCustx.tblKhachHang.FirstOrDefault(item => item.iMaKH.Equals(MAKH) == true);
                _dbCustx.tblKhachHang.Remove(b);
                _dbCustx.SaveChanges();

                
                var c = _dbCustx.tblTaiKhoan.FirstOrDefault(item => item.iMaTK.Equals(d) == true);
                _dbCustx.tblTaiKhoan.Remove(c);

                
               
                _dbCustx.SaveChanges();

                
            }
            else
            {
                var a = _dbCustx.tblGioHang.Where(item => item.iMaKH.Equals(MAKH) == true).ToList();
                for( var i = 0;i< a.Count; i++ )
                {
                    _dbCustx.tblGioHang.Remove(a[i]);
                    _dbCustx.SaveChanges();
                }
               
            }

            return Json(new { success = true, message = "suc" });


            //return RedirectToAction("Index");
        }

       

        [HttpPost]
        public IActionResult MuaNgay(int productId, string imageUrl)
        {
            // Kết hợp thời gian và số duy nhất để tạo mã ngẫu nhiên


            // Kiểm tra người dùng đã đăng nhập chưa
            if (HttpContext.Session.GetString("MaKH") != null)
            {
                int MaKH = Convert.ToInt32(HttpContext.Session.GetString("MaKH"));
                //int maxMaGH = _dbCustx.tblGioHang.Max(gh => gh.iMaGioHang);

                //int maxMaGH;

                //if (_dbCustx.tblGioHang.Any())
                //{
                //    maxMaGH = _dbCustx.tblGioHang.Max(gh => gh.iMaGioHang) + 1;
                //}
                //else
                //{
                //    maxMaGH = 1;
                //}

                var existingCartItem = _dbCustx.tblGioHang.SingleOrDefault(item => item.iMaKH == MaKH && item.iMaGiay == productId);

                if (existingCartItem != null)
                {
                    // Nếu sản phẩm đã tồn tại, cập nhật số lượng
                    existingCartItem.iSoLuong += 1;
                    _dbCustx.SaveChanges();
                }
                else
                {
                    // Nếu sản phẩm chưa tồn tại, thêm mới vào giỏ hàng
                    //int newGioHangId = _dbCustx.tblGioHang.Max(gh => gh.iMaGioHang) + 1;
                    int newGioHangId;

                    if (_dbCustx.tblGioHang.Any())
                    {
                        newGioHangId = _dbCustx.tblGioHang.Max(gh => gh.iMaGioHang) + 1;
                    }
                    else
                    {
                        newGioHangId = 1;
                    }

                    GioHang gh = new GioHang
                    {
                        iMaGioHang = newGioHangId,
                        iMaKH = MaKH,
                        iMaGiay = productId,
                        iSoLuong = 1,
                        iSize = _dbCustx.tblKho.Where(item => item.iMaGiay == productId).Select(item => item.iSize).FirstOrDefault(),
                        sImageUrl = imageUrl
                    };

                    _dbCustx.tblGioHang.Add(gh);
                    _dbCustx.SaveChanges();
                }



                //GioHang gioHang = new GioHang
                //{
                //    iMaKH = MaKH,
                //    iMaGioHang = maxMaGH + 1,
                //    iMaGiay = productId,
                //    iSoLuong = 1,
                //    iSize = _dbCustx.tblKho.Where(item => item.iMaGiay == productId).Select(item => item.iSize).FirstOrDefault(),
                //    sImageUrl = imageUrl
                //};
                //_dbCustx.tblGioHang.Add(gioHang);
                //_dbCustx.SaveChanges();

            }
            else
            {
                int MaKH = 200;
                var khachHang = _dbCustx.tblKhachHang.FirstOrDefault(kh => kh.iMaKH == MaKH);

                    // Tạo mới một tài khoản
                    var taiKhoan = new TaiKhoan
                    {
                        iMaTK = 200,

                        sTenTK = "Guest_" + MaKH, // Tạo tên tài khoản tạm thời
                        sMatkhau = "" // Có thể đặt mật khẩu mặc định hoặc để trống tùy vào yêu cầu
                    };
                    _dbCustx.tblTaiKhoan.Add(taiKhoan);
                    _dbCustx.SaveChanges();

                    // Tạo mới một khách hàng và liên kết với tài khoản vừa tạo
                    khachHang = new KhachHang
                    {
                        iMaKH = 200,

                        sTenKH = "Guest",
                        sDiaChi = "Guest",
                        sSDT = "123456789",
                        iMaTK = taiKhoan.iMaTK // Liên kết với tài khoản mới tạo
                    };
                    _dbCustx.tblKhachHang.Add(khachHang);
                    _dbCustx.SaveChanges();

                    GioHang gh = new GioHang
                    {
                        iMaGioHang = 200,
                        iMaKH = 200,
                        iMaGiay = productId,
                        iSoLuong = 1,
                        iSize = _dbCustx.tblKho.Where(item => item.iMaGiay == productId).Select(item => item.iSize).FirstOrDefault(),
                        sImageUrl = imageUrl
                    };

                    // Thêm vào cơ sở dữ liệu
                    _dbCustx.tblGioHang.Add(gh);
                    _dbCustx.SaveChanges();

                
            }
            return Json(new { success = true });
        }





        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private int GenerateGuestCustomerId()
        {
            // Lấy phần tử cuối cùng của thời gian hiện tại
            long lastTicks = DateTime.Now.Ticks % 10000000; // Lấy 7 chữ số cuối cùng của ticks (1 tick = 100 nanoseconds)

            // Thêm một số duy nhất vào cuối mã để đảm bảo tính duy nhất
            int uniqueNumber = new Random().Next(1000, 9999);

            // Kết hợp thời gian và số duy nhất để tạo mã ngẫu nhiên
            int guestCustomerId = (int)(lastTicks * 10000 + uniqueNumber);
            return guestCustomerId;
        }

    }
}
