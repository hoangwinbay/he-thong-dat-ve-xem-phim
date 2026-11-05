using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GiaoDienQuanTri.Data;
using GiaoDienQuanTri.Models;

namespace GiaoDienQuanTri.Controllers
{
    public class QuanLyVeController : Controller
    {
        private readonly AdminDbContext _context;

        public QuanLyVeController(AdminDbContext context)
        {
            _context = context;
        }

        // GET: /QuanLyVe
        public async Task<IActionResult> Index(string? trangThai, string? search)
        {
            var query = _context.VeDienTus
                .Include(v => v.ChiTietDatVe)
                    .ThenInclude(ct => ct!.DatVe)
                        .ThenInclude(dv => dv!.NguoiDung)
                .Include(v => v.ChiTietDatVe)
                    .ThenInclude(ct => ct!.DatVe)
                        .ThenInclude(dv => dv!.SuatChieu)
                            .ThenInclude(sc => sc!.Phim)
                .Include(v => v.ChiTietDatVe)
                    .ThenInclude(ct => ct!.DatVe)
                        .ThenInclude(dv => dv!.SuatChieu)
                            .ThenInclude(sc => sc!.PhongChieu)
                                .ThenInclude(pc => pc!.RapChieu)
                .Include(v => v.ChiTietDatVe)
                    .ThenInclude(ct => ct!.GheSuatChieu)
                        .ThenInclude(gsc => gsc!.GheNgoi)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(trangThai))
            {
                query = query.Where(v => v.TrangThai == trangThai);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim();
                query = query.Where(v =>
                    v.MaQR.Contains(s) ||
                    v.MaVe.ToString().Contains(s) ||
                    (v.ChiTietDatVe != null && v.ChiTietDatVe.MaDatVe.ToString().Contains(s)) ||
                    (v.ChiTietDatVe != null && v.ChiTietDatVe.DatVe != null && v.ChiTietDatVe.DatVe.NguoiDung != null &&
                        (v.ChiTietDatVe.DatVe.NguoiDung.HoTen.Contains(s) || v.ChiTietDatVe.DatVe.NguoiDung.Email.Contains(s))) ||
                    (v.ChiTietDatVe != null && v.ChiTietDatVe.DatVe != null && v.ChiTietDatVe.DatVe.SuatChieu != null &&
                        v.ChiTietDatVe.DatVe.SuatChieu.Phim != null && v.ChiTietDatVe.DatVe.SuatChieu.Phim.TenPhim.Contains(s)));
            }

            ViewBag.TrangThai = trangThai;
            ViewBag.Search = search;

            // Thống kê nhanh
            ViewBag.TongSoVe = await _context.VeDienTus.CountAsync();
            ViewBag.SoVeChuaSuDung = await _context.VeDienTus.CountAsync(v => v.TrangThai == "ChuaSuDung");
            ViewBag.SoVeDaSuDung = await _context.VeDienTus.CountAsync(v => v.TrangThai == "DaSuDung");
            ViewBag.SoVeDaHuy = await _context.VeDienTus.CountAsync(v => v.TrangThai == "DaHuy");

            var tickets = await query.OrderByDescending(v => v.NgayPhatHanh).ToListAsync();
            return View(tickets);
        }

        // GET: /QuanLyVe/ChiTiet/5
        public async Task<IActionResult> ChiTiet(int id)
        {
            var ve = await _context.VeDienTus
                .Include(v => v.ChiTietDatVe)
                    .ThenInclude(ct => ct!.DatVe)
                        .ThenInclude(dv => dv!.NguoiDung)
                .Include(v => v.ChiTietDatVe)
                    .ThenInclude(ct => ct!.DatVe)
                        .ThenInclude(dv => dv!.SuatChieu)
                            .ThenInclude(sc => sc!.Phim)
                .Include(v => v.ChiTietDatVe)
                    .ThenInclude(ct => ct!.DatVe)
                        .ThenInclude(dv => dv!.SuatChieu)
                            .ThenInclude(sc => sc!.PhongChieu)
                                .ThenInclude(pc => pc!.RapChieu)
                .Include(v => v.ChiTietDatVe)
                    .ThenInclude(ct => ct!.GheSuatChieu)
                        .ThenInclude(gsc => gsc!.GheNgoi)
                .FirstOrDefaultAsync(v => v.MaVe == id);

            if (ve == null)
            {
                TempData["Error"] = $"Không tìm thấy vé #{id}";
                return RedirectToAction(nameof(Index));
            }

            return View(ve);
        }

        // GET: /QuanLyVe/SoatVe
        [HttpGet]
        public IActionResult SoatVe()
        {
            return View();
        }

        // POST: /QuanLyVe/SoatVe
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SoatVe(string maQR)
        {
            if (string.IsNullOrWhiteSpace(maQR))
            {
                ViewBag.Error = "Vui lòng nhập hoặc quét mã QR/mã vé!";
                return View();
            }

            var q = maQR.Trim();
            var ve = await _context.VeDienTus
                .Include(v => v.ChiTietDatVe)
                    .ThenInclude(ct => ct!.DatVe)
                        .ThenInclude(dv => dv!.NguoiDung)
                .Include(v => v.ChiTietDatVe)
                    .ThenInclude(ct => ct!.DatVe)
                        .ThenInclude(dv => dv!.SuatChieu)
                            .ThenInclude(sc => sc!.Phim)
                .Include(v => v.ChiTietDatVe)
                    .ThenInclude(ct => ct!.DatVe)
                        .ThenInclude(dv => dv!.SuatChieu)
                            .ThenInclude(sc => sc!.PhongChieu)
                                .ThenInclude(pc => pc!.RapChieu)
                .Include(v => v.ChiTietDatVe)
                    .ThenInclude(ct => ct!.GheSuatChieu)
                        .ThenInclude(gsc => gsc!.GheNgoi)
                .FirstOrDefaultAsync(v => v.MaQR == q || v.MaVe.ToString() == q);

            if (ve == null)
            {
                ViewBag.Error = $"KHÔNG TÌM THẤY VÉ phù hợp với thông tin: '{q}'!";
                ViewBag.MaQR = q;
                return View();
            }

            if (ve.TrangThai == "DaHuy")
            {
                ViewBag.Warning = "VÉ NÀY ĐÃ BỊ HỦY! Không cho phép vào phòng chiếu.";
                ViewBag.Ve = ve;
                ViewBag.MaQR = q;
                return View();
            }

            if (ve.TrangThai == "DaSuDung")
            {
                ViewBag.Warning = $"VÉ ĐÃ SỬ DỤNG TRƯỚC ĐÓ vào lúc {ve.NgaySuDung:HH:mm dd/MM/yyyy}! Có thể vé bị quét lặp lại.";
                ViewBag.Ve = ve;
                ViewBag.MaQR = q;
                return View();
            }

            // Hợp lệ -> Check-in thành công
            ve.TrangThai = "DaSuDung";
            ve.NgaySuDung = DateTime.Now;
            await _context.SaveChangesAsync();

            ViewBag.Success = "SOÁT VÉ THÀNH CÔNG! Vé hợp lệ, mời khách vào phòng chiếu.";
            ViewBag.Ve = ve;
            ViewBag.MaQR = q;
            return View();
        }

        // POST: /QuanLyVe/HuyVe/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HuyVe(int id)
        {
            var ve = await _context.VeDienTus.FindAsync(id);
            if (ve == null)
            {
                TempData["Error"] = $"Không tìm thấy vé #{id}";
                return RedirectToAction(nameof(Index));
            }

            ve.TrangThai = "DaHuy";
            await _context.SaveChangesAsync();

            TempData["Success"] = $"Đã hủy vé điện tử #{id} thành công!";
            return RedirectToAction(nameof(Index));
        }
    }
}
