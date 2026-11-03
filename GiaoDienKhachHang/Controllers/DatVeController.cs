using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GiaoDienKhachHang.Data;

namespace GiaoDienKhachHang.Controllers
{
    public class DatVeController : Controller
    {
        private readonly CustomerDbContext _context;

        public DatVeController(CustomerDbContext context)
        {
            _context = context;
        }

        // Bước 1: Chọn rạp
        public async Task<IActionResult> ChonRap(int? maPhim)
        {
            var raps = await _context.RapChieus
                .Where(r => r.TrangThai)
                .OrderBy(r => r.TenRap)
                .ToListAsync();

            if (maPhim.HasValue)
            {
                ViewBag.Phim = await _context.Phims.FindAsync(maPhim.Value);
            }

            ViewBag.SelectedPhim = maPhim;
            return View(raps);
        }

        // Bước 2: Chọn suất chiếu
        public async Task<IActionResult> ChonSuatChieu(int? maPhim, int? maRap, DateTime? ngayChieu)
        {
            var targetNgay = ngayChieu?.Date ?? DateTime.Today;

            var query = _context.SuatChieus
                .Include(s => s.Phim)
                .Include(s => s.PhongChieu)
                .ThenInclude(pc => pc!.RapChieu)
                .Where(s => s.TrangThai == "DangMoBan");

            if (maPhim.HasValue) query = query.Where(s => s.MaPhim == maPhim.Value);
            if (maRap.HasValue) query = query.Where(s => s.PhongChieu!.MaRap == maRap.Value);

            var suatChieus = await query
                .OrderBy(s => s.NgayChieu)
                .ThenBy(s => s.GioBatDau)
                .ToListAsync();

            ViewBag.Phims = await _context.Phims.Where(p => p.TrangThai == "DangChieu").ToListAsync();
            ViewBag.Raps = await _context.RapChieus.Where(r => r.TrangThai).ToListAsync();
            ViewBag.SelectedPhim = maPhim;
            ViewBag.SelectedRap = maRap;
            ViewBag.SelectedNgay = targetNgay;

            return View(suatChieus);
        }

        // Bước 3: Chọn ghế
        public async Task<IActionResult> ChonGhe(int id)
        {
            var suatChieu = await _context.SuatChieus
                .Include(s => s.Phim)
                .Include(s => s.PhongChieu)
                .ThenInclude(pc => pc!.RapChieu)
                .Include(s => s.GheSuatChieus)
                .ThenInclude(gs => gs.GheNgoi)
                .FirstOrDefaultAsync(s => s.MaSuatChieu == id);

            if (suatChieu == null) return NotFound();

            return View(suatChieu);
        }
    }
}

