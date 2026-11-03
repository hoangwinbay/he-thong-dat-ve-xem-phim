using DichVuQuanLyRap.Models;

namespace DichVuQuanLyRap.Repositories
{
    public interface IRapRepository
    {
        Task<IEnumerable<RapChieu>> GetAllRapAsync();
        Task<RapChieu?> GetRapByIdAsync(int maRap);
        Task<RapChieu> AddRapAsync(RapChieu rap);
        Task UpdateRapAsync(RapChieu rap);
        Task DeleteRapAsync(int maRap);

        Task<IEnumerable<PhongChieu>> GetPhongChieuByRapAsync(int maRap);
        Task<PhongChieu?> GetPhongChieuByIdAsync(int maPhong);
        Task<PhongChieu> AddPhongChieuAsync(PhongChieu phong);
        Task UpdatePhongChieuAsync(PhongChieu phong);
        Task DeletePhongChieuAsync(int maPhong);

        Task SaveChangesAsync();
    }
}

