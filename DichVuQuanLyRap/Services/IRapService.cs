using DichVuQuanLyRap.DTOs;

namespace DichVuQuanLyRap.Services
{
    public interface IRapService
    {
        Task<IEnumerable<ThongTinRapDTO>> GetAllRapAsync();
        Task<ThongTinRapDTO?> GetRapByIdAsync(int maRap);
        Task<ThongTinRapDTO> ThemRapAsync(ThemRapDTO dto);
        Task<bool> CapNhatRapAsync(int maRap, CapNhatRapDTO dto);
        Task<bool> XoaRapAsync(int maRap);

        Task<IEnumerable<ThongTinPhongChieuDTO>> GetPhongChieuByRapAsync(int maRap);
        Task<ThongTinPhongChieuDTO?> GetPhongChieuByIdAsync(int maPhong);
        Task<ThongTinPhongChieuDTO> ThemPhongChieuAsync(ThemPhongChieuDTO dto);
        Task<bool> CapNhatPhongChieuAsync(int maPhong, CapNhatPhongChieuDTO dto);
        Task<bool> XoaPhongChieuAsync(int maPhong);
    }
}

