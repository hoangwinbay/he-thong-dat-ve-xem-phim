using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GiaoDienQuanTri.Data;
using GiaoDienQuanTri.Models;

namespace GiaoDienQuanTri.Controllers
{
    public class QuanLyRapController : Controller
    {
        private readonly AdminDbContext _context;

        public QuanLyRapController(AdminDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _context.RapChieus
                .Include(r => r.PhongChieus)
                .OrderBy(r => r.MaRap)
                .ToListAsync();
            return View(list);
        }

        public IActionResult Them()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Them(RapChieu model)
        {
            if (ModelState.IsValid)
            {
                _context.RapChieus.Add(model);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Thêm cụm rạp mới thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> CapNhat(int id)
        {
            var rap = await _context.RapChieus.FindAsync(id);
            if (rap == null) return NotFound();
            return View(rap);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CapNhat(int id, RapChieu model)
        {
            if (id != model.MaRap) return NotFound();

            var rap = await _context.RapChieus.FindAsync(id);
            if (rap == null) return NotFound();

            if (ModelState.IsValid)
            {
                rap.TenRap = model.TenRap;
                rap.DiaChi = model.DiaChi;
                rap.SoDienThoai = model.SoDienThoai;
                rap.TrangThai = model.TrangThai;

                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Cập nhật thông tin rạp thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Xoa(int id)
        {
            var rap = await _context.RapChieus.Include(r => r.PhongChieus).FirstOrDefaultAsync(r => r.MaRap == id);
            if (rap != null)
            {
                if (rap.PhongChieus.Any())
                {
                    TempData["ErrorMessage"] = $"Không thể xóa rạp này vì đang có {rap.PhongChieus.Count} phòng chiếu.";
                    return RedirectToAction(nameof(Index));
                }

                _context.RapChieus.Remove(rap);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Đã xóa rạp chiếu thành công.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

