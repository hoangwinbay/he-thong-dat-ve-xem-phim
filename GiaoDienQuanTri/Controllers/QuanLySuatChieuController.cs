using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GiaoDienQuanTri.Data;
using GiaoDienQuanTri.Models;

namespace GiaoDienQuanTri.Controllers
{
    public class QuanLySuatChieuController : Controller
    {
        private readonly AdminDbContext _context;

        public QuanLySuatChieuController(AdminDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? maPhim, int? maPhong, DateTime? ngayChieu, string? trangThai)
        {
            var query = _context.SuatChieus
                .Include(s => s.Phim)
                .Include(s => s.PhongChieu)
                .ThenInclude(p => p!.RapChieu)
                .AsNoTracking();

            if (maPhim.HasValue) query = query.Where(s => s.MaPhim == maPhim.Value);
            if (maPhong.HasValue) query = query.Where(s => s.MaPhong == maPhong.Value);
            if (ngayChieu.HasValue)
            {
                var d = ngayChieu.Value.Date;
                query = query.Where(s => s.NgayChieu == d);
            }
            if (!string.IsNullOrWhiteSpace(trangThai)) query = query.Where(s => s.TrangThai == trangThai);

            ViewBag.Phims = await _context.Phims.OrderBy(p => p.TenPhim).ToListAsync();
            ViewBag.Phongs = await _context.PhongChieus.Include(p => p.RapChieu).OrderBy(p => p.MaRap).ThenBy(p => p.TenPhong).ToListAsync();
            ViewBag.SelectedPhim = maPhim;
            ViewBag.SelectedPhong = maPhong;
            ViewBag.SelectedNgay = ngayChieu?.ToString("yyyy-MM-dd");
            ViewBag.SelectedTrangThai = trangThai;

            var list = await query.OrderByDescending(s => s.NgayChieu).ThenBy(s => s.GioBatDau).ToListAsync();
            return View(list);
        }

        public async Task<IActionResult> Them()
        {
            ViewBag.Phims = await _context.Phims.OrderBy(p => p.TenPhim).ToListAsync();
            ViewBag.Phongs = await _context.PhongChieus.Include(p => p.RapChieu).OrderBy(p => p.MaRap).ThenBy(p => p.TenPhong).ToListAsync();
            return View(new SuatChieu { NgayChieu = DateTime.Today, GiaVe = 90000 });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Them(SuatChieu model)
        {
            if (model.GioKetThuc <= model.GioBatDau)
            {
                ModelState.AddModelError("GioKetThuc", "Giờ kết thúc phải lớn hơn giờ bắt đầu.");
            }

            var d = model.NgayChieu.Date;
            var conflict = await _context.SuatChieus.AnyAsync(s => s.MaPhong == model.MaPhong && s.NgayChieu == d && s.TrangThai != "Huy" && s.GioBatDau < model.GioKetThuc && s.GioKetThuc > model.GioBatDau);
            if (conflict)
            {
                ModelState.AddModelError("GioBatDau", "Phòng chiếu này đã có suất chiếu khác trong khung giờ này.");
            }

            if (ModelState.IsValid)
            {
                model.NgayChieu = d;
                _context.SuatChieus.Add(model);
                await _context.SaveChangesAsync();

                // Tự động khởi tạo ghế cho suất chiếu từ danh sách ghế phòng
                var ghesPhong = await _context.GheNgois.Where(g => g.MaPhong == model.MaPhong && g.TrangThai).ToListAsync();
                if (ghesPhong.Any())
                {
                    foreach (var g in ghesPhong)
                    {
                        decimal gia = model.GiaVe;
                        if (g.LoaiGhe == "VIP") gia += 20000;
                        else if (g.LoaiGhe == "Doi") gia += 80000;

                        _context.GheSuatChieus.Add(new GheSuatChieu
                        {
                            MaSuatChieu = model.MaSuatChieu,
                            MaGhe = g.MaGhe,
                            GiaVe = gia,
                            TrangThai = "Trong"
                        });
                    }
                    await _context.SaveChangesAsync();
                }

                TempData["SuccessMessage"] = "Thêm suất chiếu mới và tự động khởi tạo sơ đồ ghế thành công!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Phims = await _context.Phims.OrderBy(p => p.TenPhim).ToListAsync();
            ViewBag.Phongs = await _context.PhongChieus.Include(p => p.RapChieu).OrderBy(p => p.MaRap).ThenBy(p => p.TenPhong).ToListAsync();
            return View(model);
        }

        public async Task<IActionResult> CapNhat(int id)
        {
            var sc = await _context.SuatChieus.FindAsync(id);
            if (sc == null) return NotFound();

            ViewBag.Phims = await _context.Phims.OrderBy(p => p.TenPhim).ToListAsync();
            ViewBag.Phongs = await _context.PhongChieus.Include(p => p.RapChieu).OrderBy(p => p.MaRap).ThenBy(p => p.TenPhong).ToListAsync();
            return View(sc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CapNhat(int id, SuatChieu model)
        {
            if (id != model.MaSuatChieu) return NotFound();

            var sc = await _context.SuatChieus.FindAsync(id);
            if (sc == null) return NotFound();

            if (model.GioKetThuc <= model.GioBatDau)
            {
                ModelState.AddModelError("GioKetThuc", "Giờ kết thúc phải lớn hơn giờ bắt đầu.");
            }

            if (ModelState.IsValid)
            {
                sc.MaPhim = model.MaPhim;
                sc.MaPhong = model.MaPhong;
                sc.NgayChieu = model.NgayChieu.Date;
                sc.GioBatDau = model.GioBatDau;
                sc.GioKetThuc = model.GioKetThuc;
                sc.GiaVe = model.GiaVe;
                sc.TrangThai = model.TrangThai;

                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Cập nhật suất chiếu thành công!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Phims = await _context.Phims.OrderBy(p => p.TenPhim).ToListAsync();
            ViewBag.Phongs = await _context.PhongChieus.Include(p => p.RapChieu).OrderBy(p => p.MaRap).ThenBy(p => p.TenPhong).ToListAsync();
            return View(model);
        }

        public async Task<IActionResult> ChiTiet(int id)
        {
            var sc = await _context.SuatChieus
                .Include(s => s.Phim)
                .Include(s => s.PhongChieu)
                .ThenInclude(p => p!.RapChieu)
                .Include(s => s.GheSuatChieus)
                .ThenInclude(gs => gs.GheNgoi)
                .FirstOrDefaultAsync(s => s.MaSuatChieu == id);

            if (sc == null) return NotFound();
            return View(sc);
        }

        [HttpPost]
        public async Task<IActionResult> Xoa(int id)
        {
            var sc = await _context.SuatChieus.Include(s => s.GheSuatChieus).FirstOrDefaultAsync(s => s.MaSuatChieu == id);
            if (sc != null)
            {
                _context.GheSuatChieus.RemoveRange(sc.GheSuatChieus);
                _context.SuatChieus.Remove(sc);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Đã xóa suất chiếu thành công.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

