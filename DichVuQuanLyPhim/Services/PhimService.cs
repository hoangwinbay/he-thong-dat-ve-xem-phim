using DichVuQuanLyPhim.DTOs;
using DichVuQuanLyPhim.Models;
using DichVuQuanLyPhim.Repositories;

namespace DichVuQuanLyPhim.Services
{
    public class PhimService : IPhimService
    {
        private readonly IPhimRepository _repository;

        public PhimService(IPhimRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ThongTinPhimDTO>> GetAllAsync(string? trangThai = null)
        {
            var phims = await _repository.GetAllAsync(trangThai);
            return phims.Select(MapToDTO);
        }

        public async Task<ThongTinPhimDTO?> GetByIdAsync(int id)
        {
            var phim = await _repository.GetByIdAsync(id);
            return phim != null ? MapToDTO(phim) : null;
        }

        public async Task<ThongTinPhimDTO> ThemPhimAsync(ThemPhimDTO dto)
        {
            var phim = new Phim
            {
                TenPhim = dto.TenPhim.Trim(),
                MoTa = dto.MoTa?.Trim(),
                DaoDien = dto.DaoDien?.Trim(),
                DienVien = dto.DienVien?.Trim(),
                ThoiLuong = dto.ThoiLuong,
                NgayKhoiChieu = dto.NgayKhoiChieu,
                NgayKetThuc = dto.NgayKetThuc,
                QuocGia = dto.QuocGia?.Trim(),
                DoTuoi = dto.DoTuoi?.Trim(),
                Poster = dto.Poster?.Trim(),
                Trailer = dto.Trailer?.Trim(),
                TrangThai = dto.TrangThai
            };

            await _repository.AddAsync(phim, dto.TheLoaiIds);

            var created = await _repository.GetByIdAsync(phim.MaPhim);
            return MapToDTO(created!);
        }

        public async Task<bool> CapNhatPhimAsync(int id, CapNhatPhimDTO dto)
        {
            var phim = await _repository.GetByIdAsync(id);
            if (phim == null) return false;

            phim.TenPhim = dto.TenPhim.Trim();
            phim.MoTa = dto.MoTa?.Trim();
            phim.DaoDien = dto.DaoDien?.Trim();
            phim.DienVien = dto.DienVien?.Trim();
            phim.ThoiLuong = dto.ThoiLuong;
            phim.NgayKhoiChieu = dto.NgayKhoiChieu;
            phim.NgayKetThuc = dto.NgayKetThuc;
            phim.QuocGia = dto.QuocGia?.Trim();
            phim.DoTuoi = dto.DoTuoi?.Trim();
            phim.Poster = dto.Poster?.Trim();
            phim.Trailer = dto.Trailer?.Trim();
            phim.TrangThai = dto.TrangThai;

            await _repository.UpdateAsync(phim, dto.TheLoaiIds);
            return true;
        }

        public async Task<bool> CapNhatTrangThaiAsync(int id, string trangThai)
        {
            var phim = await _repository.GetByIdAsync(id);
            if (phim == null) return false;

            phim.TrangThai = trangThai;
            await _repository.UpdateAsync(phim, null);
            return true;
        }

        public async Task<bool> XoaPhimAsync(int id)
        {
            var phim = await _repository.GetByIdAsync(id);
            if (phim == null) return false;

            await _repository.DeleteAsync(id);
            return true;
        }

        private static ThongTinPhimDTO MapToDTO(Phim p)
        {
            return new ThongTinPhimDTO
            {
                MaPhim = p.MaPhim,
                TenPhim = p.TenPhim,
                MoTa = p.MoTa,
                DaoDien = p.DaoDien,
                DienVien = p.DienVien,
                ThoiLuong = p.ThoiLuong,
                NgayKhoiChieu = p.NgayKhoiChieu,
                NgayKetThuc = p.NgayKetThuc,
                QuocGia = p.QuocGia,
                DoTuoi = p.DoTuoi,
                Poster = p.Poster,
                Trailer = p.Trailer,
                TrangThai = p.TrangThai,
                TheLoai = p.PhimTheLoais.Select(pt => pt.TheLoai?.TenTheLoai ?? "").Where(t => !string.IsNullOrEmpty(t)).ToList()
            };
        }
    }
}

