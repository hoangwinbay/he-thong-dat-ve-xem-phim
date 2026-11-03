using Microsoft.EntityFrameworkCore;
using DichVuQuanLyNguoiDung.Data;
using DichVuQuanLyNguoiDung.Models;

namespace DichVuQuanLyNguoiDung.Repositories
{
    public class NguoiDungRepository : INguoiDungRepository
    {
        private readonly NguoiDungDbContext _context;

        public NguoiDungRepository(NguoiDungDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<NguoiDung>> GetAllAsync()
        {
            return await _context.NguoiDungs.AsNoTracking().ToListAsync();
        }

        public async Task<NguoiDung?> GetByIdAsync(int id)
        {
            return await _context.NguoiDungs.FindAsync(id);
        }

        public async Task<NguoiDung?> GetByEmailAsync(string email)
        {
            return await _context.NguoiDungs.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.NguoiDungs.AnyAsync(u => u.Email == email);
        }

        public async Task<NguoiDung> AddAsync(NguoiDung nguoiDung)
        {
            await _context.NguoiDungs.AddAsync(nguoiDung);
            await _context.SaveChangesAsync();
            return nguoiDung;
        }

        public async Task UpdateAsync(NguoiDung nguoiDung)
        {
            _context.NguoiDungs.Update(nguoiDung);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _context.NguoiDungs.FindAsync(id);
            if (user != null)
            {
                _context.NguoiDungs.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

