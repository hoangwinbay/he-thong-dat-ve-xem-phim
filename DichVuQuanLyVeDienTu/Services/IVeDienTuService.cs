using DichVuQuanLyVeDienTu.DTOs;

namespace DichVuQuanLyVeDienTu.Services
{
    public interface IVeDienTuService
    {
        Task<List<VeDienTuDTO>> PhatHanhVeChoDonDatVeAsync(int maDatVe);
        Task<KetQuaSoatVeDTO> KiemTraVaSoatVeAsync(string maQR);
        Task<VeDienTuDTO?> GetByIdAsync(int id);
        Task<VeDienTuDTO?> GetByMaQRAsync(string maQR);
        Task<List<VeDienTuDTO>> GetByDatVeIdAsync(int maDatVe);
        Task<List<VeDienTuDTO>> GetAllAsync(string? trangThai = null);
        Task<bool> HuyVeAsync(int id);
    }
}
