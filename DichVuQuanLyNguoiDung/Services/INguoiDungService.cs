using DichVuQuanLyNguoiDung.DTOs;

namespace DichVuQuanLyNguoiDung.Services
{
    public interface INguoiDungService
    {
        Task<ThongTinNguoiDungDTO> DangKyAsync(DangKyDTO dto);
        Task<ThongTinNguoiDungDTO?> DangNhapAsync(DangNhapDTO dto);
        Task<ThongTinNguoiDungDTO?> GetByIdAsync(int id);
        Task<IEnumerable<ThongTinNguoiDungDTO>> GetAllAsync();
        Task<bool> CapNhatAsync(int id, CapNhatNguoiDungDTO dto);
        Task<bool> PhanQuyenAsync(int id, PhanQuyenDTO dto);
        Task<bool> DoiMatKhauAsync(int id, DoiMatKhauDTO dto);
        Task<bool> XoaAsync(int id);
    }
}

