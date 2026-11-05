using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GiaoDienQuanTri.Data;
using GiaoDienQuanTri.Models;

namespace GiaoDienQuanTri.Controllers
{
    public class QuanLyThongBaoController : Controller
    {
        private readonly AdminDbContext _context;

        public QuanLyThongBaoController(AdminDbContext context)
        {
            _context = context;
        }

        // GET: /QuanLyThongBao
        public async Task<IActionResult> Index(string? search, bool? daDoc)
        {
            var query = _context.ThongBaos
                .Include(tb => tb.NguoiDung)
                .AsQueryable();

            if (daDoc.HasValue)
            {
                query = query.Where(tb => tb.DaDoc == daDoc.Value);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim();
                query = query.Where(tb =>
                    tb.TieuDe.Contains(s) ||
                    (tb.NoiDung != null && tb.NoiDung.Contains(s)) ||
                    (tb.NguoiDung != null && (tb.NguoiDung.HoTen.Contains(s) || tb.NguoiDung.Email.Contains(s))));
            }

            ViewBag.Search = search;
            ViewBag.DaDoc = daDoc;

            // Thống kê nhanh
            ViewBag.TongSo = await _context.ThongBaos.CountAsync();
            ViewBag.SoChuaDoc = await _context.ThongBaos.CountAsync(t => !t.DaDoc);
            ViewBag.SoDaDoc = await _context.ThongBaos.CountAsync(t => t.DaDoc);

            var list = await query.OrderByDescending(tb => tb.NgayGui).ToListAsync();
            return View(list);
        }

        // GET: /QuanLyThongBao/TaoMoi
        [HttpGet]
        public async Task<IActionResult> TaoMoi()
        {
            ViewBag.NguoiDungs = await _context.NguoiDungs.OrderBy(u => u.HoTen).ToListAsync();
            return View();
        }

        // POST: /QuanLyThongBao/TaoMoi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TaoMoi(int? maNguoiDung, string tieuDe, string noiDung, bool guiTatCa = false)
        {
            if (string.IsNullOrWhiteSpace(tieuDe))
            {
                ModelState.AddModelError("tieuDe", "Tiêu đề thông báo không được để trống.");
            }

            if (string.IsNullOrWhiteSpace(noiDung))
            {
                ModelState.AddModelError("noiDung", "Nội dung thông báo không được để trống.");
            }

            if (!guiTatCa && (!maNguoiDung.HasValue || maNguoiDung.Value <= 0))
            {
                ModelState.AddModelError("maNguoiDung", "Vui lòng chọn người nhận hoặc chọn phát sóng cho tất cả.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.NguoiDungs = await _context.NguoiDungs.OrderBy(u => u.HoTen).ToListAsync();
                return View();
            }

            if (guiTatCa)
            {
                var users = await _context.NguoiDungs.ToListAsync();
                var notifications = users.Select(u => new ThongBao
                {
                    MaNguoiDung = u.MaNguoiDung,
                    TieuDe = tieuDe.Trim(),
                    NoiDung = noiDung.Trim(),
                    NgayGui = DateTime.Now,
                    DaDoc = false
                }).ToList();

                _context.ThongBaos.AddRange(notifications);
                await _context.SaveChangesAsync();

                TempData["Success"] = $"Đã phát sóng thông báo thành công tới toàn bộ {notifications.Count} người dùng!";
            }
            else
            {
                var notification = new ThongBao
                {
                    MaNguoiDung = maNguoiDung!.Value,
                    TieuDe = tieuDe.Trim(),
                    NoiDung = noiDung.Trim(),
                    NgayGui = DateTime.Now,
                    DaDoc = false
                };

                _context.ThongBaos.Add(notification);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Đã gửi thông báo thành công tới khách hàng!";
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /QuanLyThongBao/ChiTiet/5
        public async Task<IActionResult> ChiTiet(int id)
        {
            var item = await _context.ThongBaos
                .Include(tb => tb.NguoiDung)
                .FirstOrDefaultAsync(tb => tb.MaThongBao == id);

            if (item == null)
            {
                TempData["Error"] = $"Không tìm thấy thông báo #{id}";
                return RedirectToAction(nameof(Index));
            }

            return View(item);
        }

        // POST: /QuanLyThongBao/Xoa/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Xoa(int id)
        {
            var item = await _context.ThongBaos.FindAsync(id);
            if (item == null)
            {
                TempData["Error"] = $"Không tìm thấy thông báo #{id}";
                return RedirectToAction(nameof(Index));
            }

            _context.ThongBaos.Remove(item);
            await _context.SaveChangesAsync();

            TempData["Success"] = $"Đã xóa thông báo #{id} thành công!";
            return RedirectToAction(nameof(Index));
        }
    }
}
