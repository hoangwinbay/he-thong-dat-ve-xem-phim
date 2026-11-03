using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GiaoDienQuanTri.Data;
using GiaoDienQuanTri.Models;

namespace GiaoDienQuanTri.Controllers
{
    public class QuanLyPhongChieuController : Controller
    {
        private readonly AdminDbContext _context;

        public QuanLyPhongChieuController(AdminDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? maRap)
        {
            var query = _context.PhongChieus
                .Include(p => p.RapChieu)
                .Include(p => p.GheNgois)
                .AsNoTracking();

            if (maRap.HasValue)
            {
                query = query.Where(p => p.MaRap == maRap.Value);
            }

            ViewBag.Raps = await _context.RapChieus.OrderBy(r => r.TenRap).ToListAsync();
            ViewBag.SelectedRap = maRap;

            var list = await query.OrderBy(p => p.MaRap).ThenBy(p => p.TenPhong).ToListAsync();
            return View(list);
        }

        public async Task<IActionResult> Them(int? maRap)
        {
            ViewBag.Raps = await _context.RapChieus.OrderBy(r => r.TenRap).ToListAsync();
            var model = new PhongChieu();
            if (maRap.HasValue) model.MaRap = maRap.Value;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Them(PhongChieu model)
        {
            if (await _context.PhongChieus.AnyAsync(p => p.MaRap == model.MaRap && p.TenPhong == model.TenPhong))
            {
                ModelState.AddModelError("TenPhong", "Rạp này đã có phòng chiếu cùng tên.");
            }

            if (ModelState.IsValid)
            {
                _context.PhongChieus.Add(model);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Thêm phòng chiếu mới thành công!";
                return RedirectToAction(nameof(Index), new { maRap = model.MaRap });
            }

            ViewBag.Raps = await _context.RapChieus.OrderBy(r => r.TenRap).ToListAsync();
            return View(model);
        }

        public async Task<IActionResult> CapNhat(int id)
        {
            var phong = await _context.PhongChieus.FindAsync(id);
            if (phong == null) return NotFound();

            ViewBag.Raps = await _context.RapChieus.OrderBy(r => r.TenRap).ToListAsync();
            return View(phong);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CapNhat(int id, PhongChieu model)
        {
            if (id != model.MaPhong) return NotFound();

            var phong = await _context.PhongChieus.FindAsync(id);
            if (phong == null) return NotFound();

            if (ModelState.IsValid)
            {
                phong.MaRap = model.MaRap;
                phong.TenPhong = model.TenPhong;
                phong.LoaiPhong = model.LoaiPhong;
                phong.SoLuongGhe = model.SoLuongGhe;

                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Cập nhật phòng chiếu thành công!";
                return RedirectToAction(nameof(Index), new { maRap = model.MaRap });
            }

            ViewBag.Raps = await _context.RapChieus.OrderBy(r => r.TenRap).ToListAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Xoa(int id)
        {
            var phong = await _context.PhongChieus.Include(p => p.GheNgois).FirstOrDefaultAsync(p => p.MaPhong == id);
            if (phong != null)
            {
                _context.GheNgois.RemoveRange(phong.GheNgois);
                _context.PhongChieus.Remove(phong);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Đã xóa phòng chiếu và toàn bộ ghế liên quan thành công.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

