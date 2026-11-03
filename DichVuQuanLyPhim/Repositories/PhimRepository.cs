using Microsoft.EntityFrameworkCore;
using DichVuQuanLyPhim.Data;
using DichVuQuanLyPhim.Models;

namespace DichVuQuanLyPhim.Repositories
{
    public class PhimRepository : IPhimRepository
    {
        private readonly PhimDbContext _context;

        public PhimRepository(PhimDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Phim>> GetAllAsync(string? trangThai = null)
        {
            var query = _context.Phims
                .Include(p => p.PhimTheLoais)
                .ThenInclude(pt => pt.TheLoai)
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(trangThai))
            {
                query = query.Where(p => p.TrangThai == trangThai);
            }

            return await query.ToListAsync();
        }

        public async Task<Phim?> GetByIdAsync(int id)
        {
            return await _context.Phims
                .Include(p => p.PhimTheLoais)
                .ThenInclude(pt => pt.TheLoai)
                .FirstOrDefaultAsync(p => p.MaPhim == id);
        }

        public async Task<Phim> AddAsync(Phim phim, List<int>? theLoaiIds)
        {
            await _context.Phims.AddAsync(phim);
            await _context.SaveChangesAsync();

            if (theLoaiIds != null && theLoaiIds.Any())
            {
                foreach (var tlId in theLoaiIds)
                {
                    _context.PhimTheLoais.Add(new PhimTheLoai { MaPhim = phim.MaPhim, MaTheLoai = tlId });
                }
                await _context.SaveChangesAsync();
            }

            return phim;
        }

        public async Task UpdateAsync(Phim phim, List<int>? theLoaiIds)
        {
            _context.Phims.Update(phim);

            if (theLoaiIds != null)
            {
                var existing = await _context.PhimTheLoais.Where(pt => pt.MaPhim == phim.MaPhim).ToListAsync();
                _context.PhimTheLoais.RemoveRange(existing);

                foreach (var tlId in theLoaiIds)
                {
                    _context.PhimTheLoais.Add(new PhimTheLoai { MaPhim = phim.MaPhim, MaTheLoai = tlId });
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var phim = await _context.Phims.FindAsync(id);
            if (phim != null)
            {
                var theLoais = await _context.PhimTheLoais.Where(pt => pt.MaPhim == id).ToListAsync();
                _context.PhimTheLoais.RemoveRange(theLoais);

                _context.Phims.Remove(phim);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

