using Microsoft.EntityFrameworkCore;
using DichVuQuanLyVeDienTu.Data;
using DichVuQuanLyVeDienTu.Models;

namespace DichVuQuanLyVeDienTu.Repositories
{
    public class VeDienTuRepository : IVeDienTuRepository
    {
        private readonly VeDienTuDbContext _context;

        public VeDienTuRepository(VeDienTuDbContext context)
        {
            _context = context;
        }

        public async Task<VeDienTu?> GetByIdAsync(int id)
        {
            return await _context.VeDienTus
                .Include(v => v.ChiTietDatVe)
                    .ThenInclude(ct => ct!.DatVe)
                        .ThenInclude(dv => dv!.NguoiDung)
                .Include(v => v.ChiTietDatVe)
                    .ThenInclude(ct => ct!.DatVe)
                        .ThenInclude(dv => dv!.SuatChieu)
                            .ThenInclude(sc => sc!.Phim)
                .Include(v => v.ChiTietDatVe)
                    .ThenInclude(ct => ct!.DatVe)
                        .ThenInclude(dv => dv!.SuatChieu)
                            .ThenInclude(sc => sc!.PhongChieu)
                                .ThenInclude(pc => pc!.RapChieu)
                .Include(v => v.ChiTietDatVe)
                    .ThenInclude(ct => ct!.GheSuatChieu)
                        .ThenInclude(gsc => gsc!.GheNgoi)
                .FirstOrDefaultAsync(v => v.MaVe == id);
        }

        public async Task<VeDienTu?> GetByMaQRAsync(string maQR)
        {
            var q = maQR.Trim();
            return await _context.VeDienTus
                .Include(v => v.ChiTietDatVe)
                    .ThenInclude(ct => ct!.DatVe)
                        .ThenInclude(dv => dv!.NguoiDung)
                .Include(v => v.ChiTietDatVe)
                    .ThenInclude(ct => ct!.DatVe)
                        .ThenInclude(dv => dv!.SuatChieu)
                            .ThenInclude(sc => sc!.Phim)
                .Include(v => v.ChiTietDatVe)
                    .ThenInclude(ct => ct!.DatVe)
                        .ThenInclude(dv => dv!.SuatChieu)
                            .ThenInclude(sc => sc!.PhongChieu)
                                .ThenInclude(pc => pc!.RapChieu)
                .Include(v => v.ChiTietDatVe)
                    .ThenInclude(ct => ct!.GheSuatChieu)
                        .ThenInclude(gsc => gsc!.GheNgoi)
                .FirstOrDefaultAsync(v => v.MaQR == q || v.MaVe.ToString() == q);
        }

        public async Task<List<VeDienTu>> GetByDatVeIdAsync(int maDatVe)
        {
            return await _context.VeDienTus
                .Include(v => v.ChiTietDatVe)
                    .ThenInclude(ct => ct!.DatVe)
                        .ThenInclude(dv => dv!.NguoiDung)
                .Include(v => v.ChiTietDatVe)
                    .ThenInclude(ct => ct!.DatVe)
                        .ThenInclude(dv => dv!.SuatChieu)
                            .ThenInclude(sc => sc!.Phim)
                .Include(v => v.ChiTietDatVe)
                    .ThenInclude(ct => ct!.DatVe)
                        .ThenInclude(dv => dv!.SuatChieu)
                            .ThenInclude(sc => sc!.PhongChieu)
                                .ThenInclude(pc => pc!.RapChieu)
                .Include(v => v.ChiTietDatVe)
                    .ThenInclude(ct => ct!.GheSuatChieu)
                        .ThenInclude(gsc => gsc!.GheNgoi)
                .Where(v => v.ChiTietDatVe != null && v.ChiTietDatVe.MaDatVe == maDatVe)
                .OrderBy(v => v.MaVe)
                .ToListAsync();
        }

        public async Task<List<VeDienTu>> GetAllAsync(string? trangThai = null)
        {
            var query = _context.VeDienTus
                .Include(v => v.ChiTietDatVe)
                    .ThenInclude(ct => ct!.DatVe)
                        .ThenInclude(dv => dv!.NguoiDung)
                .Include(v => v.ChiTietDatVe)
                    .ThenInclude(ct => ct!.DatVe)
                        .ThenInclude(dv => dv!.SuatChieu)
                            .ThenInclude(sc => sc!.Phim)
                .Include(v => v.ChiTietDatVe)
                    .ThenInclude(ct => ct!.DatVe)
                        .ThenInclude(dv => dv!.SuatChieu)
                            .ThenInclude(sc => sc!.PhongChieu)
                                .ThenInclude(pc => pc!.RapChieu)
                .Include(v => v.ChiTietDatVe)
                    .ThenInclude(ct => ct!.GheSuatChieu)
                        .ThenInclude(gsc => gsc!.GheNgoi)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(trangThai))
            {
                query = query.Where(v => v.TrangThai == trangThai);
            }

            return await query.OrderByDescending(v => v.NgayPhatHanh).ToListAsync();
        }

        public async Task<DatVe?> GetDatVeWithDetailsAsync(int maDatVe)
        {
            return await _context.DatVes
                .Include(dv => dv.ChiTietDatVes)
                    .ThenInclude(ct => ct.VeDienTu)
                .FirstOrDefaultAsync(dv => dv.MaDatVe == maDatVe);
        }

        public async Task<VeDienTu> CreateAsync(VeDienTu veDienTu)
        {
            _context.VeDienTus.Add(veDienTu);
            await _context.SaveChangesAsync();
            return veDienTu;
        }

        public async Task CreateRangeAsync(List<VeDienTu> veDienTus)
        {
            _context.VeDienTus.AddRange(veDienTus);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(VeDienTu veDienTu)
        {
            _context.VeDienTus.Update(veDienTu);
            await _context.SaveChangesAsync();
        }
    }
}
