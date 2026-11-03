using DichVuQuanLySuatChieu.DTOs;
using DichVuQuanLySuatChieu.Models;
using DichVuQuanLySuatChieu.Repositories;

namespace DichVuQuanLySuatChieu.Services
{
    public class SuatChieuService : ISuatChieuService
    {
        private readonly ISuatChieuRepository _repository;

        public SuatChieuService(ISuatChieuRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ThongTinSuatChieuDTO>> GetListAsync(LocSuatChieuDTO filter)
        {
            var list = await _repository.GetListAsync(filter);
            return list.Select(MapToDTO);
        }

        public async Task<ThongTinSuatChieuDTO?> GetByIdAsync(int id)
        {
            var sc = await _repository.GetByIdAsync(id);
            return sc != null ? MapToDTO(sc) : null;
        }

        public async Task<ThongTinSuatChieuDTO> ThemSuatChieuAsync(ThemSuatChieuDTO dto)
        {
            if (dto.GioKetThuc <= dto.GioBatDau)
            {
                throw new ArgumentException("Giờ kết thúc phải lớn hơn giờ bắt đầu.");
            }

            if (await _repository.HasConflictAsync(dto.MaPhong, dto.NgayChieu, dto.GioBatDau, dto.GioKetThuc))
            {
                throw new InvalidOperationException("Phòng chiếu đã có suất chiếu khác trong khung giờ này.");
            }

            var sc = new SuatChieu
            {
                MaPhim = dto.MaPhim,
                MaPhong = dto.MaPhong,
                NgayChieu = dto.NgayChieu.Date,
                GioBatDau = dto.GioBatDau,
                GioKetThuc = dto.GioKetThuc,
                GiaVe = dto.GiaVe,
                TrangThai = dto.TrangThai
            };

            await _repository.AddAsync(sc);

            var created = await _repository.GetByIdAsync(sc.MaSuatChieu);
            return MapToDTO(created!);
        }

        public async Task<bool> CapNhatSuatChieuAsync(int id, CapNhatSuatChieuDTO dto)
        {
            if (dto.GioKetThuc <= dto.GioBatDau)
            {
                throw new ArgumentException("Giờ kết thúc phải lớn hơn giờ bắt đầu.");
            }

            if (await _repository.HasConflictAsync(dto.MaPhong, dto.NgayChieu, dto.GioBatDau, dto.GioKetThuc, id))
            {
                throw new InvalidOperationException("Phòng chiếu đã có suất chiếu khác trong khung giờ này.");
            }

            var sc = await _repository.GetByIdAsync(id);
            if (sc == null) return false;

            sc.MaPhim = dto.MaPhim;
            sc.MaPhong = dto.MaPhong;
            sc.NgayChieu = dto.NgayChieu.Date;
            sc.GioBatDau = dto.GioBatDau;
            sc.GioKetThuc = dto.GioKetThuc;
            sc.GiaVe = dto.GiaVe;
            sc.TrangThai = dto.TrangThai;

            await _repository.UpdateAsync(sc);
            return true;
        }

        public async Task<bool> CapNhatTrangThaiAsync(int id, string trangThai)
        {
            var sc = await _repository.GetByIdAsync(id);
            if (sc == null) return false;

            sc.TrangThai = trangThai;
            await _repository.UpdateAsync(sc);
            return true;
        }

        public async Task<bool> XoaSuatChieuAsync(int id)
        {
            var sc = await _repository.GetByIdAsync(id);
            if (sc == null) return false;

            await _repository.DeleteAsync(id);
            return true;
        }

        private static ThongTinSuatChieuDTO MapToDTO(SuatChieu s)
        {
            return new ThongTinSuatChieuDTO
            {
                MaSuatChieu = s.MaSuatChieu,
                MaPhim = s.MaPhim,
                TenPhim = s.Phim?.TenPhim,
                PosterPhim = s.Phim?.Poster,
                DoTuoi = s.Phim?.DoTuoi,
                ThoiLuong = s.Phim?.ThoiLuong ?? 0,
                MaPhong = s.MaPhong,
                TenPhong = s.PhongChieu?.TenPhong,
                LoaiPhong = s.PhongChieu?.LoaiPhong,
                NgayChieu = s.NgayChieu,
                GioBatDau = s.GioBatDau,
                GioKetThuc = s.GioKetThuc,
                GiaVe = s.GiaVe,
                TrangThai = s.TrangThai
            };
        }
    }
}

