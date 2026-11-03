using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GiaoDienQuanTri.Data;
using GiaoDienQuanTri.Models;

namespace GiaoDienQuanTri.Controllers
{
    public class QuanLyGheController : Controller
    {
        private readonly AdminDbContext _context;

        public QuanLyGheController(AdminDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? maPhong)
        {
            var phongs = await _context.PhongChieus
                .Include(p => p.RapChieu)
                .OrderBy(p => p.MaRap)
                .ThenBy(p => p.TenPhong)
                .ToListAsync();

            ViewBag.Phongs = phongs;

            int targetPhong = maPhong ?? phongs.FirstOrDefault()?.MaPhong ?? 0;
            ViewBag.SelectedPhong = targetPhong;

            var ghes = await _context.GheNgois
                .Include(g => g.PhongChieu)
                .ThenInclude(p => p!.RapChieu)
                .Where(g => g.MaPhong == targetPhong)
                .OrderBy(g => g.HangGhe)
                .ThenBy(g => g.SoGhe)
                .ToListAsync();

            var currentPhong = phongs.FirstOrDefault(p => p.MaPhong == targetPhong);
            ViewBag.CurrentPhong = currentPhong;

            return View(ghes);
        }

        public async Task<IActionResult> SoDoGhe(int maPhong)
        {
            var phong = await _context.PhongChieus
                .Include(p => p.RapChieu)
                .FirstOrDefaultAsync(p => p.MaPhong == maPhong);

            if (phong == null) return NotFound();

            var ghes = await _context.GheNgois
                .Where(g => g.MaPhong == maPhong)
                .OrderBy(g => g.HangGhe)
                .ThenBy(g => g.SoGhe)
                .ToListAsync();

            ViewBag.Phong = phong;
            return View(ghes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Them(GheNgoi model)
        {
            model.HangGhe = model.HangGhe.Trim().ToUpper();

            if (await _context.GheNgois.AnyAsync(g => g.MaPhong == model.MaPhong && g.HangGhe == model.HangGhe && g.SoGhe == model.SoGhe))
            {
                TempData["ErrorMessage"] = $"Ghế {model.HangGhe}{model.SoGhe} đã tồn tại trong phòng chiếu này.";
                return RedirectToAction(nameof(Index), new { maPhong = model.MaPhong });
            }

            _context.GheNgois.Add(model);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = $"Đã thêm ghế {model.HangGhe}{model.SoGhe} thành công!";
            return RedirectToAction(nameof(Index), new { maPhong = model.MaPhong });
        }

        [HttpPost]
        public async Task<IActionResult> TaoSoDoMacDinh(int maPhong, int soHang = 6, int soGheMoiHang = 8)
        {
            var phong = await _context.PhongChieus.FindAsync(maPhong);
            if (phong == null) return NotFound();

            // Xóa ghế cũ nếu có
            var oldGhes = await _context.GheNgois.Where(g => g.MaPhong == maPhong).ToListAsync();
            _context.GheNgois.RemoveRange(oldGhes);

            char startChar = 'A';
            var newGhes = new List<GheNgoi>();

            for (int r = 0; r < soHang; r++)
            {
                string hang = ((char)(startChar + r)).ToString();
                string loai = (r < 2) ? "Thuong" : (r < soHang - 1 ? "VIP" : "Doi");
                int count = (loai == "Doi") ? soGheMoiHang / 2 : soGheMoiHang;

                for (int c = 1; c <= count; c++)
                {
                    newGhes.Add(new GheNgoi
                    {
                        MaPhong = maPhong,
                        HangGhe = hang,
                        SoGhe = c,
                        LoaiGhe = loai,
                        TrangThai = true
                    });
                }
            }

            _context.GheNgois.AddRange(newGhes);
            phong.SoLuongGhe = newGhes.Count;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Đã tự động khởi tạo {newGhes.Count} ghế cho phòng chiếu {phong.TenPhong}!";
            return RedirectToAction(nameof(Index), new { maPhong });
        }

        [HttpPost]
        public async Task<IActionResult> Xoa(int id, int maPhong)
        {
            var ghe = await _context.GheNgois.FindAsync(id);
            if (ghe != null)
            {
                _context.GheNgois.Remove(ghe);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Đã xóa ghế thành công.";
            }
            return RedirectToAction(nameof(Index), new { maPhong });
        }
    }
}

