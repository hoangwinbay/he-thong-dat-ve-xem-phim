using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GiaoDienQuanTri.Data;
using GiaoDienQuanTri.Models;

namespace GiaoDienQuanTri.Controllers
{
    public class QuanLyDanhGiaController : Controller
    {
        private readonly AdminDbContext _context;

        public QuanLyDanhGiaController(AdminDbContext context)
        {
            _context = context;
        }

        // GET: /QuanLyDanhGia
        public async Task<IActionResult> Index(int? soSao, int? maPhim, string? search)
        {
            var query = _context.DanhGiaPhims
                .Include(d => d.NguoiDung)
                .Include(d => d.Phim)
                .AsQueryable();

            if (soSao.HasValue && soSao.Value > 0)
            {
                query = query.Where(d => d.SoSao == soSao.Value);
            }

            if (maPhim.HasValue && maPhim.Value > 0)
            {
                query = query.Where(d => d.MaPhim == maPhim.Value);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim();
                query = query.Where(d =>
                    (d.BinhLuan != null && d.BinhLuan.Contains(s)) ||
                    (d.Phim != null && d.Phim.TenPhim.Contains(s)) ||
                    (d.NguoiDung != null && (d.NguoiDung.HoTen.Contains(s) || d.NguoiDung.Email.Contains(s))));
            }

            ViewBag.SoSao = soSao;
            ViewBag.MaPhim = maPhim;
            ViewBag.Search = search;
            ViewBag.Phims = await _context.Phims.OrderBy(p => p.TenPhim).ToListAsync();

            // Thống kê nhanh
            var allReviews = await _context.DanhGiaPhims.ToListAsync();
            ViewBag.TongSoDanhGia = allReviews.Count;
            ViewBag.DiemTrungBinh = allReviews.Count > 0 ? Math.Round(allReviews.Average(d => d.SoSao), 1) : 0;
            ViewBag.SoLuong5Sao = allReviews.Count(d => d.SoSao == 5);
            ViewBag.SoLuong1Sao = allReviews.Count(d => d.SoSao == 1);

            var list = await query.OrderByDescending(d => d.NgayDanhGia).ToListAsync();
            return View(list);
        }

        // GET: /QuanLyDanhGia/ChiTiet/5
        public async Task<IActionResult> ChiTiet(int id)
        {
            var item = await _context.DanhGiaPhims
                .Include(d => d.NguoiDung)
                .Include(d => d.Phim)
                .FirstOrDefaultAsync(d => d.MaDanhGia == id);

            if (item == null)
            {
                TempData["Error"] = $"Không tìm thấy đánh giá #{id}";
                return RedirectToAction(nameof(Index));
            }

            return View(item);
        }

        // POST: /QuanLyDanhGia/Xoa/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Xoa(int id)
        {
            var item = await _context.DanhGiaPhims.FindAsync(id);
            if (item == null)
            {
                TempData["Error"] = $"Không tìm thấy đánh giá #{id}";
                return RedirectToAction(nameof(Index));
            }

            _context.DanhGiaPhims.Remove(item);
            await _context.SaveChangesAsync();

            TempData["Success"] = $"Đã xóa đánh giá #{id} thành công!";
            return RedirectToAction(nameof(Index));
        }
    }
}
