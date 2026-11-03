using Microsoft.EntityFrameworkCore;
using DichVuQuanLyNguoiDung.Models;

namespace DichVuQuanLyNguoiDung.Data
{
    public class NguoiDungDbContext : DbContext
    {
        public NguoiDungDbContext(DbContextOptions<NguoiDungDbContext> options) : base(options)
        {
        }

        public DbSet<NguoiDung> NguoiDungs { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<NguoiDung>(entity =>
            {
                entity.ToTable("NguoiDung");
                entity.HasKey(e => e.MaNguoiDung);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.VaiTro).HasDefaultValue("KhachHang");
                entity.Property(e => e.NgayTao).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.TrangThai).HasDefaultValue(true);
            });
        }
    }
}

