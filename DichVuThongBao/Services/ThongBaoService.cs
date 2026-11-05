using DichVuThongBao.DTOs;
using DichVuThongBao.Models;
using DichVuThongBao.Repositories;

namespace DichVuThongBao.Services
{
    public class ThongBaoService : IThongBaoService
    {
        private readonly IThongBaoRepository _repo;

        public ThongBaoService(IThongBaoRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<ThongBaoDTO>> GetByNguoiDungAsync(int maNguoiDung)
        {
            var list = await _repo.GetByNguoiDungAsync(maNguoiDung);
            return list.Select(MapToDTO).ToList();
        }

        public async Task<ThongBaoDTO?> GetByIdAsync(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            return item != null ? MapToDTO(item) : null;
        }

        public async Task<List<ThongBaoDTO>> GetAllAsync(bool? daDoc = null)
        {
            var list = await _repo.GetAllAsync(daDoc);
            return list.Select(MapToDTO).ToList();
        }

        public async Task<int> DemSoChuaDocAsync(int maNguoiDung)
        {
            return await _repo.DemSoChuaDocAsync(maNguoiDung);
        }

        public async Task<ThongBaoDTO> GuiThongBaoAsync(GuiThongBaoRequestDTO request)
        {
            var thongBao = new ThongBao
            {
                MaNguoiDung = request.MaNguoiDung,
                TieuDe = request.TieuDe.Trim(),
                NoiDung = request.NoiDung,
                NgayGui = DateTime.Now,
                DaDoc = false
            };

            var created = await _repo.CreateAsync(thongBao);
            var itemWithUser = await _repo.GetByIdAsync(created.MaThongBao);
            return MapToDTO(itemWithUser ?? created);
        }

        public async Task<int> GuiThongBaoChoTatCaAsync(string tieuDe, string noiDung)
        {
            var users = await _repo.GetAllUsersAsync();
            if (!users.Any()) return 0;

            var list = users.Select(u => new ThongBao
            {
                MaNguoiDung = u.MaNguoiDung,
                TieuDe = tieuDe.Trim(),
                NoiDung = noiDung,
                NgayGui = DateTime.Now,
                DaDoc = false
            }).ToList();

            await _repo.CreateRangeAsync(list);
            return list.Count;
        }

        public async Task<bool> DanhDauDaDocAsync(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item == null) return false;

            item.DaDoc = true;
            await _repo.UpdateAsync(item);
            return true;
        }

        public async Task<bool> DanhDauTatCaDaDocAsync(int maNguoiDung)
        {
            var list = await _repo.GetByNguoiDungAsync(maNguoiDung);
            var unread = list.Where(t => !t.DaDoc).ToList();
            if (!unread.Any()) return true;

            foreach (var item in unread)
            {
                item.DaDoc = true;
                await _repo.UpdateAsync(item);
            }

            return true;
        }

        public async Task<bool> XoaThongBaoAsync(int id)
        {
            return await _repo.DeleteAsync(id);
        }

        private static ThongBaoDTO MapToDTO(ThongBao tb)
        {
            return new ThongBaoDTO
            {
                MaThongBao = tb.MaThongBao,
                MaNguoiDung = tb.MaNguoiDung,
                TenNguoiDung = tb.NguoiDung?.HoTen ?? "Khách hàng",
                EmailNguoiDung = tb.NguoiDung?.Email ?? "",
                TieuDe = tb.TieuDe,
                NoiDung = tb.NoiDung,
                NgayGui = tb.NgayGui,
                DaDoc = tb.DaDoc
            };
        }
    }
}
