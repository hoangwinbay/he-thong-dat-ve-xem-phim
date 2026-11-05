using DichVuDanhGiaPhim.Models;

namespace DichVuDanhGiaPhim.Repositories
{
    public interface IDanhGiaPhimRepository
    {
        Task<DanhGiaPhim?> GetByIdAsync(int id);
        Task<DanhGiaPhim?> GetByUserAndMovieAsync(int maNguoiDung, int maPhim);
        Task<List<DanhGiaPhim>> GetByPhimIdAsync(int maPhim);
        Task<List<DanhGiaPhim>> GetByNguoiDungAsync(int maNguoiDung);
        Task<List<DanhGiaPhim>> GetAllAsync(int? soSao = null, int? maPhim = null);
        Task<DanhGiaPhim> CreateAsync(DanhGiaPhim danhGia);
        Task UpdateAsync(DanhGiaPhim danhGia);
        Task<bool> DeleteAsync(int id);
        Task<Phim?> GetPhimByIdAsync(int maPhim);
    }
}
