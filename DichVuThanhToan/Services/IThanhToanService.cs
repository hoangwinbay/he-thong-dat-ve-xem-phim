using DichVuThanhToan.DTOs;

namespace DichVuThanhToan.Services
{
    public interface IThanhToanService
    {
        Task<KetQuaThanhToanDTO> TaoVaXacNhanGiaoDichAsync(TaoGiaoDichThanhToanDTO dto);
        Task<ThanhToanDTO?> GetByIdAsync(int id);
        Task<ThanhToanDTO?> GetByDatVeIdAsync(int maDatVe);
        Task<List<ThanhToanDTO>> GetAllAsync(string? trangThai = null, string? phuongThuc = null);
        Task<bool> CapNhatTrangThaiAsync(int id, string trangThai, string? maGiaoDich = null);
        Task<bool> HoanTienAsync(int id);
    }
}
