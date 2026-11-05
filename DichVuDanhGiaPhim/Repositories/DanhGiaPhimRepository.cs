using Microsoft.EntityFrameworkCore;
using DichVuDanhGiaPhim.Data;
using DichVuDanhGiaPhim.Models;

namespace DichVuDanhGiaPhim.Repositories
{
    public class DanhGiaPhimRepository : IDanhGiaPhimRepository
    {
        private readonly DanhGiaPhimDbContext _context;

        public DanhGiaPhimRepository(DanhGiaPhimDbContext context)
        {
            _context = context;
        }

        public async Task<DanhGiaPhim?> GetByIdAsync(int id)
        {
            return await _context.DanhGiaPhims
                .Include(d => d.NguoiDung)
                .Include(d => d.Phim)
                .FirstOrDefaultAsync(d => d.MaDanhGia == id);
        }

        public async Task<DanhGiaPhim?> GetByUserAndMovieAsync(int maNguoiDung, int maPhim)
        {
            return await _context.DanhGiaPhims
                .Include(d => d.NguoiDung)
                .Include(d => d.Phim)
                .FirstOrDefaultAsync(d => d.MaNguoiDung == maNguoiDung && d.MaPhim == maPhim);
        }

        public async Task<List<DanhGiaPhim>> GetByPhimIdAsync(int maPhim)
        {
            return await _context.DanhGiaPhims
                .Include(d => d.NguoiDung)
                .Include(d => d.Phim)
                .Where(d => d.MaPhim == maPhim)
                .OrderByDescending(d => d.NgayDanhGia)
                .ToListAsync();
        }

        public async Task<List<DanhGiaPhim>> GetByNguoiDungAsync(int maNguoiDung)
        {
            return await _context.DanhGiaPhims
                .Include(d => d.NguoiDung)
                .Include(d => d.Phim)
                .Where(d => d.MaNguoiDung == maNguoiDung)
                .OrderByDescending(d => d.NgayDanhGia)
                .ToListAsync();
        }

        public async Task<List<DanhGiaPhim>> GetAllAsync(int? soSao = null, int? maPhim = null)
        {
            var query = _context.DanhGiaPhims
                .Include(d => d.NguoiDung)
                .Include(d => d.Phim)
                .AsQueryable();

            if (soSao.HasValue)
            {
                query = query.Where(d => d.SoSao == soSao.Value);
            }

            if (maPhim.HasValue)
            {
                query = query.Where(d => d.MaPhim == maPhim.Value);
            }

            return await query.OrderByDescending(d => d.NgayDanhGia).ToListAsync();
        }

        public async Task<DanhGiaPhim> CreateAsync(DanhGiaPhim danhGia)
        {
            _context.DanhGiaPhims.Add(danhGia);
            await _context.SaveChangesAsync();
            return danhGia;
        }

        public async Task UpdateAsync(DanhGiaPhim danhGia)
        {
            _context.DanhGiaPhims.Update(danhGia);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context.DanhGiaPhims.FindAsync(id);
            if (item == null) return false;

            _context.DanhGiaPhims.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Phim?> GetPhimByIdAsync(int maPhim)
        {
            return await _context.Phims.FindAsync(maPhim);
        }
    }
}
