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
        }
    }
}
