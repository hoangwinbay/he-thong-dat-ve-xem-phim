using DichVuDanhGiaPhim.DTOs;
using DichVuDanhGiaPhim.Models;
using DichVuDanhGiaPhim.Repositories;

namespace DichVuDanhGiaPhim.Services
{
    public class DanhGiaPhimService : IDanhGiaPhimService
    {
        private readonly IDanhGiaPhimRepository _repo;

        public DanhGiaPhimService(IDanhGiaPhimRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<DanhGiaPhimDTO>> GetByPhimIdAsync(int maPhim)
        {
            var list = await _repo.GetByPhimIdAsync(maPhim);
            return list.Select(MapToDTO).ToList();
        }

        public async Task<List<DanhGiaPhimDTO>> GetByNguoiDungAsync(int maNguoiDung)
        {
            var list = await _repo.GetByNguoiDungAsync(maNguoiDung);
            return list.Select(MapToDTO).ToList();
        }

        public async Task<DanhGiaPhimDTO?> GetByIdAsync(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            return item != null ? MapToDTO(item) : null;
        }

        public async Task<List<DanhGiaPhimDTO>> GetAllAsync(int? soSao = null, int? maPhim = null)
        {
            var list = await _repo.GetAllAsync(soSao, maPhim);
            return list.Select(MapToDTO).ToList();
        }

        public async Task<ThongKeDanhGiaPhimDTO> GetThongKePhimAsync(int maPhim)
        {
            var phim = await _repo.GetPhimByIdAsync(maPhim);
            var reviews = await _repo.GetByPhimIdAsync(maPhim);

            var count = reviews.Count;
            double avg = count > 0 ? Math.Round(reviews.Average(r => r.SoSao), 1) : 0;

            return new ThongKeDanhGiaPhimDTO
            {
                MaPhim = maPhim,
                TenPhim = phim?.TenPhim ?? "N/A",
                TongSoDanhGia = count,
                DiemTrungBinh = avg,
                SoSao5 = reviews.Count(r => r.SoSao == 5),
                SoSao4 = reviews.Count(r => r.SoSao == 4),
                SoSao3 = reviews.Count(r => r.SoSao == 3),
                SoSao2 = reviews.Count(r => r.SoSao == 2),
                SoSao1 = reviews.Count(r => r.SoSao == 1)
            };
        }

        public async Task<DanhGiaPhimDTO> TaoHoacCapNhatDanhGiaAsync(TaoDanhGiaRequestDTO request)
        {
            var existing = await _repo.GetByUserAndMovieAsync(request.MaNguoiDung, request.MaPhim);
            if (existing != null)
            {
                // Cập nhật đánh giá
                existing.SoSao = request.SoSao;
                existing.BinhLuan = request.BinhLuan;
                existing.NgayDanhGia = DateTime.Now;
                await _repo.UpdateAsync(existing);

                var updated = await _repo.GetByIdAsync(existing.MaDanhGia);
                return MapToDTO(updated ?? existing);
            }

            // Tạo mới
            var item = new DanhGiaPhim
            {
                MaNguoiDung = request.MaNguoiDung,
                MaPhim = request.MaPhim,
                SoSao = request.SoSao,
                BinhLuan = request.BinhLuan,
                NgayDanhGia = DateTime.Now
            };

            var created = await _repo.CreateAsync(item);
            var itemWithIncludes = await _repo.GetByIdAsync(created.MaDanhGia);
            return MapToDTO(itemWithIncludes ?? created);
        }

        public async Task<bool> XoaDanhGiaAsync(int id)
        {
            return await _repo.DeleteAsync(id);
        }

        private static DanhGiaPhimDTO MapToDTO(DanhGiaPhim d)
        {
            return new DanhGiaPhimDTO
            {
                MaDanhGia = d.MaDanhGia,
                MaNguoiDung = d.MaNguoiDung,
                TenNguoiDung = d.NguoiDung?.HoTen ?? "Khách hàng",
                EmailNguoiDung = d.NguoiDung?.Email ?? "",
                MaPhim = d.MaPhim,
                TenPhim = d.Phim?.TenPhim ?? "N/A",
                PosterPhim = d.Phim?.Poster,
                SoSao = d.SoSao,
                BinhLuan = d.BinhLuan,
                NgayDanhGia = d.NgayDanhGia
            };
        }
    }
}
