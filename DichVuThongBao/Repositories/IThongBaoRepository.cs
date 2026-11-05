using DichVuThongBao.Models;

namespace DichVuThongBao.Repositories
{
    public interface IThongBaoRepository
    {
        Task<ThongBao?> GetByIdAsync(int id);
        Task<List<ThongBao>> GetByNguoiDungAsync(int maNguoiDung);
        Task<List<ThongBao>> GetAllAsync(bool? daDoc = null);
        Task<int> DemSoChuaDocAsync(int maNguoiDung);
        Task<ThongBao> CreateAsync(ThongBao thongBao);
        Task CreateRangeAsync(List<ThongBao> thongBaos);
        Task UpdateAsync(ThongBao thongBao);
        Task<bool> DeleteAsync(int id);
        Task<List<NguoiDung>> GetAllUsersAsync();
    }
}
