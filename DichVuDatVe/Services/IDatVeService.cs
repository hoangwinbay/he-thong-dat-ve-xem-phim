using DichVuDatVe.DTOs;

namespace DichVuDatVe.Services
{
    public interface IDatVeService
    {
        Task<DonDatVeDTO> TaoDonDatVeAsync(TaoDonDatVeDTO dto);
        Task<DonDatVeDTO?> GetByIdAsync(int id);
        Task<List<DonDatVeDTO>> GetAllAsync(string? trangThai = null, int? maSuatChieu = null);
        Task<List<DonDatVeDTO>> GetByNguoiDungAsync(int maNguoiDung);
        Task<bool> CapNhatTrangThaiAsync(int id, string trangThai);
        Task<bool> HuyDonDatVeAsync(int id);
    }
}
