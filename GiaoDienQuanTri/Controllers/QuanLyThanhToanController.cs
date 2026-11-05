using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GiaoDienQuanTri.Data;

namespace GiaoDienQuanTri.Controllers
{
    public class QuanLyThanhToanController : Controller
    {
        private readonly AdminDbContext _context;

        public QuanLyThanhToanController(AdminDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string? trangThai, string? phuongThuc, string? search)
        {
            var query = _context.ThanhToans
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
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(trangThai))
            {
                query = query.Where(tt => tt.TrangThai == trangThai);
            }

            if (!string.IsNullOrWhiteSpace(phuongThuc))
            {
                query = query.Where(tt => tt.PhuongThuc == phuongThuc);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim();
                query = query.Where(tt =>
                    (tt.MaGiaoDich != null && tt.MaGiaoDich.Contains(s)) ||
                    tt.MaDatVe.ToString().Contains(s) ||
                    (tt.DatVe != null && tt.DatVe.NguoiDung != null && (tt.DatVe.NguoiDung.HoTen.Contains(s) || tt.DatVe.NguoiDung.Email.Contains(s))));
            }

            ViewBag.TrangThai = trangThai;
            ViewBag.PhuongThuc = phuongThuc;
            ViewBag.Search = search;

            // Thống kê doanh thu
            ViewBag.TongDoanhThu = await _context.ThanhToans.Where(t => t.TrangThai == "ThanhCong").SumAsync(t => (decimal?)t.SoTien) ?? 0;
            ViewBag.SoGiaoDichThanhCong = await _context.ThanhToans.CountAsync(t => t.TrangThai == "ThanhCong");
            ViewBag.SoGiaoDichChoXuLy = await _context.ThanhToans.CountAsync(t => t.TrangThai == "ChoXuLy");
            ViewBag.SoGiaoDichHoanTien = await _context.ThanhToans.CountAsync(t => t.TrangThai == "HoanTien");

            var list = await query.OrderByDescending(tt => tt.NgayThanhToan).ToListAsync();
            return View(list);
        }

        public async Task<IActionResult> ChiTiet(int id)
        {
            var tt = await _context.ThanhToans
                .Include(t => t.DatVe)
                    .ThenInclude(dv => dv!.NguoiDung)
                .Include(t => t.DatVe)
                    .ThenInclude(dv => dv!.SuatChieu)
                        .ThenInclude(sc => sc!.Phim)
                .Include(t => t.DatVe)
                    .ThenInclude(dv => dv!.SuatChieu)
                        .ThenInclude(sc => sc!.PhongChieu)
                            .ThenInclude(pc => pc!.RapChieu)
                .Include(t => t.DatVe)
                    .ThenInclude(dv => dv!.ChiTietDatVes)
                        .ThenInclude(ct => ct.GheSuatChieu)
                            .ThenInclude(gsc => gsc!.GheNgoi)
                .FirstOrDefaultAsync(t => t.MaThanhToan == id);

            if (tt == null) return NotFound();

            return View(tt);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CapNhatTrangThai(int id, string trangThai)
        {
            var tt = await _context.ThanhToans
                .Include(t => t.DatVe)
                    .ThenInclude(dv => dv!.ChiTietDatVes)
                        .ThenInclude(ct => ct.GheSuatChieu)
                .FirstOrDefaultAsync(t => t.MaThanhToan == id);

            if (tt == null)
            {
                TempData["Error"] = "Không tìm thấy giao dịch thanh toán.";
                return RedirectToAction(nameof(Index));
            }

            tt.TrangThai = trangThai;

            if (tt.DatVe != null)
            {
                if (trangThai == "ThanhCong")
                {
                    tt.DatVe.TrangThai = "DaThanhToan";
                    foreach (var ct in tt.DatVe.ChiTietDatVes)
                    {
                        if (ct.GheSuatChieu != null)
                        {
                            ct.GheSuatChieu.TrangThai = "DaDat";
                            ct.GheSuatChieu.ThoiGianHetHan = null;
                        }
                    }
                }
                else if (trangThai == "HoanTien" || trangThai == "ThatBai")
                {
                    tt.DatVe.TrangThai = "DaHuy";
                    foreach (var ct in tt.DatVe.ChiTietDatVes)
                    {
                        if (ct.GheSuatChieu != null)
                        {
                            ct.GheSuatChieu.TrangThai = "Trong";
                            ct.GheSuatChieu.ThoiGianHetHan = null;
                        }
                    }
                }
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = $"Đã cập nhật trạng thái giao dịch #{id} thành '{trangThai}'.";
            return RedirectToAction(nameof(ChiTiet), new { id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HoanTien(int id)
        {
            return await CapNhatTrangThai(id, "HoanTien");
        }
    }
}
