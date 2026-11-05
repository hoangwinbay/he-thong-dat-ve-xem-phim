using Microsoft.EntityFrameworkCore;
using GiaoDienKhachHang.Models;

namespace GiaoDienKhachHang.Data
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
        {
        }

        public DbSet<NguoiDung> NguoiDungs { get; set; } = null!;
        public DbSet<Phim> Phims { get; set; } = null!;
        public DbSet<TheLoai> TheLoais { get; set; } = null!;
        public DbSet<PhimTheLoai> PhimTheLoais { get; set; } = null!;
        public DbSet<RapChieu> RapChieus { get; set; } = null!;
        public DbSet<PhongChieu> PhongChieus { get; set; } = null!;
        public DbSet<GheNgoi> GheNgois { get; set; } = null!;
        public DbSet<SuatChieu> SuatChieus { get; set; } = null!;
        public DbSet<GheSuatChieu> GheSuatChieus { get; set; } = null!;
        public DbSet<DatVe> DatVes { get; set; } = null!;
        public DbSet<ChiTietDatVe> ChiTietDatVes { get; set; } = null!;
        public DbSet<ThanhToan> ThanhToans { get; set; } = null!;
        public DbSet<VeDienTu> VeDienTus { get; set; } = null!;
        public DbSet<ThongBao> ThongBaos { get; set; } = null!;
        public DbSet<DanhGiaPhim> DanhGiaPhims { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PhimTheLoai>()
                .HasKey(pt => new { pt.MaPhim, pt.MaTheLoai });

            modelBuilder.Entity<PhimTheLoai>()
                .HasOne(pt => pt.Phim)
                .WithMany(p => p.PhimTheLoais)
                .HasForeignKey(pt => pt.MaPhim);

            modelBuilder.Entity<PhimTheLoai>()
                .HasOne(pt => pt.TheLoai)
                .WithMany(t => t.PhimTheLoais)
                .HasForeignKey(pt => pt.MaTheLoai);

            modelBuilder.Entity<PhongChieu>()
                .HasOne(pc => pc.RapChieu)
                .WithMany(r => r.PhongChieus)
                .HasForeignKey(pc => pc.MaRap);

            modelBuilder.Entity<GheNgoi>()
                .HasOne(g => g.PhongChieu)
                .WithMany(pc => pc.GheNgois)
                .HasForeignKey(g => g.MaPhong);

            modelBuilder.Entity<SuatChieu>()
                .HasOne(s => s.Phim)
                .WithMany(p => p.SuatChieus)
                .HasForeignKey(s => s.MaPhim);

            modelBuilder.Entity<SuatChieu>()
                .HasOne(s => s.PhongChieu)
                .WithMany()
                .HasForeignKey(s => s.MaPhong);

            modelBuilder.Entity<GheSuatChieu>()
                .HasOne(gs => gs.SuatChieu)
                .WithMany(s => s.GheSuatChieus)
                .HasForeignKey(gs => gs.MaSuatChieu);

            modelBuilder.Entity<GheSuatChieu>()
                .HasOne(gs => gs.GheNgoi)
                .WithMany()
                .HasForeignKey(gs => gs.MaGhe);

            modelBuilder.Entity<GheSuatChieu>()
                .HasIndex(gs => new { gs.MaSuatChieu, gs.MaGhe }).IsUnique();

            modelBuilder.Entity<DatVe>()
                .HasOne(dv => dv.NguoiDung)
                .WithMany()
                .HasForeignKey(dv => dv.MaNguoiDung);

            modelBuilder.Entity<DatVe>()
                .HasOne(dv => dv.SuatChieu)
                .WithMany()
                .HasForeignKey(dv => dv.MaSuatChieu);

            modelBuilder.Entity<ChiTietDatVe>()
                .HasOne(ct => ct.DatVe)
                .WithMany(dv => dv.ChiTietDatVes)
                .HasForeignKey(ct => ct.MaDatVe)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ChiTietDatVe>()
                .HasOne(ct => ct.GheSuatChieu)
                .WithMany()
                .HasForeignKey(ct => ct.MaGheSuatChieu);

            modelBuilder.Entity<ThanhToan>()
                .HasOne(tt => tt.DatVe)
                .WithMany(dv => dv.ThanhToans)
                .HasForeignKey(tt => tt.MaDatVe)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<VeDienTu>()
                .HasOne(v => v.ChiTietDatVe)
                .WithOne(ct => ct.VeDienTu)
                .HasForeignKey<VeDienTu>(v => v.MaChiTiet)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ThongBao>()
                .HasOne(tb => tb.NguoiDung)
                .WithMany()
                .HasForeignKey(tb => tb.MaNguoiDung)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DanhGiaPhim>()
                .HasIndex(dg => new { dg.MaNguoiDung, dg.MaPhim })
                .IsUnique();

            modelBuilder.Entity<DanhGiaPhim>()
                .HasOne(dg => dg.NguoiDung)
                .WithMany()
                .HasForeignKey(dg => dg.MaNguoiDung)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DanhGiaPhim>()
                .HasOne(dg => dg.Phim)
                .WithMany()
                .HasForeignKey(dg => dg.MaPhim)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
