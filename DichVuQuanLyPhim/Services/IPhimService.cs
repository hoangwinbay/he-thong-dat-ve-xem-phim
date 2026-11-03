using DichVuQuanLyPhim.DTOs;

namespace DichVuQuanLyPhim.Services
{
    public interface IPhimService
    {
        Task<IEnumerable<ThongTinPhimDTO>> GetAllAsync(string? trangThai = null);
        Task<ThongTinPhimDTO?> GetByIdAsync(int id);
        Task<ThongTinPhimDTO> ThemPhimAsync(ThemPhimDTO dto);
        Task<bool> CapNhatPhimAsync(int id, CapNhatPhimDTO dto);
        Task<bool> CapNhatTrangThaiAsync(int id, string trangThai);
        Task<bool> XoaPhimAsync(int id);
    }
}

