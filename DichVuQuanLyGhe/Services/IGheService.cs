using DichVuQuanLyGhe.DTOs;

namespace DichVuQuanLyGhe.Services
{
    public interface IGheService
    {
        Task<IEnumerable<ThongTinGheDTO>> GetGheByPhongAsync(int maPhong);
        Task<ThongTinGheDTO> ThemGheAsync(ThemGheDTO dto);
        Task<bool> CapNhatGheAsync(int maGhe, CapNhatGheDTO dto);
        Task<bool> XoaGheAsync(int maGhe);
        Task<int> TaoSoDoGheMacDinhAsync(int maPhong, int soHang = 6, int soGheMoiHang = 8);

        Task<SoDoGheSuatChieuDTO> GetSoDoGheSuatChieuAsync(int maSuatChieu);
        Task<int> KhoiTaoGheSuatChieuAsync(KhoiTaoGheSuatChieuDTO dto);
        Task<bool> GiuGheAsync(GiuGheRequestDTO dto);
        Task<bool> GiaiPhongGheAsync(GiaiPhongGheRequestDTO dto);
        Task<bool> DatGheAsync(List<int> danhSachMaGheSuatChieu);
    }
}

