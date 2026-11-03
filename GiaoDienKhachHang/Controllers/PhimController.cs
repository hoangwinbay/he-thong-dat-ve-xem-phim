using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GiaoDienKhachHang.Data;

namespace GiaoDienKhachHang.Controllers
{
    public class PhimController : Controller
    {
        private readonly CustomerDbContext _context;

        public PhimController(CustomerDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> DanhSach(string? trangThai, int? theLoaiId, string? search)
        {
            var query = _context.Phims
                .Include(p => p.PhimTheLoais)
                .ThenInclude(pt => pt.TheLoai)
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(trangThai))
            {
                query = query.Where(p => p.TrangThai == trangThai);
            }

            if (theLoaiId.HasValue)
            {
                query = query.Where(p => p.PhimTheLoais.Any(pt => pt.MaTheLoai == theLoaiId.Value));
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p => p.TenPhim.Contains(search) || (p.DienVien != null && p.DienVien.Contains(search)));
            }

            ViewBag.TheLoais = await _context.TheLoais.OrderBy(t => t.TenTheLoai).ToListAsync();
            ViewBag.SelectedTrangThai = trangThai;
            ViewBag.SelectedTheLoai = theLoaiId;
            ViewBag.Search = search;

            var list = await query.OrderByDescending(p => p.MaPhim).ToListAsync();
            return View(list);
        }

        public async Task<IActionResult> ChiTiet(int id)
        {
            var phim = await _context.Phims
                .Include(p => p.PhimTheLoais)
                .ThenInclude(pt => pt.TheLoai)
                .Include(p => p.SuatChieus)
                .ThenInclude(s => s.PhongChieu)
                .ThenInclude(pc => pc!.RapChieu)
                .FirstOrDefaultAsync(p => p.MaPhim == id);

            if (phim == null) return NotFound();

            var suatChieus = phim.SuatChieus
                .Where(s => s.TrangThai == "DangMoBan")
                .OrderBy(s => s.NgayChieu)
                .ThenBy(s => s.GioBatDau)
                .ToList();

            ViewBag.SuatChieus = suatChieus;
            return View(phim);
        }
    }
}

