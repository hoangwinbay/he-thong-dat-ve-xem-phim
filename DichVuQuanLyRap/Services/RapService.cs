using DichVuQuanLyRap.DTOs;
using DichVuQuanLyRap.Models;
using DichVuQuanLyRap.Repositories;

namespace DichVuQuanLyRap.Services
{
    public class RapService : IRapService
    {
        private readonly IRapRepository _repository;

        public RapService(IRapRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ThongTinRapDTO>> GetAllRapAsync()
        {
            var raps = await _repository.GetAllRapAsync();
            return raps.Select(MapRapToDTO);
        }

        public async Task<ThongTinRapDTO?> GetRapByIdAsync(int maRap)
        {
            var rap = await _repository.GetRapByIdAsync(maRap);
            return rap != null ? MapRapToDTO(rap) : null;
        }

        public async Task<ThongTinRapDTO> ThemRapAsync(ThemRapDTO dto)
        {
            var rap = new RapChieu
            {
                TenRap = dto.TenRap.Trim(),
                DiaChi = dto.DiaChi?.Trim(),
                SoDienThoai = dto.SoDienThoai?.Trim(),
                TrangThai = dto.TrangThai
            };

            await _repository.AddRapAsync(rap);
            return MapRapToDTO(rap);
        }

        public async Task<bool> CapNhatRapAsync(int maRap, CapNhatRapDTO dto)
        {
            var rap = await _repository.GetRapByIdAsync(maRap);
            if (rap == null) return false;

            rap.TenRap = dto.TenRap.Trim();
            rap.DiaChi = dto.DiaChi?.Trim();
            rap.SoDienThoai = dto.SoDienThoai?.Trim();
            rap.TrangThai = dto.TrangThai;

            await _repository.UpdateRapAsync(rap);
            return true;
        }

        public async Task<bool> XoaRapAsync(int maRap)
        {
            var rap = await _repository.GetRapByIdAsync(maRap);
            if (rap == null) return false;

            await _repository.DeleteRapAsync(maRap);
            return true;
        }

        public async Task<IEnumerable<ThongTinPhongChieuDTO>> GetPhongChieuByRapAsync(int maRap)
        {
            var phongs = await _repository.GetPhongChieuByRapAsync(maRap);
            return phongs.Select(MapPhongToDTO);
        }

        public async Task<ThongTinPhongChieuDTO?> GetPhongChieuByIdAsync(int maPhong)
        {
            var phong = await _repository.GetPhongChieuByIdAsync(maPhong);
            return phong != null ? MapPhongToDTO(phong) : null;
        }

        public async Task<ThongTinPhongChieuDTO> ThemPhongChieuAsync(ThemPhongChieuDTO dto)
        {
            var phong = new PhongChieu
            {
                MaRap = dto.MaRap,
                TenPhong = dto.TenPhong.Trim(),
                LoaiPhong = dto.LoaiPhong?.Trim(),
                SoLuongGhe = dto.SoLuongGhe
            };

            await _repository.AddPhongChieuAsync(phong);
            return MapPhongToDTO(phong);
        }

        public async Task<bool> CapNhatPhongChieuAsync(int maPhong, CapNhatPhongChieuDTO dto)
        {
            var phong = await _repository.GetPhongChieuByIdAsync(maPhong);
            if (phong == null) return false;

            phong.TenPhong = dto.TenPhong.Trim();
            phong.LoaiPhong = dto.LoaiPhong?.Trim();
            phong.SoLuongGhe = dto.SoLuongGhe;

            await _repository.UpdatePhongChieuAsync(phong);
            return true;
        }

        public async Task<bool> XoaPhongChieuAsync(int maPhong)
        {
            var phong = await _repository.GetPhongChieuByIdAsync(maPhong);
            if (phong == null) return false;

            await _repository.DeletePhongChieuAsync(maPhong);
            return true;
        }

        private static ThongTinRapDTO MapRapToDTO(RapChieu r)
        {
            return new ThongTinRapDTO
            {
                MaRap = r.MaRap,
                TenRap = r.TenRap,
                DiaChi = r.DiaChi,
                SoDienThoai = r.SoDienThoai,
                TrangThai = r.TrangThai,
                SoLuongPhongChieu = r.PhongChieus.Count,
                PhongChieus = r.PhongChieus.Select(MapPhongToDTO).ToList()
            };
        }

        private static ThongTinPhongChieuDTO MapPhongToDTO(PhongChieu p)
        {
            return new ThongTinPhongChieuDTO
            {
                MaPhong = p.MaPhong,
                MaRap = p.MaRap,
                TenRap = p.RapChieu?.TenRap,
                TenPhong = p.TenPhong,
                LoaiPhong = p.LoaiPhong,
                SoLuongGhe = p.SoLuongGhe
            };
        }
    }
}

