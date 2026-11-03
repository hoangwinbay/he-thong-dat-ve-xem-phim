using Microsoft.EntityFrameworkCore;
using DichVuQuanLyGhe.Models;

namespace DichVuQuanLyGhe.Data
{
    public class GheDbContext : DbContext
    {
        public GheDbContext(DbContextOptions<GheDbContext> options) : base(options)
        {
        }

        public DbSet<GheNgoi> GheNgois { get; set; } = null!;
        public DbSet<GheSuatChieu> GheSuatChieus { get; set; } = null!;
        public DbSet<PhongChieu> PhongChieus { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GheNgoi>(entity =>
            {
                entity.ToTable("GheNgoi");
                entity.HasKey(e => e.MaGhe);
                entity.HasIndex(e => new { e.MaPhong, e.HangGhe, e.SoGhe }).IsUnique();
                entity.Property(e => e.LoaiGhe).HasDefaultValue("Thuong");
                entity.Property(e => e.TrangThai).HasDefaultValue(true);

                entity.HasOne(g => g.PhongChieu)
                      .WithMany()
                      .HasForeignKey(g => g.MaPhong);
            });

            modelBuilder.Entity<GheSuatChieu>(entity =>
            {
                entity.ToTable("GheSuatChieu");
                entity.HasKey(e => e.MaGheSuatChieu);
                entity.HasIndex(e => new { e.MaSuatChieu, e.MaGhe }).IsUnique();
                entity.Property(e => e.TrangThai).HasDefaultValue("Trong");

                entity.HasOne(gs => gs.GheNgoi)
                      .WithMany()
                      .HasForeignKey(gs => gs.MaGhe);
            });

            modelBuilder.Entity<PhongChieu>().ToTable("PhongChieu").HasKey(e => e.MaPhong);
        }
    }
}

