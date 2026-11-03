using Microsoft.EntityFrameworkCore;
using DichVuQuanLyGhe.Data;
using DichVuQuanLyGhe.Models;

namespace DichVuQuanLyGhe.Repositories
{
    public class GheRepository : IGheRepository
    {
        private readonly GheDbContext _context;

        public GheRepository(GheDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GheNgoi>> GetGheByPhongAsync(int maPhong)
        {
            return await _context.GheNgois
                .Where(g => g.MaPhong == maPhong)
                .OrderBy(g => g.HangGhe)
                .ThenBy(g => g.SoGhe)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<GheNgoi?> GetGheByIdAsync(int maGhe)
        {
            return await _context.GheNgois.FindAsync(maGhe);
        }

        public async Task<GheNgoi> AddGheAsync(GheNgoi ghe)
        {
            await _context.GheNgois.AddAsync(ghe);
            await _context.SaveChangesAsync();
            return ghe;
        }

        public async Task AddGheRangeAsync(IEnumerable<GheNgoi> ghes)
        {
            await _context.GheNgois.AddRangeAsync(ghes);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGheAsync(GheNgoi ghe)
        {
            _context.GheNgois.Update(ghe);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGheAsync(int maGhe)
        {
            var ghe = await _context.GheNgois.FindAsync(maGhe);
            if (ghe != null)
            {
                _context.GheNgois.Remove(ghe);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<GheSuatChieu>> GetGheBySuatChieuAsync(int maSuatChieu)
        {
            return await _context.GheSuatChieus
                .Include(gs => gs.GheNgoi)
                .Where(gs => gs.MaSuatChieu == maSuatChieu)
                .OrderBy(gs => gs.GheNgoi!.HangGhe)
                .ThenBy(gs => gs.GheNgoi!.SoGhe)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<GheSuatChieu?> GetGheSuatChieuByIdAsync(int maGheSuatChieu)
        {
            return await _context.GheSuatChieus
                .Include(gs => gs.GheNgoi)
                .FirstOrDefaultAsync(gs => gs.MaGheSuatChieu == maGheSuatChieu);
        }

        public async Task<List<GheSuatChieu>> GetGheSuatChieuByIdsAsync(IEnumerable<int> ids)
        {
            return await _context.GheSuatChieus
                .Include(gs => gs.GheNgoi)
                .Where(gs => ids.Contains(gs.MaGheSuatChieu))
                .ToListAsync();
        }

        public async Task AddGheSuatChieuRangeAsync(IEnumerable<GheSuatChieu> list)
        {
            await _context.GheSuatChieus.AddRangeAsync(list);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGheSuatChieuAsync(GheSuatChieu gheSuatChieu)
        {
            _context.GheSuatChieus.Update(gheSuatChieu);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGheSuatChieuRangeAsync(IEnumerable<GheSuatChieu> list)
        {
            _context.GheSuatChieus.UpdateRange(list);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

