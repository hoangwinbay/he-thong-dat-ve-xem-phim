using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GiaoDienQuanTri.Data;
using GiaoDienQuanTri.Models;

namespace GiaoDienQuanTri.Controllers
{
    public class QuanLyPhimController : Controller
    {
        private readonly AdminDbContext _context;

        public QuanLyPhimController(AdminDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string? search, string? trangThai)
        {
            var query = _context.Phims
                .Include(p => p.PhimTheLoais)
                .ThenInclude(pt => pt.TheLoai)
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p => p.TenPhim.Contains(search) || (p.DaoDien != null && p.DaoDien.Contains(search)));
            }

            if (!string.IsNullOrWhiteSpace(trangThai))
            {
                query = query.Where(p => p.TrangThai == trangThai);
            }

            var list = await query.OrderByDescending(p => p.MaPhim).ToListAsync();
            ViewBag.Search = search;
            ViewBag.TrangThai = trangThai;
            return View(list);
        }

        public async Task<IActionResult> Them()
        {
            ViewBag.TheLoais = await _context.TheLoais.OrderBy(t => t.TenTheLoai).ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Them(Phim model, List<int>? theLoaiIds)
        {
            if (ModelState.IsValid)
            {
                _context.Phims.Add(model);
                await _context.SaveChangesAsync();

                if (theLoaiIds != null && theLoaiIds.Any())
                {
                    foreach (var tid in theLoaiIds)
                    {
                        _context.PhimTheLoais.Add(new PhimTheLoai { MaPhim = model.MaPhim, MaTheLoai = tid });
                    }
                    await _context.SaveChangesAsync();
                }

                TempData["SuccessMessage"] = "Thêm phim mới thành công!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.TheLoais = await _context.TheLoais.OrderBy(t => t.TenTheLoai).ToListAsync();
            return View(model);
        }

        public async Task<IActionResult> CapNhat(int id)
        {
            var phim = await _context.Phims
                .Include(p => p.PhimTheLoais)
                .FirstOrDefaultAsync(p => p.MaPhim == id);

            if (phim == null) return NotFound();

            ViewBag.TheLoais = await _context.TheLoais.OrderBy(t => t.TenTheLoai).ToListAsync();
            ViewBag.SelectedTheLoais = phim.PhimTheLoais.Select(pt => pt.MaTheLoai).ToList();
            return View(phim);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CapNhat(int id, Phim model, List<int>? theLoaiIds)
        {
            if (id != model.MaPhim) return NotFound();

            var phim = await _context.Phims
                .Include(p => p.PhimTheLoais)
                .FirstOrDefaultAsync(p => p.MaPhim == id);

            if (phim == null) return NotFound();

            if (ModelState.IsValid)
            {
                phim.TenPhim = model.TenPhim;
                phim.MoTa = model.MoTa;
                phim.DaoDien = model.DaoDien;
                phim.DienVien = model.DienVien;
                phim.ThoiLuong = model.ThoiLuong;
                phim.NgayKhoiChieu = model.NgayKhoiChieu;
                phim.NgayKetThuc = model.NgayKetThuc;
                phim.QuocGia = model.QuocGia;
                phim.DoTuoi = model.DoTuoi;
                phim.Poster = model.Poster;
                phim.Trailer = model.Trailer;
                phim.TrangThai = model.TrangThai;

                // Cập nhật thể loại
                _context.PhimTheLoais.RemoveRange(phim.PhimTheLoais);
                if (theLoaiIds != null)
                {
                    foreach (var tid in theLoaiIds)
                    {
                        _context.PhimTheLoais.Add(new PhimTheLoai { MaPhim = id, MaTheLoai = tid });
                    }
                }

                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Cập nhật thông tin phim thành công!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.TheLoais = await _context.TheLoais.OrderBy(t => t.TenTheLoai).ToListAsync();
            return View(model);
        }

        public async Task<IActionResult> ChiTiet(int id)
        {
            var phim = await _context.Phims
                .Include(p => p.PhimTheLoais)
                .ThenInclude(pt => pt.TheLoai)
                .FirstOrDefaultAsync(p => p.MaPhim == id);

            if (phim == null) return NotFound();
            return View(phim);
        }

        [HttpPost]
        public async Task<IActionResult> Xoa(int id)
        {
            var phim = await _context.Phims.FindAsync(id);
            if (phim != null)
            {
                var theLoais = await _context.PhimTheLoais.Where(pt => pt.MaPhim == id).ToListAsync();
                _context.PhimTheLoais.RemoveRange(theLoais);
                _context.Phims.Remove(phim);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Đã xóa phim thành công.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

