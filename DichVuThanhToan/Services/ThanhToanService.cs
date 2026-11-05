using DichVuThanhToan.DTOs;
using DichVuThanhToan.Models;
using DichVuThanhToan.Repositories;

namespace DichVuThanhToan.Services
{
    public class ThanhToanService : IThanhToanService
    {
        private readonly IThanhToanRepository _repo;

        public ThanhToanService(IThanhToanRepository repo)
        {
            _repo = repo;
        }

        public async Task<KetQuaThanhToanDTO> TaoVaXacNhanGiaoDichAsync(TaoGiaoDichThanhToanDTO dto)
        {
            var datVe = await _repo.GetDatVeWithDetailsAsync(dto.MaDatVe);
            if (datVe == null)
            {
                throw new InvalidOperationException($"Không tìm thấy đơn đặt vé #{dto.MaDatVe}");
            }

            if (datVe.TrangThai == "DaThanhToan")
            {
                throw new InvalidOperationException($"Đơn đặt vé #{dto.MaDatVe} đã được thanh toán trước đó.");
            }

            if (datVe.TrangThai == "DaHuy" || datVe.TrangThai == "HetHan")
            {
                throw new InvalidOperationException($"Đơn đặt vé #{dto.MaDatVe} đã bị hủy hoặc hết hạn, không thể thanh toán.");
            }

            decimal soTienThanhToan = dto.SoTien ?? datVe.TongTien;
            string maGiaoDich = $"TXN-{dto.PhuongThuc.ToUpper()}-{DateTime.Now:yyyyMMddHHmmss}-{dto.MaDatVe}";

            var thanhToan = new ThanhToan
            {
                MaDatVe = dto.MaDatVe,
                PhuongThuc = dto.PhuongThuc,
                MaGiaoDich = maGiaoDich,
                SoTien = soTienThanhToan,
                NgayThanhToan = DateTime.Now,
                TrangThai = "ThanhCong"
            };

            await _repo.CreateAsync(thanhToan);

            // Cập nhật trạng thái đơn đặt vé thành DaThanhToan
            datVe.TrangThai = "DaThanhToan";
            await _repo.UpdateDatVeAsync(datVe);

            // Cập nhật trạng thái ghế sang DaDat
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

            return new KetQuaThanhToanDTO
            {
                ThanhCong = true,
                ThongBao = "Giao dịch thanh toán thành công!",
                MaThanhToan = thanhToan.MaThanhToan,
                MaGiaoDich = maGiaoDich,
                MaDatVe = datVe.MaDatVe,
                SoTien = soTienThanhToan,
                PhuongThuc = dto.PhuongThuc,
                NgayThanhToan = thanhToan.NgayThanhToan
            };
        }

        public async Task<ThanhToanDTO?> GetByIdAsync(int id)
        {
            var tt = await _repo.GetByIdAsync(id);
            return tt == null ? null : MapToDTO(tt);
        }

        public async Task<ThanhToanDTO?> GetByDatVeIdAsync(int maDatVe)
        {
            var tt = await _repo.GetByDatVeIdAsync(maDatVe);
            return tt == null ? null : MapToDTO(tt);
        }

        public async Task<List<ThanhToanDTO>> GetAllAsync(string? trangThai = null, string? phuongThuc = null)
        {
            var list = await _repo.GetAllAsync(trangThai, phuongThuc);
            return list.Select(MapToDTO).ToList();
        }

        public async Task<bool> CapNhatTrangThaiAsync(int id, string trangThai, string? maGiaoDich = null)
        {
            var tt = await _repo.GetByIdAsync(id);
            if (tt == null) return false;

            tt.TrangThai = trangThai;
            if (!string.IsNullOrWhiteSpace(maGiaoDich))
            {
                tt.MaGiaoDich = maGiaoDich;
            }

            // Đồng bộ trạng thái đơn vé
            if (tt.DatVe != null)
            {
                if (trangThai == "ThanhCong")
                {
                    tt.DatVe.TrangThai = "DaThanhToan";
                    var ghes = tt.DatVe.ChiTietDatVes
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
                else if (trangThai == "HoanTien" || trangThai == "ThatBai")
                {
                    tt.DatVe.TrangThai = "DaHuy";
                    var ghes = tt.DatVe.ChiTietDatVes
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
                await _repo.UpdateDatVeAsync(tt.DatVe);
            }

            await _repo.UpdateAsync(tt);
            return true;
        }

        public async Task<bool> HoanTienAsync(int id)
        {
            return await CapNhatTrangThaiAsync(id, "HoanTien");
        }

        private static ThanhToanDTO MapToDTO(ThanhToan tt)
        {
            var dv = tt.DatVe;
            var sc = dv?.SuatChieu;
            var pc = sc?.PhongChieu;
            var rc = pc?.RapChieu;

            var ghes = dv?.ChiTietDatVes
                .Select(ct => ct.GheSuatChieu?.GheNgoi)
                .Where(g => g != null)
                .Select(g => $"{g!.HangGhe}{g.SoGhe}")
                .ToList() ?? new List<string>();

            return new ThanhToanDTO
            {
                MaThanhToan = tt.MaThanhToan,
                MaDatVe = tt.MaDatVe,
                PhuongThuc = tt.PhuongThuc,
                MaGiaoDich = tt.MaGiaoDich,
                SoTien = tt.SoTien,
                NgayThanhToan = tt.NgayThanhToan,
                TrangThai = tt.TrangThai,
                TenKhachHang = dv?.NguoiDung?.HoTen ?? "Khách hàng",
                EmailKhachHang = dv?.NguoiDung?.Email ?? "",
                SoDienThoai = dv?.NguoiDung?.SoDienThoai,
                TenPhim = sc?.Phim?.TenPhim ?? "",
                PosterPhim = sc?.Phim?.Poster,
                TenRap = rc?.TenRap ?? "",
                TenPhong = pc?.TenPhong ?? "",
                NgayChieu = sc?.NgayChieu ?? DateTime.MinValue,
                GioBatDau = sc?.GioBatDau ?? TimeSpan.Zero,
                DanhSachGhe = ghes
            };
        }
    }
}
