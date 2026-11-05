using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GiaoDienKhachHang.Data;
using GiaoDienKhachHang.Models;

namespace GiaoDienKhachHang.Controllers
{
    public class ThanhToanController : Controller
    {
        private readonly CustomerDbContext _context;

        public ThanhToanController(CustomerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int maDatVe)
        {
            var datVe = await _context.DatVes
                .Include(dv => dv.NguoiDung)
                .Include(dv => dv.SuatChieu)
                    .ThenInclude(sc => sc!.Phim)
                .Include(dv => dv.SuatChieu)
                    .ThenInclude(sc => sc!.PhongChieu)
                        .ThenInclude(pc => pc!.RapChieu)
                .Include(dv => dv.ChiTietDatVes)
                    .ThenInclude(ct => ct.GheSuatChieu)
                        .ThenInclude(gsc => gsc!.GheNgoi)
                .FirstOrDefaultAsync(dv => dv.MaDatVe == maDatVe);

            if (datVe == null)
            {
                TempData["Error"] = "Không tìm thấy đơn đặt vé cần thanh toán.";
                return RedirectToAction("Index", "LichSuDatVe");
            }

            if (datVe.TrangThai == "DaThanhToan")
            {
                var existingTT = await _context.ThanhToans
                    .OrderByDescending(t => t.NgayThanhToan)
                    .FirstOrDefaultAsync(t => t.MaDatVe == maDatVe && t.TrangThai == "ThanhCong");

                if (existingTT != null)
                {
                    return RedirectToAction(nameof(ThanhCong), new { id = existingTT.MaThanhToan });
                }
            }

            if (datVe.TrangThai == "DaHuy" || datVe.TrangThai == "HetHan")
            {
                TempData["Error"] = "Đơn đặt vé này đã bị hủy hoặc hết hạn thời gian giữ chỗ.";
                return RedirectToAction("Index", "LichSuDatVe");
            }

            return View(datVe);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> XuLyThanhToan(int maDatVe, string phuongThuc)
        {
            var datVe = await _context.DatVes
                .Include(dv => dv.ChiTietDatVes)
                    .ThenInclude(ct => ct.GheSuatChieu)
                .FirstOrDefaultAsync(dv => dv.MaDatVe == maDatVe);

            if (datVe == null)
            {
                TempData["Error"] = "Không tìm thấy đơn đặt vé.";
                return RedirectToAction("Index", "LichSuDatVe");
            }

            if (datVe.TrangThai == "DaThanhToan")
            {
                TempData["Error"] = "Đơn đặt vé này đã hoàn tất thanh toán trước đó.";
                return RedirectToAction("Index", "LichSuDatVe");
            }

            string phuongThucChuan = string.IsNullOrWhiteSpace(phuongThuc) ? "VNPAY" : phuongThuc;
            string maGiaoDich = $"TXN-{phuongThucChuan.ToUpper()}-{DateTime.Now:yyyyMMddHHmmss}-{maDatVe}";

            var thanhToan = new ThanhToan
            {
                MaDatVe = maDatVe,
                PhuongThuc = phuongThucChuan,
                MaGiaoDich = maGiaoDich,
                SoTien = datVe.TongTien,
                NgayThanhToan = DateTime.Now,
                TrangThai = "ThanhCong"
            };

            _context.ThanhToans.Add(thanhToan);

            // Cập nhật trạng thái đơn vé
            datVe.TrangThai = "DaThanhToan";

            // Cập nhật trạng thái ghế sang DaDat
            foreach (var ct in datVe.ChiTietDatVes)
            {
                if (ct.GheSuatChieu != null)
                {
                    ct.GheSuatChieu.TrangThai = "DaDat";
                    ct.GheSuatChieu.ThoiGianHetHan = null;
                }

                // Phát hành vé điện tử & mã QR tương ứng
                var existingVe = await _context.VeDienTus.FirstOrDefaultAsync(v => v.MaChiTiet == ct.MaChiTiet);
                if (existingVe == null)
                {
                    var qrCode = $"CGV-{datVe.MaDatVe}-{ct.MaChiTiet}-{Guid.NewGuid().ToString("N")[..8].ToUpper()}";
                    _context.VeDienTus.Add(new VeDienTu
                    {
                        MaChiTiet = ct.MaChiTiet,
                        MaQR = qrCode,
                        NgayPhatHanh = DateTime.Now,
                        TrangThai = "ChuaSuDung"
                    });
                }
            }

            // Gửi thông báo xác nhận đặt vé thành công (Dịch vụ Thông báo)
            _context.ThongBaos.Add(new ThongBao
            {
                MaNguoiDung = datVe.MaNguoiDung,
                TieuDe = $"Đặt vé thành công đơn #{datVe.MaDatVe}!",
                NoiDung = $"Giao dịch thanh toán {datVe.TongTien:N0} đ thành công qua {phuongThucChuan}. Vé điện tử và mã QR đã được tạo sẵn trong mục Vé Điện Tử. Chúc quý khách xem phim vui vẻ!",
                NgayGui = DateTime.Now,
                DaDoc = false
            });

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ThanhCong), new { id = thanhToan.MaThanhToan });
        }

        [HttpGet]
        public async Task<IActionResult> ThanhCong(int id)
        {
            var thanhToan = await _context.ThanhToans
                .Include(tt => tt.DatVe)
                    .ThenInclude(dv => dv!.NguoiDung)
                .Include(tt => tt.DatVe)
                    .ThenInclude(dv => dv!.SuatChieu)
                        .ThenInclude(sc => sc!.Phim)
                .Include(tt => tt.DatVe)
                    .ThenInclude(dv => dv!.SuatChieu)
                        .ThenInclude(sc => sc!.PhongChieu)
                            .ThenInclude(pc => pc!.RapChieu)
                .Include(tt => tt.DatVe)
                    .ThenInclude(dv => dv!.ChiTietDatVes)
                        .ThenInclude(ct => ct.GheSuatChieu)
                            .ThenInclude(gsc => gsc!.GheNgoi)
                .FirstOrDefaultAsync(tt => tt.MaThanhToan == id);

            if (thanhToan == null) return NotFound();

            return View(thanhToan);
        }
    }
}
