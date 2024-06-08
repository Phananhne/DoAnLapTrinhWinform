using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace QuanLySieuThi.Models
{
    public partial class SieuthiContextDB : DbContext
    {
        public SieuthiContextDB()
            : base("name=SieuthiContextDB")
        {
        }

        public virtual DbSet<ChucVu> ChucVus { get; set; }
        public virtual DbSet<HangHoa> HangHoas { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<PhieuKiemKe> PhieuKiemKes { get; set; }
        public virtual DbSet<PhieuNhap> PhieuNhaps { get; set; }
        public virtual DbSet<PhieuXuat> PhieuXuats { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public virtual DbSet<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChucVu>()
                .Property(e => e.MaCV)
                .IsUnicode(false);

            modelBuilder.Entity<ChucVu>()
                .HasMany(e => e.Accounts)
                .WithRequired(e => e.ChucVu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ChucVu>()
                .HasMany(e => e.NhanViens)
                .WithRequired(e => e.ChucVu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HangHoa>()
                .Property(e => e.MaHang)
                .IsUnicode(false);

            modelBuilder.Entity<HangHoa>()
                .HasMany(e => e.HoaDons)
                .WithRequired(e => e.HangHoa)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HangHoa>()
                .HasMany(e => e.PhieuKiemKes)
                .WithRequired(e => e.HangHoa)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HangHoa>()
                .HasMany(e => e.PhieuNhaps)
                .WithRequired(e => e.HangHoa)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HangHoa>()
                .HasMany(e => e.PhieuXuats)
                .WithRequired(e => e.HangHoa)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HoaDon>()
                .Property(e => e.MaHD)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDon>()
                .Property(e => e.MaNV)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDon>()
                .Property(e => e.MaHang)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDon>()
                .HasOptional(e => e.ChiTietHoaDon)
                .WithRequired(e => e.HoaDon);

            modelBuilder.Entity<NhaCungCap>()
                .Property(e => e.MaNCC)
                .IsUnicode(false);

            modelBuilder.Entity<NhaCungCap>()
                .HasMany(e => e.PhieuNhaps)
                .WithRequired(e => e.NhaCungCap)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.MaNV)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.MaCV)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.HoaDons)
                .WithRequired(e => e.NhanVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.PhieuKiemKes)
                .WithRequired(e => e.NhanVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.PhieuNhaps)
                .WithRequired(e => e.NhanVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.PhieuXuats)
                .WithRequired(e => e.NhanVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PhieuKiemKe>()
                .Property(e => e.MaPKK)
                .IsUnicode(false);

            modelBuilder.Entity<PhieuKiemKe>()
                .Property(e => e.MaNV)
                .IsUnicode(false);

            modelBuilder.Entity<PhieuKiemKe>()
                .Property(e => e.MaHang)
                .IsUnicode(false);

            modelBuilder.Entity<PhieuNhap>()
                .Property(e => e.MaPN)
                .IsUnicode(false);

            modelBuilder.Entity<PhieuNhap>()
                .Property(e => e.MaNCC)
                .IsUnicode(false);

            modelBuilder.Entity<PhieuNhap>()
                .Property(e => e.MaNV)
                .IsUnicode(false);

            modelBuilder.Entity<PhieuNhap>()
                .Property(e => e.MaHang)
                .IsUnicode(false);

            modelBuilder.Entity<PhieuNhap>()
                .HasMany(e => e.ChiTietPhieuNhaps)
                .WithRequired(e => e.PhieuNhap)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PhieuXuat>()
                .Property(e => e.MaPX)
                .IsUnicode(false);

            modelBuilder.Entity<PhieuXuat>()
                .Property(e => e.MaNV)
                .IsUnicode(false);

            modelBuilder.Entity<PhieuXuat>()
                .Property(e => e.MaHang)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.MaCV)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietHoaDon>()
                .Property(e => e.MaHD)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietPhieuNhap>()
                .Property(e => e.MaPN)
                .IsUnicode(false);
        }
    }
}
