using DichVuQuanLyNguoiDung.Models;

namespace DichVuQuanLyNguoiDung.Repositories
{
    public interface INguoiDungRepository
    {
        Task<IEnumerable<NguoiDung>> GetAllAsync();
        Task<NguoiDung?> GetByIdAsync(int id);
        Task<NguoiDung?> GetByEmailAsync(string email);
        Task<bool> ExistsByEmailAsync(string email);
        Task<NguoiDung> AddAsync(NguoiDung nguoiDung);
        Task UpdateAsync(NguoiDung nguoiDung);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}

