using DichVuDatVe.DTOs;
using DichVuDatVe.Models;
using DichVuDatVe.Repositories;

namespace DichVuDatVe.Services
{
    public class DatVeService : IDatVeService
    {
        private readonly IDatVeRepository _repo;

        public DatVeService(IDatVeRepository repo)
        {
            _repo = repo;
        }

        public async Task<DonDatVeDTO> TaoDonDatVeAsync(TaoDonDatVeDTO dto)
        {
            var ghes = await _repo.GetGheSuatChieusAsync(dto.DanhSachMaGheSuatChieu);
            if (ghes.Count != dto.DanhSachMaGheSuatChieu.Count)
            {
                throw new InvalidOperationException("Một số ghế không tồn tại trong suất chiếu này!");
            }

            // Kiểm tra trạng thái ghế
            var unavailable = ghes.Where(g => g.TrangThai != "Trong").ToList();
            if (unavailable.Any())
            {
                var names = string.Join(", ", unavailable.Select(u => $"{u.GheNgoi?.HangGhe}{u.GheNgoi?.SoGhe}"));
                throw new InvalidOperationException($"Các ghế sau đã được người khác giữ hoặc đặt: {names}");
            }

            // Tính tổng tiền
            decimal tongTien = ghes.Sum(g => g.GiaVe);

            var donDatVe = new DatVe
            {
                MaNguoiDung = dto.MaNguoiDung,
                MaSuatChieu = dto.MaSuatChieu,
                NgayDat = DateTime.Now,
                TongTien = tongTien,
                TrangThai = "ChoThanhToan"
            };

            foreach (var g in ghes)
            {
                donDatVe.ChiTietDatVes.Add(new ChiTietDatVe
                {
                    MaGheSuatChieu = g.MaGheSuatChieu,
                    DonGia = g.GiaVe
                });

                // Chuyển ghế sang trạng thái giữ chỗ
                g.TrangThai = "DangGiu";
                g.ThoiGianHetHan = DateTime.Now.AddMinutes(10);
            }

            await _repo.CreateAsync(donDatVe);
            await _repo.UpdateGheSuatChieusAsync(ghes);

            var created = await _repo.GetByIdAsync(donDatVe.MaDatVe);
            return MapToDTO(created!);
        }

        public async Task<DonDatVeDTO?> GetByIdAsync(int id)
        {
            var datVe = await _repo.GetByIdAsync(id);
            return datVe == null ? null : MapToDTO(datVe);
        }

        public async Task<List<DonDatVeDTO>> GetAllAsync(string? trangThai = null, int? maSuatChieu = null)
        {
            var list = await _repo.GetAllAsync(trangThai, maSuatChieu);
            return list.Select(MapToDTO).ToList();
        }

        public async Task<List<DonDatVeDTO>> GetByNguoiDungAsync(int maNguoiDung)
        {
            var list = await _repo.GetByNguoiDungAsync(maNguoiDung);
            return list.Select(MapToDTO).ToList();
        }

        public async Task<bool> CapNhatTrangThaiAsync(int id, string trangThai)
        {
            var datVe = await _repo.GetByIdAsync(id);
            if (datVe == null) return false;

            datVe.TrangThai = trangThai;

            // Nếu trạng thái là DaThanhToan, xác nhận ghế thành DaDat
            if (trangThai == "DaThanhToan")
            {
                var ghes = datVe.ChiTietDatVes
                    .Where(ct => ct.GheSuatChieu != null)
                    .Select(ct => ct.GheSuatChieu!)
                    .ToList();
                foreach (var g in ghes)
                {
                    g.TrangThai = "DaDat";
                    g.ThoiGianHetHan = null;
                }
                await _repo.UpdateGheSuatChieusAsync(ghes);
            }
            // Nếu hủy đơn hoặc hết hạn, trả lại trạng thái Trong cho ghế
            else if (trangThai == "DaHuy" || trangThai == "HetHan")
            {
                var ghes = datVe.ChiTietDatVes
                    .Where(ct => ct.GheSuatChieu != null)
                    .Select(ct => ct.GheSuatChieu!)
                    .ToList();
                foreach (var g in ghes)
                {
                    g.TrangThai = "Trong";
                    g.ThoiGianHetHan = null;
                }
                await _repo.UpdateGheSuatChieusAsync(ghes);
            }

            await _repo.UpdateAsync(datVe);
            return true;
        }

        public async Task<bool> HuyDonDatVeAsync(int id)
        {
            return await CapNhatTrangThaiAsync(id, "DaHuy");
        }

        private static DonDatVeDTO MapToDTO(DatVe dv)
        {
            return new DonDatVeDTO
            {
                MaDatVe = dv.MaDatVe,
                MaNguoiDung = dv.MaNguoiDung,
                TenNguoiDung = dv.NguoiDung?.HoTen ?? "Khách hàng",
                EmailNguoiDung = dv.NguoiDung?.Email ?? "",
                SoDienThoai = dv.NguoiDung?.SoDienThoai,
                MaSuatChieu = dv.MaSuatChieu,
                MaPhim = dv.SuatChieu?.MaPhim ?? 0,
                TenPhim = dv.SuatChieu?.Phim?.TenPhim ?? "",
                PosterPhim = dv.SuatChieu?.Phim?.Poster,
                TenRap = dv.SuatChieu?.PhongChieu?.RapChieu?.TenRap ?? "",
                TenPhong = dv.SuatChieu?.PhongChieu?.TenPhong ?? "",
                NgayChieu = dv.SuatChieu?.NgayChieu ?? DateTime.MinValue,
                GioBatDau = dv.SuatChieu?.GioBatDau ?? TimeSpan.Zero,
                GioKetThuc = dv.SuatChieu?.GioKetThuc ?? TimeSpan.Zero,
                NgayDat = dv.NgayDat,
                TongTien = dv.TongTien,
                TrangThai = dv.TrangThai,
                DanhSachGhe = dv.ChiTietDatVes.Select(ct => new ChiTietGheDatDTO
                {
                    MaGheSuatChieu = ct.MaGheSuatChieu,
                    MaGhe = ct.GheSuatChieu?.MaGhe ?? 0,
                    HangGhe = ct.GheSuatChieu?.GheNgoi?.HangGhe ?? "",
                    SoGhe = ct.GheSuatChieu?.GheNgoi?.SoGhe ?? 0,
                    LoaiGhe = ct.GheSuatChieu?.GheNgoi?.LoaiGhe ?? "Thuong",
                    DonGia = ct.DonGia
                }).ToList()
            };
        }
    }
}
