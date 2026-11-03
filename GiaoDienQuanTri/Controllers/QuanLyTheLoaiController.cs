using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GiaoDienQuanTri.Data;
using GiaoDienQuanTri.Models;

namespace GiaoDienQuanTri.Controllers
{
    public class QuanLyTheLoaiController : Controller
    {
        private readonly AdminDbContext _context;

        public QuanLyTheLoaiController(AdminDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _context.TheLoais
                .Include(t => t.PhimTheLoais)
                .OrderBy(t => t.TenTheLoai)
                .ToListAsync();
            return View(list);
        }

        public IActionResult Them()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Them(TheLoai model)
        {
            if (await _context.TheLoais.AnyAsync(t => t.TenTheLoai == model.TenTheLoai))
            {
                ModelState.AddModelError("TenTheLoai", "Tên thể loại đã tồn tại.");
            }

            if (ModelState.IsValid)
            {
                _context.TheLoais.Add(model);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Thêm thể loại mới thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> CapNhat(int id)
        {
            var tl = await _context.TheLoais.FindAsync(id);
            if (tl == null) return NotFound();
            return View(tl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CapNhat(int id, TheLoai model)
        {
            if (id != model.MaTheLoai) return NotFound();

            var tl = await _context.TheLoais.FindAsync(id);
            if (tl == null) return NotFound();

            if (ModelState.IsValid)
            {
                tl.TenTheLoai = model.TenTheLoai.Trim();
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Cập nhật thể loại thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Xoa(int id)
        {
            var tl = await _context.TheLoais.FindAsync(id);
            if (tl != null)
            {
                var countPhim = await _context.PhimTheLoais.CountAsync(pt => pt.MaTheLoai == id);
                if (countPhim > 0)
                {
                    TempData["ErrorMessage"] = $"Không thể xóa thể loại này vì đang có {countPhim} bộ phim liên kết.";
                    return RedirectToAction(nameof(Index));
                }

                _context.TheLoais.Remove(tl);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Đã xóa thể loại thành công.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

