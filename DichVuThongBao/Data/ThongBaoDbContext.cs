using Microsoft.EntityFrameworkCore;
using DichVuThongBao.Models;

namespace DichVuThongBao.Data
{
    public class ThongBaoDbContext : DbContext
    {
        public ThongBaoDbContext(DbContextOptions<ThongBaoDbContext> options) : base(options) { }

        public DbSet<ThongBao> ThongBaos { get; set; }
        public DbSet<NguoiDung> NguoiDungs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ThongBao>()
                .HasOne(tb => tb.NguoiDung)
                .WithMany(nd => nd.ThongBaos)
                .HasForeignKey(tb => tb.MaNguoiDung)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
