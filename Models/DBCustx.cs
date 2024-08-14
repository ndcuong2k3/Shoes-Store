using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace QuanLyBanGiay.Models
{
    public class DBCustx:DbContext
    {
        public DBCustx(DbContextOptions<DBCustx> options) : base(options) { }
        //public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<BoPhanTest> BoPhanTest { get; set; }
        public DbSet<NhanVien> tblNhanVien { get; set; }
        public DbSet<BoPhan> tblBoPhan { get; set; }
        public DbSet<Giay_Anh> tblGiay_Anh { get; set; }
        public DbSet<NhaCungCap> tblNhaCungCap { get; set; }
        public DbSet <Giay> tblGiay { get; set; }
        public DbSet<Kho> tblKho { get; set; }
        public DbSet<PhieuNhapKho> tblPhieuNhapKho { get; set; }
        public DbSet<ChiTietPhieuNhapKho> tblChiTietPhieuNhapKho { get; set; }

        public DbSet<KhachHang> tblKhachHang { get; set; }
        public DbSet<LoaiGiay> tblLoaiGiay { get; set; }
        public DbSet<TaiKhoan> tblTaiKhoan { get; set; }
        public DbSet<HoaDon> tblHoaDon { get; set; }
        public DbSet<ChiTietHD> tblChiTietHD { get; set; }
        public DbSet<GioHang> tblGioHang { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kho>()
                .HasKey(k => new { k.iMaNCC, k.iMaGiay, k.iSize});
            modelBuilder.Entity<GioHang>().HasKey(k => new { k.iMaGiay, k.iMaGioHang });
            modelBuilder.Entity<ChiTietPhieuNhapKho>().HasKey(k => new {k.iMaGiay, k.iMaPNK, k.iSize});
            modelBuilder.Entity<ChiTietHD>()
                        .HasKey(chiTiet => new { chiTiet.iMaHD, chiTiet.iMaGiay });
        }

       
        //public IQueryable<NhanVien> TimKiemNV(string TenNV)
        //{
        //    SqlParameter pContactName = new SqlParameter("@TenNV", TenNV);

        //    return this.tblNhanVien.FromSqlRaw("EXECUTE TimKiemNV @TenNV", pContactName);

        //}

        //public IQueryable<NhanVien> DSNV()
        //{

        //    return this.tblNhanVien.FromSqlRaw("Select * from tblNhanVien");

        //}


    }
}
