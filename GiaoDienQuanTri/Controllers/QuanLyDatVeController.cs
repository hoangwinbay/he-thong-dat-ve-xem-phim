using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GiaoDienQuanTri.Data;

namespace GiaoDienQuanTri.Controllers
{
    public class QuanLyDatVeController : Controller
    {
        private readonly AdminDbContext _context;

        public QuanLyDatVeController(AdminDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string? trangThai, string? search)
        {
            var query = _context.DatVes
                .Include(dv => dv.NguoiDung)
                .Include(dv => dv.SuatChieu)
                    .ThenInclude(sc => sc!.Phim)
                .Include(dv => dv.SuatChieu)
                    .ThenInclude(sc => sc!.PhongChieu)
                        .ThenInclude(pc => pc!.RapChieu)
                .Include(dv => dv.ChiTietDatVes)
                    .ThenInclude(ct => ct.GheSuatChieu)
                        .ThenInclude(gsc => gsc!.GheNgoi)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(trangThai))
            {
                query = query.Where(dv => dv.TrangThai == trangThai);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim();
                query = query.Where(dv =>
                    dv.MaDatVe.ToString().Contains(s) ||
                    (dv.NguoiDung != null && (dv.NguoiDung.HoTen.Contains(s) || dv.NguoiDung.Email.Contains(s) || (dv.NguoiDung.SoDienThoai != null && dv.NguoiDung.SoDienThoai.Contains(s)))) ||
                    (dv.SuatChieu != null && dv.SuatChieu.Phim != null && dv.SuatChieu.Phim.TenPhim.Contains(s)));
            }

            ViewBag.TrangThai = trangThai;
            ViewBag.Search = search;

            var donDatVes = await query.OrderByDescending(dv => dv.NgayDat).ToListAsync();
            return View(donDatVes);
        }

        public async Task<IActionResult> ChiTiet(int id)
        {
            var donDatVe = await _context.DatVes
                .Include(dv => dv.NguoiDung)
                .Include(dv => dv.SuatChieu)
                    .ThenInclude(sc => sc!.Phim)
                .Include(dv => dv.SuatChieu)
                    .ThenInclude(sc => sc!.PhongChieu)
                        .ThenInclude(pc => pc!.RapChieu)
                .Include(dv => dv.ChiTietDatVes)
                    .ThenInclude(ct => ct.GheSuatChieu)
                        .ThenInclude(gsc => gsc!.GheNgoi)
                .FirstOrDefaultAsync(dv => dv.MaDatVe == id);

            if (donDatVe == null) return NotFound();

            return View(donDatVe);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CapNhatTrangThai(int id, string trangThai)
        {
            var donDatVe = await _context.DatVes
                .Include(dv => dv.ChiTietDatVes)
                    .ThenInclude(ct => ct.GheSuatChieu)
                .FirstOrDefaultAsync(dv => dv.MaDatVe == id);

            if (donDatVe == null)
            {
                TempData["Error"] = "Không tìm thấy đơn đặt vé.";
                return RedirectToAction(nameof(Index));
            }

            donDatVe.TrangThai = trangThai;

            // Xử lý cập nhật trạng thái ghế
            if (trangThai == "DaThanhToan")
            {
                foreach (var ct in donDatVe.ChiTietDatVes)
                {
                    if (ct.GheSuatChieu != null)
                    {
                        ct.GheSuatChieu.TrangThai = "DaDat";
                        ct.GheSuatChieu.ThoiGianHetHan = null;
                    }
                }
            }
            else if (trangThai == "DaHuy" || trangThai == "HetHan")
            {
                foreach (var ct in donDatVe.ChiTietDatVes)
                {
                    if (ct.GheSuatChieu != null)
                    {
                        ct.GheSuatChieu.TrangThai = "Trong";
                        ct.GheSuatChieu.ThoiGianHetHan = null;
                    }
                }
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = $"Đã cập nhật trạng thái đơn vé #{id} thành '{trangThai}' thành công.";
            return RedirectToAction(nameof(ChiTiet), new { id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> XoaDonDatVe(int id)
        {
            var donDatVe = await _context.DatVes
                .Include(dv => dv.ChiTietDatVes)
                    .ThenInclude(ct => ct.GheSuatChieu)
                .FirstOrDefaultAsync(dv => dv.MaDatVe == id);

            if (donDatVe == null)
            {
                TempData["Error"] = "Không tìm thấy đơn đặt vé cần xóa.";
                return RedirectToAction(nameof(Index));
            }

            // Giải phóng ghế nếu đang giữ
            foreach (var ct in donDatVe.ChiTietDatVes)
            {
                if (ct.GheSuatChieu != null && ct.GheSuatChieu.TrangThai != "DaDat")
                {
                    ct.GheSuatChieu.TrangThai = "Trong";
                    ct.GheSuatChieu.ThoiGianHetHan = null;
                }
            }

            _context.DatVes.Remove(donDatVe);
            await _context.SaveChangesAsync();

            TempData["Success"] = $"Đã xóa đơn đặt vé #{id} thành công.";
            return RedirectToAction(nameof(Index));
        }
    }
}
