using Microsoft.EntityFrameworkCore;
using DichVuDatVe.Data;
using DichVuDatVe.Models;

namespace DichVuDatVe.Repositories
{
    public class DatVeRepository : IDatVeRepository
    {
        private readonly DatVeDbContext _context;

        public DatVeRepository(DatVeDbContext context)
        {
            _context = context;
        }

        public async Task<DatVe?> GetByIdAsync(int id)
        {
            return await _context.DatVes
                .Include(dv => dv.NguoiDung)
                .Include(dv => dv.SuatChieu)
                    .ThenInclude(sc => sc!.Phim)
                .Include(dv => dv.SuatChieu)
                    .ThenInclude(sc => sc!.PhongChieu)
                        .ThenInclude(pc => pc!.RapChieu)
                .Include(dv => dv.ChiTietDatVes)
                    .ThenInclude(ct => ct.GheSuatChieu)
                        .ThenInclude(gsc => gsc!.GheNgoi)
                .FirstOrDefaultAsync(dv => dv.MaDatVe == id);
        }

        public async Task<List<DatVe>> GetAllAsync(string? trangThai = null, int? maSuatChieu = null)
        {
            var query = _context.DatVes
                .Include(dv => dv.NguoiDung)
                .Include(dv => dv.SuatChieu)
                    .ThenInclude(sc => sc!.Phim)
                .Include(dv => dv.SuatChieu)
                    .ThenInclude(sc => sc!.PhongChieu)
                        .ThenInclude(pc => pc!.RapChieu)
                .Include(dv => dv.ChiTietDatVes)
                    .ThenInclude(ct => ct.GheSuatChieu)
                        .ThenInclude(gsc => gsc!.GheNgoi)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(trangThai))
            {
                query = query.Where(dv => dv.TrangThai == trangThai);
            }

            if (maSuatChieu.HasValue)
            {
                query = query.Where(dv => dv.MaSuatChieu == maSuatChieu.Value);
            }

            return await query.OrderByDescending(dv => dv.NgayDat).ToListAsync();
        }

        public async Task<List<DatVe>> GetByNguoiDungAsync(int maNguoiDung)
        {
            return await _context.DatVes
                .Include(dv => dv.NguoiDung)
                .Include(dv => dv.SuatChieu)
                    .ThenInclude(sc => sc!.Phim)
                .Include(dv => dv.SuatChieu)
                    .ThenInclude(sc => sc!.PhongChieu)
                        .ThenInclude(pc => pc!.RapChieu)
                .Include(dv => dv.ChiTietDatVes)
                    .ThenInclude(ct => ct.GheSuatChieu)
                        .ThenInclude(gsc => gsc!.GheNgoi)
                .Where(dv => dv.MaNguoiDung == maNguoiDung)
                .OrderByDescending(dv => dv.NgayDat)
                .ToListAsync();
        }

        public async Task<List<GheSuatChieu>> GetGheSuatChieusAsync(List<int> ids)
        {
            return await _context.GheSuatChieus
                .Include(g => g.GheNgoi)
                .Where(g => ids.Contains(g.MaGheSuatChieu))
                .ToListAsync();
        }

        public async Task<DatVe> CreateAsync(DatVe datVe)
        {
            _context.DatVes.Add(datVe);
            await _context.SaveChangesAsync();
            return datVe;
        }

        public async Task UpdateAsync(DatVe datVe)
        {
            _context.DatVes.Update(datVe);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGheSuatChieusAsync(List<GheSuatChieu> ghes)
        {
            _context.GheSuatChieus.UpdateRange(ghes);
            await _context.SaveChangesAsync();
        }
    }
}
