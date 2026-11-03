using Microsoft.EntityFrameworkCore;
using DichVuQuanLySuatChieu.Data;
using DichVuQuanLySuatChieu.DTOs;
using DichVuQuanLySuatChieu.Models;

namespace DichVuQuanLySuatChieu.Repositories
{
    public class SuatChieuRepository : ISuatChieuRepository
    {
        private readonly SuatChieuDbContext _context;

        public SuatChieuRepository(SuatChieuDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SuatChieu>> GetListAsync(LocSuatChieuDTO filter)
        {
            var query = _context.SuatChieus
                .Include(s => s.Phim)
                .Include(s => s.PhongChieu)
                .AsNoTracking();

            if (filter.MaPhim.HasValue)
            {
                query = query.Where(s => s.MaPhim == filter.MaPhim.Value);
            }

            if (filter.MaPhong.HasValue)
            {
                query = query.Where(s => s.MaPhong == filter.MaPhong.Value);
            }

            if (filter.NgayChieu.HasValue)
            {
                var d = filter.NgayChieu.Value.Date;
                query = query.Where(s => s.NgayChieu == d);
            }

            if (!string.IsNullOrWhiteSpace(filter.TrangThai))
            {
                query = query.Where(s => s.TrangThai == filter.TrangThai);
            }

            return await query.OrderBy(s => s.NgayChieu).ThenBy(s => s.GioBatDau).ToListAsync();
        }

        public async Task<SuatChieu?> GetByIdAsync(int id)
        {
            return await _context.SuatChieus
                .Include(s => s.Phim)
                .Include(s => s.PhongChieu)
                .FirstOrDefaultAsync(s => s.MaSuatChieu == id);
        }

        public async Task<SuatChieu> AddAsync(SuatChieu suatChieu)
        {
            await _context.SuatChieus.AddAsync(suatChieu);
            await _context.SaveChangesAsync();
            return suatChieu;
        }

        public async Task UpdateAsync(SuatChieu suatChieu)
        {
            _context.SuatChieus.Update(suatChieu);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var suatChieu = await _context.SuatChieus.FindAsync(id);
            if (suatChieu != null)
            {
                _context.SuatChieus.Remove(suatChieu);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> HasConflictAsync(int maPhong, DateTime ngayChieu, TimeSpan gioBatDau, TimeSpan gioKetThuc, int? excludeId = null)
        {
            var d = ngayChieu.Date;
            var query = _context.SuatChieus.Where(s => s.MaPhong == maPhong && s.NgayChieu == d && s.TrangThai != "Huy");

            if (excludeId.HasValue)
            {
                query = query.Where(s => s.MaSuatChieu != excludeId.Value);
            }

            // Hai khoảng thời gian giao nhau khi: (StartA < EndB) và (EndA > StartB)
            return await query.AnyAsync(s => s.GioBatDau < gioKetThuc && s.GioKetThuc > gioBatDau);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

