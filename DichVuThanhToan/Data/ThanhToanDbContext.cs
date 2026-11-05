using Microsoft.EntityFrameworkCore;
using DichVuThanhToan.Models;

namespace DichVuThanhToan.Data
{
    public class ThanhToanDbContext : DbContext
    {
        public ThanhToanDbContext(DbContextOptions<ThanhToanDbContext> options) : base(options) { }

        public DbSet<ThanhToan> ThanhToans { get; set; }
        public DbSet<DatVe> DatVes { get; set; }
        public DbSet<ChiTietDatVe> ChiTietDatVes { get; set; }
        public DbSet<GheSuatChieu> GheSuatChieus { get; set; }
        public DbSet<GheNgoi> GheNgois { get; set; }
        public DbSet<SuatChieu> SuatChieus { get; set; }
        public DbSet<Phim> Phims { get; set; }
        public DbSet<PhongChieu> PhongChieus { get; set; }
        public DbSet<RapChieu> RapChieus { get; set; }
        public DbSet<NguoiDung> NguoiDungs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ThanhToan>()
                .HasOne(tt => tt.DatVe)
                .WithMany(dv => dv.ThanhToans)
                .HasForeignKey(tt => tt.MaDatVe)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ChiTietDatVe>()
                .HasOne(ct => ct.DatVe)
                .WithMany(dv => dv.ChiTietDatVes)
                .HasForeignKey(ct => ct.MaDatVe);

            modelBuilder.Entity<ChiTietDatVe>()
                .HasOne(ct => ct.GheSuatChieu)
                .WithMany()
                .HasForeignKey(ct => ct.MaGheSuatChieu);

            modelBuilder.Entity<GheSuatChieu>()
                .HasOne(g => g.GheNgoi)
                .WithMany()
                .HasForeignKey(g => g.MaGhe);

            modelBuilder.Entity<DatVe>()
                .HasOne(dv => dv.NguoiDung)
                .WithMany()
                .HasForeignKey(dv => dv.MaNguoiDung);

            modelBuilder.Entity<DatVe>()
                .HasOne(dv => dv.SuatChieu)
                .WithMany()
                .HasForeignKey(dv => dv.MaSuatChieu);

            modelBuilder.Entity<SuatChieu>()
                .HasOne(sc => sc.Phim)
                .WithMany()
                .HasForeignKey(sc => sc.MaPhim);

            modelBuilder.Entity<SuatChieu>()
                .HasOne(sc => sc.PhongChieu)
                .WithMany()
                .HasForeignKey(sc => sc.MaPhong);

            modelBuilder.Entity<PhongChieu>()
                .HasOne(pc => pc.RapChieu)
                .WithMany()
                .HasForeignKey(pc => pc.MaRap);
        }
    }
}
