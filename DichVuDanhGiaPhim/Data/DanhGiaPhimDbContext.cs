using Microsoft.EntityFrameworkCore;
using DichVuDanhGiaPhim.Models;

namespace DichVuDanhGiaPhim.Data
{
    public class DanhGiaPhimDbContext : DbContext
    {
        public DanhGiaPhimDbContext(DbContextOptions<DanhGiaPhimDbContext> options) : base(options) { }

        public DbSet<DanhGiaPhim> DanhGiaPhims { get; set; }
        public DbSet<NguoiDung> NguoiDungs { get; set; }
        public DbSet<Phim> Phims { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
