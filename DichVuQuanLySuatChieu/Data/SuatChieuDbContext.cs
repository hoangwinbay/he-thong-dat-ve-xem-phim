using Microsoft.EntityFrameworkCore;
using DichVuQuanLySuatChieu.Models;

namespace DichVuQuanLySuatChieu.Data
{
    public class SuatChieuDbContext : DbContext
    {
        public SuatChieuDbContext(DbContextOptions<SuatChieuDbContext> options) : base(options)
        {
        }

        public DbSet<SuatChieu> SuatChieus { get; set; } = null!;
        public DbSet<Phim> Phims { get; set; } = null!;
        public DbSet<PhongChieu> PhongChieus { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SuatChieu>(entity =>
            {
                entity.ToTable("SuatChieu");
                entity.HasKey(e => e.MaSuatChieu);
                entity.Property(e => e.TrangThai).HasDefaultValue("DangMoBan");

                entity.HasOne(s => s.Phim)
                      .WithMany()
                      .HasForeignKey(s => s.MaPhim);

                entity.HasOne(s => s.PhongChieu)
                      .WithMany()
                      .HasForeignKey(s => s.MaPhong);
            });

            modelBuilder.Entity<Phim>().ToTable("Phim").HasKey(e => e.MaPhim);
            modelBuilder.Entity<PhongChieu>().ToTable("PhongChieu").HasKey(e => e.MaPhong);
        }
    }
}

