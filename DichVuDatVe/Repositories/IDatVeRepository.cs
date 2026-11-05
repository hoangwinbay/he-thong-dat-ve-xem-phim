using DichVuDatVe.Models;

namespace DichVuDatVe.Repositories
{
    public interface IDatVeRepository
    {
        Task<DatVe?> GetByIdAsync(int id);
        Task<List<DatVe>> GetAllAsync(string? trangThai = null, int? maSuatChieu = null);
        Task<List<DatVe>> GetByNguoiDungAsync(int maNguoiDung);
        Task<List<GheSuatChieu>> GetGheSuatChieusAsync(List<int> ids);
        Task<DatVe> CreateAsync(DatVe datVe);
        Task UpdateAsync(DatVe datVe);
        Task UpdateGheSuatChieusAsync(List<GheSuatChieu> ghes);
    }
}
