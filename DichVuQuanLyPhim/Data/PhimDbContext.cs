using Microsoft.EntityFrameworkCore;
using DichVuQuanLyPhim.Models;

namespace DichVuQuanLyPhim.Data
{
    public class PhimDbContext : DbContext
    {
        public PhimDbContext(DbContextOptions<PhimDbContext> options) : base(options)
        {
        }

        public DbSet<Phim> Phims { get; set; } = null!;
        public DbSet<TheLoai> TheLoais { get; set; } = null!;
        public DbSet<PhimTheLoai> PhimTheLoais { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Phim>(entity =>
            {
                entity.ToTable("Phim");
                entity.HasKey(e => e.MaPhim);
                entity.Property(e => e.TrangThai).HasDefaultValue("SapChieu");
            });

            modelBuilder.Entity<TheLoai>(entity =>
            {
                entity.ToTable("TheLoai");
                entity.HasKey(e => e.MaTheLoai);
                entity.HasIndex(e => e.TenTheLoai).IsUnique();
            });

            modelBuilder.Entity<PhimTheLoai>(entity =>
            {
                entity.ToTable("Phim_TheLoai");
                entity.HasKey(pt => new { pt.MaPhim, pt.MaTheLoai });

                entity.HasOne(pt => pt.Phim)
                      .WithMany(p => p.PhimTheLoais)
                      .HasForeignKey(pt => pt.MaPhim);

                entity.HasOne(pt => pt.TheLoai)
                      .WithMany(t => t.PhimTheLoais)
                      .HasForeignKey(pt => pt.MaTheLoai);
            });
        }
    }
}

