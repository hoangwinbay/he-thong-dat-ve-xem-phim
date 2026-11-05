using Microsoft.EntityFrameworkCore;
using DichVuThongBao.Data;
using DichVuThongBao.Models;

namespace DichVuThongBao.Repositories
{
    public class ThongBaoRepository : IThongBaoRepository
    {
        private readonly ThongBaoDbContext _context;

        public ThongBaoRepository(ThongBaoDbContext context)
        {
            _context = context;
        }

        public async Task<ThongBao?> GetByIdAsync(int id)
        {
            return await _context.ThongBaos
                .Include(tb => tb.NguoiDung)
                .FirstOrDefaultAsync(tb => tb.MaThongBao == id);
        }

        public async Task<List<ThongBao>> GetByNguoiDungAsync(int maNguoiDung)
        {
            return await _context.ThongBaos
                .Include(tb => tb.NguoiDung)
                .Where(tb => tb.MaNguoiDung == maNguoiDung)
                .OrderByDescending(tb => tb.NgayGui)
                .ToListAsync();
        }

        public async Task<List<ThongBao>> GetAllAsync(bool? daDoc = null)
        {
            var query = _context.ThongBaos
                .Include(tb => tb.NguoiDung)
                .AsQueryable();

            if (daDoc.HasValue)
            {
                query = query.Where(tb => tb.DaDoc == daDoc.Value);
            }

            return await query.OrderByDescending(tb => tb.NgayGui).ToListAsync();
        }

        public async Task<int> DemSoChuaDocAsync(int maNguoiDung)
        {
            return await _context.ThongBaos
                .CountAsync(tb => tb.MaNguoiDung == maNguoiDung && !tb.DaDoc);
        }

        public async Task<ThongBao> CreateAsync(ThongBao thongBao)
        {
            _context.ThongBaos.Add(thongBao);
            await _context.SaveChangesAsync();
            return thongBao;
        }

        public async Task CreateRangeAsync(List<ThongBao> thongBaos)
        {
            _context.ThongBaos.AddRange(thongBaos);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ThongBao thongBao)
        {
            _context.ThongBaos.Update(thongBao);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context.ThongBaos.FindAsync(id);
            if (item == null) return false;

            _context.ThongBaos.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<NguoiDung>> GetAllUsersAsync()
        {
            return await _context.NguoiDungs.ToListAsync();
        }
    }
}
