using DichVuQuanLySuatChieu.DTOs;
using DichVuQuanLySuatChieu.Models;

namespace DichVuQuanLySuatChieu.Repositories
{
    public interface ISuatChieuRepository
    {
        Task<IEnumerable<SuatChieu>> GetListAsync(LocSuatChieuDTO filter);
        Task<SuatChieu?> GetByIdAsync(int id);
        Task<SuatChieu> AddAsync(SuatChieu suatChieu);
        Task UpdateAsync(SuatChieu suatChieu);
        Task DeleteAsync(int id);
        Task<bool> HasConflictAsync(int maPhong, DateTime ngayChieu, TimeSpan gioBatDau, TimeSpan gioKetThuc, int? excludeId = null);
        Task SaveChangesAsync();
    }
}

