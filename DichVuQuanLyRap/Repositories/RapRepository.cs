using Microsoft.EntityFrameworkCore;
using DichVuQuanLyRap.Data;
using DichVuQuanLyRap.Models;

namespace DichVuQuanLyRap.Repositories
{
    public class RapRepository : IRapRepository
    {
        private readonly RapDbContext _context;

        public RapRepository(RapDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RapChieu>> GetAllRapAsync()
        {
            return await _context.RapChieus
                .Include(r => r.PhongChieus)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<RapChieu?> GetRapByIdAsync(int maRap)
        {
            return await _context.RapChieus
                .Include(r => r.PhongChieus)
                .FirstOrDefaultAsync(r => r.MaRap == maRap);
        }

        public async Task<RapChieu> AddRapAsync(RapChieu rap)
        {
            await _context.RapChieus.AddAsync(rap);
            await _context.SaveChangesAsync();
            return rap;
        }

        public async Task UpdateRapAsync(RapChieu rap)
        {
            _context.RapChieus.Update(rap);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRapAsync(int maRap)
        {
            var rap = await _context.RapChieus.FindAsync(maRap);
            if (rap != null)
            {
                _context.RapChieus.Remove(rap);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<PhongChieu>> GetPhongChieuByRapAsync(int maRap)
        {
            return await _context.PhongChieus
                .Where(p => p.MaRap == maRap)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<PhongChieu?> GetPhongChieuByIdAsync(int maPhong)
        {
            return await _context.PhongChieus
                .Include(p => p.RapChieu)
                .FirstOrDefaultAsync(p => p.MaPhong == maPhong);
        }

        public async Task<PhongChieu> AddPhongChieuAsync(PhongChieu phong)
        {
            await _context.PhongChieus.AddAsync(phong);
            await _context.SaveChangesAsync();
            return phong;
        }

        public async Task UpdatePhongChieuAsync(PhongChieu phong)
        {
            _context.PhongChieus.Update(phong);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePhongChieuAsync(int maPhong)
        {
            var phong = await _context.PhongChieus.FindAsync(maPhong);
            if (phong != null)
            {
                _context.PhongChieus.Remove(phong);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

