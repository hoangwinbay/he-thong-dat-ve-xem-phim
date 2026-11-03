using DichVuQuanLyGhe.Models;

namespace DichVuQuanLyGhe.Repositories
{
    public interface IGheRepository
    {
        Task<IEnumerable<GheNgoi>> GetGheByPhongAsync(int maPhong);
        Task<GheNgoi?> GetGheByIdAsync(int maGhe);
        Task<GheNgoi> AddGheAsync(GheNgoi ghe);
        Task AddGheRangeAsync(IEnumerable<GheNgoi> ghes);
        Task UpdateGheAsync(GheNgoi ghe);
        Task DeleteGheAsync(int maGhe);

        Task<IEnumerable<GheSuatChieu>> GetGheBySuatChieuAsync(int maSuatChieu);
        Task<GheSuatChieu?> GetGheSuatChieuByIdAsync(int maGheSuatChieu);
        Task<List<GheSuatChieu>> GetGheSuatChieuByIdsAsync(IEnumerable<int> ids);
        Task AddGheSuatChieuRangeAsync(IEnumerable<GheSuatChieu> list);
        Task UpdateGheSuatChieuAsync(GheSuatChieu gheSuatChieu);
        Task UpdateGheSuatChieuRangeAsync(IEnumerable<GheSuatChieu> list);

        Task SaveChangesAsync();
    }
}

