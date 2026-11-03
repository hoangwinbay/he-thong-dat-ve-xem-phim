using DichVuQuanLySuatChieu.DTOs;

namespace DichVuQuanLySuatChieu.Services
{
    public interface ISuatChieuService
    {
        Task<IEnumerable<ThongTinSuatChieuDTO>> GetListAsync(LocSuatChieuDTO filter);
        Task<ThongTinSuatChieuDTO?> GetByIdAsync(int id);
        Task<ThongTinSuatChieuDTO> ThemSuatChieuAsync(ThemSuatChieuDTO dto);
        Task<bool> CapNhatSuatChieuAsync(int id, CapNhatSuatChieuDTO dto);
        Task<bool> CapNhatTrangThaiAsync(int id, string trangThai);
        Task<bool> XoaSuatChieuAsync(int id);
    }
}

