using Microsoft.EntityFrameworkCore;
using DichVuThanhToan.Data;
using DichVuThanhToan.Models;

namespace DichVuThanhToan.Repositories
{
    public class ThanhToanRepository : IThanhToanRepository
    {
        private readonly ThanhToanDbContext _context;

        public ThanhToanRepository(ThanhToanDbContext context)
        {
            _context = context;
        }

        public async Task<ThanhToan?> GetByIdAsync(int id)
        {
            return await _context.ThanhToans
                .Include(tt => tt.DatVe)
                    .ThenInclude(dv => dv!.NguoiDung)
                .Include(tt => tt.DatVe)
                    .ThenInclude(dv => dv!.SuatChieu)
                        .ThenInclude(sc => sc!.Phim)
                .Include(tt => tt.DatVe)
                    .ThenInclude(dv => dv!.SuatChieu)
                        .ThenInclude(sc => sc!.PhongChieu)
                            .ThenInclude(pc => pc!.RapChieu)
                .Include(tt => tt.DatVe)
                    .ThenInclude(dv => dv!.ChiTietDatVes)
                        .ThenInclude(ct => ct.GheSuatChieu)
                            .ThenInclude(gsc => gsc!.GheNgoi)
                .FirstOrDefaultAsync(tt => tt.MaThanhToan == id);
        }

        public async Task<ThanhToan?> GetByDatVeIdAsync(int maDatVe)
        {
            return await _context.ThanhToans
                .Include(tt => tt.DatVe)
                    .ThenInclude(dv => dv!.NguoiDung)
                .Include(tt => tt.DatVe)
                    .ThenInclude(dv => dv!.SuatChieu)
                        .ThenInclude(sc => sc!.Phim)
                .Include(tt => tt.DatVe)
                    .ThenInclude(dv => dv!.SuatChieu)
                        .ThenInclude(sc => sc!.PhongChieu)
                            .ThenInclude(pc => pc!.RapChieu)
                .Include(tt => tt.DatVe)
                    .ThenInclude(dv => dv!.ChiTietDatVes)
                        .ThenInclude(ct => ct.GheSuatChieu)
                            .ThenInclude(gsc => gsc!.GheNgoi)
                .OrderByDescending(tt => tt.NgayThanhToan)
                .FirstOrDefaultAsync(tt => tt.MaDatVe == maDatVe);
        }

        public async Task<List<ThanhToan>> GetAllAsync(string? trangThai = null, string? phuongThuc = null)
        {
            var query = _context.ThanhToans
                .Include(tt => tt.DatVe)
                    .ThenInclude(dv => dv!.NguoiDung)
                .Include(tt => tt.DatVe)
                    .ThenInclude(dv => dv!.SuatChieu)
                        .ThenInclude(sc => sc!.Phim)
                .Include(tt => tt.DatVe)
                    .ThenInclude(dv => dv!.SuatChieu)
                        .ThenInclude(sc => sc!.PhongChieu)
                            .ThenInclude(pc => pc!.RapChieu)
                .Include(tt => tt.DatVe)
                    .ThenInclude(dv => dv!.ChiTietDatVes)
                        .ThenInclude(ct => ct.GheSuatChieu)
                            .ThenInclude(gsc => gsc!.GheNgoi)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(trangThai))
            {
                query = query.Where(tt => tt.TrangThai == trangThai);
            }

            if (!string.IsNullOrWhiteSpace(phuongThuc))
            {
                query = query.Where(tt => tt.PhuongThuc == phuongThuc);
            }

            return await query.OrderByDescending(tt => tt.NgayThanhToan).ToListAsync();
        }

        public async Task<DatVe?> GetDatVeWithDetailsAsync(int maDatVe)
        {
            return await _context.DatVes
                .Include(dv => dv.NguoiDung)
                .Include(dv => dv.ChiTietDatVes)
                    .ThenInclude(ct => ct.GheSuatChieu)
                .FirstOrDefaultAsync(dv => dv.MaDatVe == maDatVe);
        }

        public async Task<ThanhToan> CreateAsync(ThanhToan thanhToan)
        {
            _context.ThanhToans.Add(thanhToan);
            await _context.SaveChangesAsync();
            return thanhToan;
        }

        public async Task UpdateAsync(ThanhToan thanhToan)
        {
            _context.ThanhToans.Update(thanhToan);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDatVeAsync(DatVe datVe)
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
