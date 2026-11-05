using DichVuDanhGiaPhim.DTOs;

namespace DichVuDanhGiaPhim.Services
{
    public interface IDanhGiaPhimService
    {
        Task<List<DanhGiaPhimDTO>> GetByPhimIdAsync(int maPhim);
        Task<List<DanhGiaPhimDTO>> GetByNguoiDungAsync(int maNguoiDung);
        Task<DanhGiaPhimDTO?> GetByIdAsync(int id);
        Task<List<DanhGiaPhimDTO>> GetAllAsync(int? soSao = null, int? maPhim = null);
        Task<ThongKeDanhGiaPhimDTO> GetThongKePhimAsync(int maPhim);
        Task<DanhGiaPhimDTO> TaoHoacCapNhatDanhGiaAsync(TaoDanhGiaRequestDTO request);
        Task<bool> XoaDanhGiaAsync(int id);
    }
}
