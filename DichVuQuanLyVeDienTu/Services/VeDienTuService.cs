using DichVuQuanLyVeDienTu.DTOs;
using DichVuQuanLyVeDienTu.Models;
using DichVuQuanLyVeDienTu.Repositories;

namespace DichVuQuanLyVeDienTu.Services
{
    public class VeDienTuService : IVeDienTuService
    {
        private readonly IVeDienTuRepository _repo;

        public VeDienTuService(IVeDienTuRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<VeDienTuDTO>> PhatHanhVeChoDonDatVeAsync(int maDatVe)
        {
            var datVe = await _repo.GetDatVeWithDetailsAsync(maDatVe);
            if (datVe == null)
            {
                throw new KeyNotFoundException($"Không tìm thấy đơn đặt vé #{maDatVe}");
            }

            // Kiểm tra các vé đã phát hành
            var existingTickets = await _repo.GetByDatVeIdAsync(maDatVe);
            if (existingTickets.Any())
            {
                return existingTickets.Select(MapToDTO).ToList();
            }

            // Tạo vé cho từng chi tiết
            var newTickets = new List<VeDienTu>();
            foreach (var ct in datVe.ChiTietDatVes)
            {
                var qrCode = $"CGV-{maDatVe}-{ct.MaChiTiet}-{Guid.NewGuid().ToString("N")[..8].ToUpper()}";
                var ticket = new VeDienTu
                {
                    MaChiTiet = ct.MaChiTiet,
                    MaQR = qrCode,
                    NgayPhatHanh = DateTime.Now,
                    TrangThai = "ChuaSuDung"
                };
                newTickets.Add(ticket);
            }

            await _repo.CreateRangeAsync(newTickets);

            var createdTickets = await _repo.GetByDatVeIdAsync(maDatVe);
            return createdTickets.Select(MapToDTO).ToList();
        }

        public async Task<KetQuaSoatVeDTO> KiemTraVaSoatVeAsync(string maQR)
        {
            if (string.IsNullOrWhiteSpace(maQR))
            {
                return new KetQuaSoatVeDTO
                {
                    HopLe = false,
                    ThongBao = "Mã QR hoặc mã vé không hợp lệ!"
                };
            }

            var ticket = await _repo.GetByMaQRAsync(maQR.Trim());
            if (ticket == null)
            {
                return new KetQuaSoatVeDTO
                {
                    HopLe = false,
                    ThongBao = "Không tìm thấy vé hợp lệ với thông tin quét này!"
                };
            }

            var ct = ticket.ChiTietDatVe;
            var dv = ct?.DatVe;
            var sc = dv?.SuatChieu;
            var pc = sc?.PhongChieu;
            var rap = pc?.RapChieu;
            var phim = sc?.Phim;
            var ghe = ct?.GheSuatChieu?.GheNgoi;

            var result = new KetQuaSoatVeDTO
            {
                MaVe = ticket.MaVe,
                MaQR = ticket.MaQR,
                TrangThai = ticket.TrangThai,
                NgaySuDung = ticket.NgaySuDung,
                MaDatVe = dv?.MaDatVe ?? 0,
                TenKhachHang = dv?.NguoiDung?.HoTen ?? "Khách vãng lai",
                SoDienThoai = dv?.NguoiDung?.SoDienThoai,
                TenPhim = phim?.TenPhim ?? "N/A",
                PosterPhim = phim?.Poster,
                TenRap = rap?.TenRap ?? "N/A",
                TenPhong = pc?.TenPhong ?? "N/A",
                NgayChieu = sc?.NgayChieu ?? DateTime.MinValue,
                GioBatDau = sc?.GioBatDau ?? TimeSpan.Zero,
                ViTriGhe = ghe != null ? $"{ghe.HangGhe}{ghe.SoGhe}" : "N/A",
                LoaiGhe = ghe?.LoaiGhe ?? "N/A",
                GiaVe = ct?.DonGia ?? 0
            };

            if (ticket.TrangThai == "DaHuy")
            {
                result.HopLe = false;
                result.ThongBao = "Vé này đã bị hủy hoặc hoàn tiền!";
                return result;
            }

            if (ticket.TrangThai == "DaSuDung")
            {
                result.HopLe = false;
                result.ThongBao = $"Vé này ĐÃ ĐƯỢC SỬ DỤNG vào lúc {ticket.NgaySuDung:HH:mm - dd/MM/yyyy}!";
                return result;
            }

            // Hợp lệ -> Check-in
            ticket.TrangThai = "DaSuDung";
            ticket.NgaySuDung = DateTime.Now;
            await _repo.UpdateAsync(ticket);

            result.HopLe = true;
            result.TrangThai = "DaSuDung";
            result.NgaySuDung = ticket.NgaySuDung;
            result.ThongBao = "Soát vé THÀNH CÔNG! Mời quý khách vào phòng chiếu.";
            return result;
        }

        public async Task<VeDienTuDTO?> GetByIdAsync(int id)
        {
            var ticket = await _repo.GetByIdAsync(id);
            return ticket != null ? MapToDTO(ticket) : null;
        }

        public async Task<VeDienTuDTO?> GetByMaQRAsync(string maQR)
        {
            var ticket = await _repo.GetByMaQRAsync(maQR);
            return ticket != null ? MapToDTO(ticket) : null;
        }

        public async Task<List<VeDienTuDTO>> GetByDatVeIdAsync(int maDatVe)
        {
            var tickets = await _repo.GetByDatVeIdAsync(maDatVe);
            return tickets.Select(MapToDTO).ToList();
        }

        public async Task<List<VeDienTuDTO>> GetAllAsync(string? trangThai = null)
        {
            var tickets = await _repo.GetAllAsync(trangThai);
            return tickets.Select(MapToDTO).ToList();
        }

        public async Task<bool> HuyVeAsync(int id)
        {
            var ticket = await _repo.GetByIdAsync(id);
            if (ticket == null) return false;

            ticket.TrangThai = "DaHuy";
            await _repo.UpdateAsync(ticket);
            return true;
        }

        private static VeDienTuDTO MapToDTO(VeDienTu v)
        {
            var ct = v.ChiTietDatVe;
            var dv = ct?.DatVe;
            var sc = dv?.SuatChieu;
            var pc = sc?.PhongChieu;
            var rap = pc?.RapChieu;
            var phim = sc?.Phim;
            var ghe = ct?.GheSuatChieu?.GheNgoi;

            return new VeDienTuDTO
            {
                MaVe = v.MaVe,
                MaChiTiet = v.MaChiTiet,
                MaDatVe = dv?.MaDatVe ?? 0,
                MaQR = v.MaQR,
                NgayPhatHanh = v.NgayPhatHanh,
                TrangThai = v.TrangThai,
                NgaySuDung = v.NgaySuDung,

                TenKhachHang = dv?.NguoiDung?.HoTen ?? "Khách vãng lai",
                EmailKhachHang = dv?.NguoiDung?.Email ?? "",
                SoDienThoai = dv?.NguoiDung?.SoDienThoai,

                TenPhim = phim?.TenPhim ?? "N/A",
                PosterPhim = phim?.Poster,
                DoTuoi = phim?.DoTuoi,
                ThoiLuong = phim?.ThoiLuong ?? 0,

                TenRap = rap?.TenRap ?? "N/A",
                DiaChiRap = rap?.DiaChi,
                TenPhong = pc?.TenPhong ?? "N/A",
                NgayChieu = sc?.NgayChieu ?? DateTime.MinValue,
                GioBatDau = sc?.GioBatDau ?? TimeSpan.Zero,
                GioKetThuc = sc?.GioKetThuc ?? TimeSpan.Zero,

                HangGhe = ghe?.HangGhe ?? "",
                SoGhe = ghe?.SoGhe ?? 0,
                LoaiGhe = ghe?.LoaiGhe ?? "Thường",
                GiaVe = ct?.DonGia ?? 0
            };
        }
    }
}
