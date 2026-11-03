using DichVuQuanLyPhim.Models;

namespace DichVuQuanLyPhim.Repositories
{
    public interface IPhimRepository
    {
        Task<IEnumerable<Phim>> GetAllAsync(string? trangThai = null);
        Task<Phim?> GetByIdAsync(int id);
        Task<Phim> AddAsync(Phim phim, List<int>? theLoaiIds);
        Task UpdateAsync(Phim phim, List<int>? theLoaiIds);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}

