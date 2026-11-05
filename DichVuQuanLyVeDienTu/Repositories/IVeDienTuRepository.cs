using DichVuQuanLyVeDienTu.Models;

namespace DichVuQuanLyVeDienTu.Repositories
{
    public interface IVeDienTuRepository
    {
        Task<VeDienTu?> GetByIdAsync(int id);
        Task<VeDienTu?> GetByMaQRAsync(string maQR);
        Task<List<VeDienTu>> GetByDatVeIdAsync(int maDatVe);
        Task<List<VeDienTu>> GetAllAsync(string? trangThai = null);
        Task<DatVe?> GetDatVeWithDetailsAsync(int maDatVe);
        Task<VeDienTu> CreateAsync(VeDienTu veDienTu);
        Task CreateRangeAsync(List<VeDienTu> veDienTus);
        Task UpdateAsync(VeDienTu veDienTu);
    }
}
