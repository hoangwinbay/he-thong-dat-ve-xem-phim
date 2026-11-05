using DichVuThanhToan.Models;

namespace DichVuThanhToan.Repositories
{
    public interface IThanhToanRepository
    {
        Task<ThanhToan?> GetByIdAsync(int id);
        Task<ThanhToan?> GetByDatVeIdAsync(int maDatVe);
        Task<List<ThanhToan>> GetAllAsync(string? trangThai = null, string? phuongThuc = null);
        Task<DatVe?> GetDatVeWithDetailsAsync(int maDatVe);
        Task<ThanhToan> CreateAsync(ThanhToan thanhToan);
        Task UpdateAsync(ThanhToan thanhToan);
        Task UpdateDatVeAsync(DatVe datVe);
        Task UpdateGheSuatChieusAsync(List<GheSuatChieu> ghes);
    }
}
