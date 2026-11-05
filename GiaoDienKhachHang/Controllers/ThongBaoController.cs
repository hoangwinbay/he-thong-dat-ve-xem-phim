using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GiaoDienKhachHang.Data;
using GiaoDienKhachHang.Models;

namespace GiaoDienKhachHang.Controllers
{
    public class ThongBaoController : Controller
    {
        private readonly CustomerDbContext _context;

        public ThongBaoController(CustomerDbContext context)
        {
            _context = context;
        }

        // GET: /ThongBao
        public async Task<IActionResult> Index(string? loc, string? email)
        {
            var maNguoiDung = HttpContext.Session.GetInt32("MaNguoiDung");

            var query = _context.ThongBaos
                .Include(tb => tb.NguoiDung)
                .AsQueryable();

            if (maNguoiDung.HasValue)
            {
                query = query.Where(tb => tb.MaNguoiDung == maNguoiDung.Value);
            }
            else if (!string.IsNullOrWhiteSpace(email))
            {
                query = query.Where(tb => tb.NguoiDung != null && tb.NguoiDung.Email == email.Trim());
                ViewBag.Email = email;
            }
            else
            {
                // Mặc định lấy danh sách thông báo hệ thống gần nhất
                query = query.Take(20);
            }

            if (loc == "chuadoc")
            {
                query = query.Where(tb => !tb.DaDoc);
            }
            else if (loc == "dadoc")
            {
                query = query.Where(tb => tb.DaDoc);
            }

            ViewBag.Loc = loc ?? "tatca";
            var notifications = await query.OrderByDescending(tb => tb.NgayGui).ToListAsync();
            return View(notifications);
        }

        // GET: /ThongBao/ChiTiet/5
        public async Task<IActionResult> ChiTiet(int id)
        {
            var item = await _context.ThongBaos
                .Include(tb => tb.NguoiDung)
                .FirstOrDefaultAsync(tb => tb.MaThongBao == id);

            if (item == null)
            {
                TempData["Error"] = "Không tìm thấy thông báo.";
                return RedirectToAction(nameof(Index));
            }

            // Đánh dấu đã đọc
            if (!item.DaDoc)
            {
                item.DaDoc = true;
                await _context.SaveChangesAsync();
            }

            return View(item);
        }

        // POST: /ThongBao/DanhDauDaDoc/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DanhDauDaDoc(int id)
        {
            var item = await _context.ThongBaos.FindAsync(id);
            if (item != null && !item.DaDoc)
            {
                item.DaDoc = true;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: /ThongBao/DanhDauTatCaDaDoc
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DanhDauTatCaDaDoc()
        {
            var maNguoiDung = HttpContext.Session.GetInt32("MaNguoiDung");
            var query = _context.ThongBaos.Where(tb => !tb.DaDoc);

            if (maNguoiDung.HasValue)
            {
                query = query.Where(tb => tb.MaNguoiDung == maNguoiDung.Value);
            }

            var unreadList = await query.ToListAsync();
            foreach (var item in unreadList)
            {
                item.DaDoc = true;
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "Đã đánh dấu tất cả thông báo là đã đọc!";
            return RedirectToAction(nameof(Index));
        }

        // GET: /ThongBao/LaySoLuongChuaDoc
        [HttpGet]
        public async Task<IActionResult> LaySoLuongChuaDoc()
        {
            var maNguoiDung = HttpContext.Session.GetInt32("MaNguoiDung");
            var query = _context.ThongBaos.Where(tb => !tb.DaDoc);

            if (maNguoiDung.HasValue)
            {
                query = query.Where(tb => tb.MaNguoiDung == maNguoiDung.Value);
            }

            var count = await query.CountAsync();
            return Json(new { count });
        }
    }
}
