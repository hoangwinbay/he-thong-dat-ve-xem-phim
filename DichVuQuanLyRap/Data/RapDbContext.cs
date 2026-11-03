using Microsoft.EntityFrameworkCore;
using DichVuQuanLyRap.Models;

namespace DichVuQuanLyRap.Data
{
    public class RapDbContext : DbContext
    {
        public RapDbContext(DbContextOptions<RapDbContext> options) : base(options)
        {
        }

        public DbSet<RapChieu> RapChieus { get; set; } = null!;
        public DbSet<PhongChieu> PhongChieus { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RapChieu>(entity =>
            {
                entity.ToTable("RapChieu");
                entity.HasKey(e => e.MaRap);
                entity.Property(e => e.TrangThai).HasDefaultValue(true);
            });

            modelBuilder.Entity<PhongChieu>(entity =>
            {
                entity.ToTable("PhongChieu");
                entity.HasKey(e => e.MaPhong);
                entity.HasIndex(e => new { e.MaRap, e.TenPhong }).IsUnique();

                entity.HasOne(p => p.RapChieu)
                      .WithMany(r => r.PhongChieus)
                      .HasForeignKey(p => p.MaRap);
            });
        }
    }
}

