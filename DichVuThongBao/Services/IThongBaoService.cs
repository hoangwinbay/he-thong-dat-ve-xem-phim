using DichVuThongBao.DTOs;

namespace DichVuThongBao.Services
{
    public interface IThongBaoService
    {
        Task<List<ThongBaoDTO>> GetByNguoiDungAsync(int maNguoiDung);
        Task<ThongBaoDTO?> GetByIdAsync(int id);
        Task<List<ThongBaoDTO>> GetAllAsync(bool? daDoc = null);
        Task<int> DemSoChuaDocAsync(int maNguoiDung);
        Task<ThongBaoDTO> GuiThongBaoAsync(GuiThongBaoRequestDTO request);
        Task<int> GuiThongBaoChoTatCaAsync(string tieuDe, string noiDung);
        Task<bool> DanhDauDaDocAsync(int id);
        Task<bool> DanhDauTatCaDaDocAsync(int maNguoiDung);
        Task<bool> XoaThongBaoAsync(int id);
    }
}
